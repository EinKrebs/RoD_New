using System.Drawing;
using System.Security.AccessControl;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    partial class LevelChoosingMenu
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.Text = "LevelChoosingMenu";
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(179)))), ((int)(((byte)(233)))));

            this.Paint += DrawMenu;

            Timer = new Timer();
            Timer.Interval = 25;

            Timer.Tick += (sender, args) => Invalidate();

            Timer.Enabled = true;
        }

        #endregion

        private MenuAction[] Actions { get; set; }
        private Game Game { get; set; }
        private int Padding { get; } = 50;
        private int Margin { get; } = 10;
        private int ButtonHeight { get; } = 100;
        private int ButtonWidth { get; } = 400;
        private Font MainFont { get; } = new Font("Times New Roman",
            40,
            FontStyle.Bold,
            GraphicsUnit.Pixel);
        private Timer Timer { get; set; }
    }
}