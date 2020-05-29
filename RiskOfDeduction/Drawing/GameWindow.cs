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
        private bool ToRight { get; set; }
        private bool ToLeft { get; set; }
        private bool IsShooting { get; set; }
        private int ShootingTick { get; set; }
        private int MaxShootingTick { get; set; } = 4;
        private GameDrawer Drawer { get; set; }
        private Menu Menu { get; set; }

        public void InitializeGame(Game game)
        {
            Game = game;
            Drawer = new GameDrawer(game);
            Menu = new Menu(this, game);
            gameWidth = Game.Width;
            gameHeight = Game.Height;

            timer.Enabled = true;
        }

        public GameWindow()
        {
            InitializeComponent();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (Game.IsPaused)
            {
                Invalidate();
                return;
            }

            Game.Update();
            if (ToRight)
            {
                Game.Player.MoveTo(Direction.Right);
            }

            if (ToLeft)
            {
                Game.Player.MoveTo(Direction.Left);
            }

            if (!ToRight && !ToLeft)
            {
                Game.Player.Stop();
            }

            if (IsShooting)
            {
                ShootingTick = (ShootingTick + 1) % MaxShootingTick;
                if (ShootingTick == 0)
                {
                    Game.Player.Shoot();
                }
            }
            else
            {
                ShootingTick = 0; 
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
                        IsShooting = true;
                        Game.Player.Shoot();
                    }
                    else
                    {
                        Menu.OnClicked();
                    }
                    break;
            }
        }

        protected void Game_OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!Game.IsPaused)
                    {
                        IsShooting = true;
                    }

                    break;
            }
        }

        protected void Game_OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!Game.IsPaused)
                    {
                        IsShooting = false;
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

        public void Pause()
        {
            Game.Pause();
        }

        public void Play()
        {
            Game.Play();
        }

        public void ToMainMenu()
        {
            Game.Pause();
            Game.ToMainMenu();
        }
    }
}