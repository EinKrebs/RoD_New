using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class Menu
    {
        private Form Parent { get; }
        private Timer Timer { get; }
        private string[] ActionLabels { get; } = {"Continue", "Exit", "To main menu"};
        private MenuAction[] Actions { get; }
        private Game Game { get; }
        private Font MainFont { get; } = new Font("Times New Roman",
            40,
            FontStyle.Bold,
            GraphicsUnit.Pixel);

        public Menu(GameWindow parent, Game game)
        {
            Parent = parent;
            Timer = new Timer();
            var a = new Label();
            Actions = new MenuAction[ActionLabels.Length];
            Game = game;

            Timer.Interval = 25;
            Timer.Tick += (sender, args) => Parent.Invalidate();
        }

        public void MenuStart()
        {
            Timer.Start();
        }

        public void MenuFinish()
        {
            Timer.Stop();
        }

        public void Draw(Graphics g)
        {
            var menuSize = new Size(400, 600);
            var menuX = Parent.Width / 2 - menuSize.Width / 2;
            var menuY = (Parent.Height - menuSize.Height) / 2;
            var padding = 100;

            var textWidth = 400;
            var textHeight = 80;


            g.DrawImage(Images.Menu, new RectangleF(new PointF(menuX, menuY), menuSize));

            for (var i = 0; i < ActionLabels.Length; i++)
            {
                Actions[i] = new MenuAction(new RectangleF((Parent.Width - textWidth) / 2,
                    menuY + i * textHeight + padding,
                    textWidth,
                    textHeight), ActionLabels[i]);
                DrawAction(Actions[i], g);
            }

        }

        public void OnClicked()
        {
            var action = Actions.FirstOrDefault(act => act.Rect.Contains(Parent.PointToClient(Cursor.Position)));
            if (action != null)
            {
                switch (action.Label)
                {
                    case "Continue":
                        SendKeys.Send("{ESCAPE}");
                        break;
                    case "Exit":
                        Parent.Close();
                        break;
                }
            }
        }

        private void DrawAction(MenuAction action, Graphics g)
        {
            var brush = action.Rect.Contains(Parent.PointToClient(Cursor.Position)) ? Brushes.IndianRed : Brushes.Black;
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };

            g.DrawString(action.Label,
                MainFont,
                brush,
                action.Rect,
                stringFormat);
        }
    }
}
