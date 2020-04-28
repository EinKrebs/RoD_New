using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RiskOfDeduction.Domain;
using System.Media;
using RiskOfDeduction.Drawing;

namespace RiskOfDeduction
{
    public partial class GameWindow : Form
    {
        private Game Game { get; set; }
        private bool ToRight { get; set; } = false;
        private bool ToLeft { get; set; } = false;
        private GameDrawer Drawer { get; } 

        public GameWindow()
        {
            InitializeComponent();
            Game = new Game(gameWidth, gameHeight);
            var textLevel = new[]
            {
                "######################",
                "#                    #",
                "#                    #",
                "#                    #",
                "#                    #",
                "#         ##         #",
                "#      ####          #",
                "#                    #",
                "#           ####     #",
                "#          #####     #",
                "#     #############  #",
                "######################"
            };
            Game.InitializePlayer(200, 200, blockSize, blockSize);
            Game.AddLevel(Level.GenerateLevelFromStringArray(textLevel, gameWidth, blockSize));
            Drawer = new GameDrawer(Game);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Game.Update();
            Drawer.UpdateDrawables();
            if (ToRight)
            {
                Game.Player.MoveTo(Direction.Right);
            }

            if (ToLeft)
            {
                Game.Player.MoveTo(Direction.Left);
            }
            Refresh();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            
            Drawer.GetDrawables().ToList().ForEach(drawable => g.DrawImage(drawable.Image, drawable.Position));
            
            // g.DrawImage(Images.Hero, new RectangleF(game.Player.X, game.Player.Y, game.Player.Width, game.Player.Height));
            // foreach (var block in Game.CurrentLevel.CurrentScene.LandScape)
            // {
            //     g.DrawImage(Images.Ground, new RectangleF(block.X, block.Y, block.Width, block.Height));
            // }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    ToRight = true;
                    break;
                case Keys.A:
                    ToLeft = true;
                    break;
                case Keys.Space:
                    Game.Player.Jump();
                    break;
            }
        }

        private void Game_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Game.Player.Shoot();
                    break;
            }
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            Game.Crosshair.Move(e.X, e.Y);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    ToRight = false;
                    break;
                case Keys.A:
                    ToLeft = false;
                    break;
            }
        }
    }
}
