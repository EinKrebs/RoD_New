using System.Drawing;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    partial class MainMenu
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
            this.DoubleBuffered = true;
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(179)))), ((int)(((byte)(233)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainMenu";
            this.Text = "Main menu";
            this.ResumeLayout(false);
            this.WindowState = FormWindowState.Maximized;
            this.MaximumSize = new Size(1920, 1080);
            this.MinimumSize = this.MaximumSize;
            actions = new[]
            {
                new MainMenuElement(
                    MainMenuResources.ChoseLevelSelected,
                    MainMenuResources.ChoseLevel,
                    new RectangleF(),
                    "ChooseLevel"),

                new MainMenuElement(
                    MainMenuResources.ExitSelected,
                    MainMenuResources.Exit,
                    new RectangleF(),
                    "Exit")
            };
            this.Paint += DrawMenu;
            Timer = new Timer();
            Timer.Interval = 25;
            Timer.Enabled = true;

            Timer.Tick += (sender, args) => Invalidate();
        }

        #endregion

        private Game Game { get; set; }

        private MainMenuElement[] actions { get; set; }
        private int Padding { get; } = 50;
        private int Margin { get; } = 10;
        private int ButtonHeight { get; } = 100;
        private int ButtonWidth { get; } = 300;
        private int LogoWidth { get; } = 300;
        private int LogoHeight { get; } = 300;
        private Timer Timer { get; set; }
    }
}