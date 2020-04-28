using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class ShotDrawer : IDrawable
    {
        public Image Image { get; }
        public RectangleF Position { get; set; }
        private Shot Shot { get; }

        public ShotDrawer(Shot shot)
        {
            Shot = shot;
            Image = Images.Shot;
            Update();
        }
        
        public void Update()
        {
            Position = new RectangleF(Shot.X, Shot.Y, Shot.Width, Shot.Height);
        }
    }
}