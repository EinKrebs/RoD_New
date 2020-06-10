using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;
using RiskOfDeduction.Domain;
using System.Windows.Media;

namespace RiskOfDeduction.Drawing
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "MainForm";
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = new Size(1920, 1080);
            this.MinimumSize = this.MaximumSize;

            GameWindow = new GameWindow(this);
            MainMenu = new MainMenu();
            Game = new Game(1920, 1080);
            ChoosingMenu = new LevelChoosingMenu();

            this.Width = Game.Width;
            this.Height = Game.Height;
            Game.GameStateChanged += On_GameStateChanged;
            InitializeGame();

            MainMenu.Dock = DockStyle.Fill;
            MainMenu.TopLevel = false;
            MainMenu.ControlBox = false;
            MainMenu.FormBorderStyle = FormBorderStyle.None;
            MainMenu.KeyPreview = true;
            MainMenu.Closed += (sender, args) => Close();

            GameWindow.Dock = DockStyle.Fill;
            GameWindow.TopLevel = false;
            GameWindow.ControlBox = false;
            GameWindow.FormBorderStyle = FormBorderStyle.None;
            GameWindow.KeyPreview = true;
            GameWindow.Closed += (sender, args) => Close();

            ChoosingMenu.Dock = DockStyle.Fill;
            ChoosingMenu.TopLevel = false;
            ChoosingMenu.ControlBox = false;
            ChoosingMenu.FormBorderStyle = FormBorderStyle.None;
            ChoosingMenu.KeyPreview = true;

            Player = new MediaPlayer();
            Player.Open(new Uri(@"Resources\Sounds\BackGround\1.wav", UriKind.Relative));
            Player.MediaEnded += (sender, args) =>
            {
                Player.Open(new Uri(@"Resources\Sounds\BackGround\1.wav", UriKind.Relative));
                Player.Play();
            };
            Player.Play();

            this.Controls.Add(MainMenu);
            this.Controls.Add(GameWindow);
            this.Controls.Add(ChoosingMenu);
        }

        private void InitializeGame()
        {
            var levelPaths = Directory.GetFiles(@"Resources\Levels", "*.txt");
            foreach (var level in levelPaths)
            {
                Game.AddLevel(Level.FromFile(level, Game));   
            }
        }

        #endregion

        private MainMenu MainMenu { get; set; }
        private GameWindow GameWindow { get; set; }
        private LevelChoosingMenu ChoosingMenu { get; set; }
        private Game Game { get; set; }
        public MediaPlayer Player { get; set; }
    }
}