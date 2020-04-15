using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace RiskOfDeduction
{
    partial class GameWindow
    {
        private System.ComponentModel.IContainer components = null;
        private Timer timer { get; set; }
        private int gameHeight { get; } = 600;
        private int gameWidth { get; } = 1100;
        private int blockSize { get; } = 50;

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

            this.components = new System.ComponentModel.Container();

            this.ClientSize = new System.Drawing.Size(gameWidth, gameHeight);

            this.Text = "GameWindow";
            this.ClientSize = new Size(gameWidth, gameHeight);

            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = Images.Sky;

            timer = new Timer(components);
            timer.Enabled = true;
            timer.Interval = 25;
            timer.Tick += OnTimerTick;

            this.Paint += DrawGame;

            this.KeyDown += Game_KeyDown;
            this.KeyUp += Game_KeyUp;
        }

        #endregion
    }
}