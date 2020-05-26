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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        public void DrawMenu(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(MainMenuResources.RoD, 
                new RectangleF((Width - LogoWidth) / 2,
                    Padding,
                    LogoWidth,
                    LogoHeight));   

            var pos = PointToClient(Cursor.Position);
            for (var i = 0; i < actions.Length; i++)
            {
                actions[i].Rect = new RectangleF((Width - ButtonWidth) / 2,
                    LogoHeight + 2 * Padding + (ButtonHeight + Margin) * i,
                    ButtonWidth,
                    ButtonHeight);
                g.DrawImage(actions[i].Rect.Contains(pos) ? actions[i].Selected : actions[i].Image, actions[i].Rect);
            }
        }

        public void InitializeGame(Game game)
        {
            Game = game;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            var pos = e.Location;
            var action = actions.FirstOrDefault(a => a.Rect.Contains(pos));

            switch (action?.Label)
            {
                case "Exit":
                    // Game.Over(false);
                    this.Close();
                    break;
                case "ChooseLevel":
                    Game.BlockSize = 50;
                    Game.InitializePlayer(300, 300, Game.BlockSize / 2, Game.BlockSize / 2);
                    var map = new[]
                    {
                        "####################",
                        "#        ##        #",
                        "#        ##        #",
                        "#        ##        #",
                        "#        ##        #",
                        "#        ##        #",
                        "#        ##        #",
                        "#                  #",
                        "#                  #",
                        "####################"
                    };
                    Game.AddLevel(Level.GenerateLevelFromStringArray(map, 10 * Game.BlockSize, Game.BlockSize, Game));
                    Game.Play();
                    break;

            }
        }
    }
}
