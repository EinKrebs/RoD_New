using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class CrosshairDrawer : IDrawable
    {
        public Image Image { get; }
        public RectangleF Position { get; private set; }
        private Crosshair Crosshair { get; }

        public CrosshairDrawer(Crosshair crosshair)
        {
            Crosshair = crosshair;
            Image = Images.Crosshair;
            Update();
        }
        public void Update()
        {
            Position = new RectangleF(Crosshair.X, Crosshair.Y, Crosshair.Width, Crosshair.Height);
        }
    }
}