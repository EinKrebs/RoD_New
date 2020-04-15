using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiskOfDeduction
{
    public partial class GameForm : Form
    {
        private Game game { get; set; }
        private Size SpaceSize { get; set; }
        private Image image { get; set; }
        private Timer timer { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            DoubleBuffered = true;
            timer.Start();
            game = new Game(new Player(150, 150, 0, "Images/rocketRight.png", 1, game));
            Ground.FillRectangle(0, 256, 1024, 256, "Images/Terrain.png")
                .ForEach(ground => game.AddObject(ground));
        }

        public GameForm()
        {
            timer = new Timer {Interval = 10};
            timer.Tick += TimerTick;
            timer.Start();
            InitializeComponent();
            SpaceSize = ClientRectangle.Size;
            image = new Bitmap(SpaceSize.Width, SpaceSize.Height, PixelFormat.Format32bppArgb);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            game.Act();
            Invalidate();
            Update();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SpaceSize = ClientRectangle.Size;
            image = new Bitmap(SpaceSize.Width, SpaceSize.Height, PixelFormat.Format32bppArgb);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.A:
                    game.player.MoveLeft();
                    break;
                case Keys.D:
                    game.player.MoveRight();
                    break;
                case Keys.Space:
                    game.player.Jump();
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            game.HandleKey(e.KeyCode,false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Bisque, ClientRectangle);
            var g = Graphics.FromImage(image);
            DrawTo(g);
            e.Graphics.DrawImage(image, (ClientRectangle.Width - image.Width)/2, (ClientRectangle.Height - image.Height) / 2);
        }

        private void DrawTo(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(Brushes.Beige, ClientRectangle);

            var matrix = g.Transform;

            foreach (var gameObject in game.List.Select(item => item.GameObject)
                .OrderBy(x => x.DrawingPriority))
            {
                g.Transform = matrix;
                g.TranslateTransform(gameObject.X, gameObject.Y);
                g.RotateTransform((float) gameObject.Angle);
                g.DrawImage(gameObject.Image, new Point(-gameObject.Width/2, -gameObject.Height/2));
            }
        }
    }
}