using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class ShotDrawer : IDrawable
    {
        public Image Image { get; }
        public RectangleF Position { get; private set; }
        public double Angle { get; }
        public int DrawingPriority { get; } = 10;
        private Shot Shot { get; }

        public ShotDrawer(Shot shot)
        {
            Shot = shot;
            Image = Images.Shot;
            Angle = shot.Angle;
            Update();
        }
        
        public void Update()
        {
            Position = new RectangleF(Shot.X, Shot.Y, Shot.Width, Shot.Height);
        }
    }
}