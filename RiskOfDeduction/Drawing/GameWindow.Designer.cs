using System.Drawing;
using System.Media;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction
{
    partial class GameWindow
    {
        private System.ComponentModel.IContainer components = null;
        private Timer timer { get; set; }
        private int gameHeight { get; set; } = 600;
        private int gameWidth { get; set; } = 1500;
        private int blockSize { get; } = 50;
        private SoundPlayer ShotSound { get; set; }

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
            this.ClientSize = new System.Drawing.Size(this.gameWidth, this.gameHeight);
            this.Text = "GameWindow";
            this.ClientSize = new System.Drawing.Size(this.gameWidth, this.gameHeight);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackgroundImage = global::RiskOfDeduction.Images.Sky;
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer.Interval = 25;
            this.timer.Tick += this.OnTimerTick;
            this.Paint += this.DrawGame;
            this.KeyDown += this.Game_KeyDown;
            this.KeyUp += this.Game_KeyUp;
            this.MouseClick += this.Game_MouseClick;
            this.MouseMove += this.Game_MouseMove;
            this.MouseEnter += (sender, args) => Cursor.Hide();
            this.MouseLeave += this.Game_MouseLeave;
            this.MouseUp += Game_OnMouseUp;
            this.MouseDown += Game_OnMouseDown;
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            ShotSound = new SoundPlayer(@"D:\RoD\RoD_New\RiskOfDeduction\bin\Debug\Resources\Sounds\Shot\1.wav");
        }

        #endregion
    }
}