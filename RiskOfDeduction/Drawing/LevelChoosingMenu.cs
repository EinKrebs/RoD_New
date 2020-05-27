using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public partial class LevelChoosingMenu : Form
    {
        public LevelChoosingMenu()
        {
            InitializeComponent();
        }

        public void InitializeGame(Game game)
        {
            Game = game;
            Actions = Game.Levels
                .Select((level, index) => new MenuAction(new RectangleF((Width - ButtonWidth) / 2,
                        Padding + (ButtonHeight + Margin) * index,
                        ButtonWidth,
                        ButtonHeight),
                    level.Name))
                .ToArray();
        }

        private void DrawMenu(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            for (var i = 0; i < Actions.Length; i++)
            {
                Actions[i].Rect = new RectangleF((Width - ButtonWidth) / 2,
                    Padding + (ButtonHeight + Margin) * i,
                    ButtonWidth,
                    ButtonHeight);
            }

            foreach (var action in Actions)
            {
                DrawAction(action, g);
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            var pos = e.Location;
            var actionTup = Actions.Select((act, index) => Tuple.Create(act, index))
                .FirstOrDefault(tup => tup.Item1.Rect.Contains(pos));
            if (actionTup != null)
            {
                var index = actionTup.Item2;
                Game.CurrentLevelIndex = index;
                Game.InitializePlayer(100, 100, Game.BlockSize / 2, Game.BlockSize);
                BlockDrawer.SetBlockImage(Image.FromFile($"{@"Resources\Textures\"}{Game.CurrentLevel.LevelStyle}{@"\1.png"}"));
                Game.Play();
            }
        }
    }
}
