using System;
using System.Drawing;
using System.Windows.Forms;
using RiskOfDeduction.Domain;
using System.Media;

namespace RiskOfDeduction
{
    public partial class GameWindow : Form
    {
        private Game game;
        private bool toRight { get; set; } = false;
        private bool toLeft { get; set; } = false;

        public GameWindow()
        {
            InitializeComponent();
            game = new Game(gameWidth, gameHeight);
            var textLevel = new[]
            {
                "######################",
                "#                    #",
                "#                    #",
                "#                    #",
                "#                    #",
                "#         ##         #",
                "#      #####         #",
                "#                    #",
                "#           ####     #",
                "#          #####     #",
                "#     #############  #",
                "######################"
            };
            game.InitializePlayer(200, 200, blockSize, blockSize);
            game.AddLevel(Level.GenerateLevelFromStringArray(textLevel, gameWidth, blockSize));
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            game.Player.UpdateYPos();
            if (toRight)
            {
                game.Player.MoveTo(Direction.Right);
            }

            if (toLeft)
            {
                game.Player.MoveTo(Direction.Left);
            }
            Refresh();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.DrawImage(Images.Hero, new RectangleF(game.Player.X, game.Player.Y, game.Player.Width, game.Player.Height));
            foreach (var block in game.CurrentLevel.CurrentScene.LandScape)
            {
                g.DrawImage(Images.Ground, new RectangleF(block.X, block.Y, block.Width, block.Height));
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    toRight = true;
                    break;
                case Keys.A:
                    toLeft = true;
                    break;
                case Keys.Space:
                    game.Player.Jump();
                    break;
            }

            if (toRight)
            {
                game.Player.MoveTo(Direction.Right);
            }

            if (toLeft)
            {
                game.Player.MoveTo(Direction.Left);
            }
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    toRight = false;
                    break;
                case Keys.A:
                    toLeft = false;
                    break;
            }
        }
    }
}
