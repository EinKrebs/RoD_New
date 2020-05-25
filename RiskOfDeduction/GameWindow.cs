using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RiskOfDeduction.Domain;
using System.Media;
using RiskOfDeduction.Drawing;
using Menu = RiskOfDeduction.Drawing.Menu;

namespace RiskOfDeduction
{
    public partial class GameWindow : Form
    {
        private Game Game { get; set; }
        private bool ToRight { get; set; } = false;
        private bool ToLeft { get; set; } = false;
        private GameDrawer Drawer { get; }
        private Menu Menu { get; }

        public GameWindow()
        {
            InitializeComponent();
            Game = new Game(gameWidth, gameHeight);
            var textLevel = new[]
            {
                "############################################################",
                "#                            ##                            #",
                "#                            ##                            #",
                "#                            ##                            #",
                "#                            ##                            #",
                "#                            ##                            #",
                "#                            ##                            #",
                "#                   ML T     ##                   ML T     #",
                "#           ####   ######                 ####   #####     #",
                "#          ######                        ######            #",
                "#     #############    T            #############    T     #",
                "############################################################"
                // "                              ",
                // "##############################"
            };
            Game.InitializePlayer(200, 200, blockSize / 2, blockSize);
            // Game.InitializePlayer(0, 0, blockSize, blockSize);
            Game.AddLevel(Level.GenerateLevelFromStringArray(textLevel, gameWidth, blockSize, Game));
            Drawer = new GameDrawer(Game);
            Menu = new Menu(this, Game);
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            Game.Update();
            if (ToRight)
            {
                Game.Player.MoveTo(Direction.Right);
            }

            if (ToLeft)
            {
                Game.Player.MoveTo(Direction.Left);
            }

            Invalidate();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            if (!Game.Running)
            {
                return;
            }

            var g = e.Graphics;
            Drawer.DrawItem(g);
            if (Game.IsPaused)
            {
                Menu.Draw(g);
                Drawer.CrosshairDrawer.DrawItem(g);
            }
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (!Game.IsPaused)
                {
                    Pause();
                }
                else
                {
                    Play();
                }
            }
            if (Game.IsPaused)
            {
                return;
            }
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
                    if (!Game.IsPaused)
                    {
                        Game.Player.Shoot();
                    }
                    else
                    {
                        Menu.OnClicked();
                    }

                    break;
            }
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            Game.Crosshair.Move(e.X, e.Y);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (Game.IsPaused)
            {
                return;
            }
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

        private void Game_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
            ToLeft = false;
            ToRight = false;
        }

        private void Pause()
        {
            timer.Stop();
            Game.Pause();
            Menu.MenuStart();
        }

        private void Play()
        {
            timer.Start();
            Game.Play();
            Menu.MenuFinish();
        }
    }
}