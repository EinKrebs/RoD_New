using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class LevelFinishedMenu
    {
            private GameWindow Parent { get; }
            private string[] ActionLabels { get; } = { "Choose another level" };
            private MenuAction[] Actions { get; }
            private Game Game { get; }
            private Font MainFont { get; } = new Font(
                "Times New Roman",
                40,
                FontStyle.Bold,
                GraphicsUnit.Pixel);
            public bool Stopped { get; set; }

            public LevelFinishedMenu(GameWindow parent, Game game)
            {
                Parent = parent;
                var a = new Label();
                Actions = new MenuAction[ActionLabels.Length];
                Game = game;
            }

            public void Draw(Graphics g)
            {
                var menuSize = new Size(400, 600);
                var menuX = Parent.Width / 2 - menuSize.Width / 2;
                var menuY = (Parent.Height - menuSize.Height) / 2;
                var padding = 200;

                var gameOverSize = new Size(300, 200);
                var gameOverPos = new PointF(Parent.Width / 2 - gameOverSize.Width / 2, menuY + 20);

                var textWidth = 400;
                var textHeight = 80;


                g.DrawImage(Images.Menu, new RectangleF(new PointF(menuX, menuY), menuSize));


                var brush = Brushes.Black;
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                var font = new Font(
                    "Times New Roman",
                    80,
                    FontStyle.Bold,
                    GraphicsUnit.Pixel);

            g.DrawString("Congratulations",
                    MainFont,
                    brush,
                    new RectangleF(gameOverPos, gameOverSize),
                    stringFormat);


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
                        case "Choose another level":
                            Parent.ToLevelChoose();
                            break;
                    }
                }
            }

            private void DrawAction(MenuAction action, Graphics g)
            {
                var brush = action.Rect.Contains(Parent.PointToClient(Cursor.Position)) ? Brushes.IndianRed : Brushes.Black;
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                g.DrawString(action.Label,
                    MainFont,
                    brush,
                    action.Rect,
                    stringFormat);
            }
        }
}

