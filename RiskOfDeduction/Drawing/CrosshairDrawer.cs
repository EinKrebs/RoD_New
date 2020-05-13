using System;
using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class CrosshairDrawer
    {
        public Image Image { get; }
        public RectangleF Position { get; private set; }
        public double Angle { get; } = 0;
        public int DrawingPriority { get; } = 100;
        private Crosshair Crosshair { get; }

        public CrosshairDrawer(Crosshair crosshair)
        {
            Crosshair = crosshair;
            Image = Images.Crosshair;
            GetDrawable();
        }
        public Drawable GetDrawable()
        {
            Position = new RectangleF(Crosshair.X, Crosshair.Y, Crosshair.Width, Crosshair.Height);
            return new Drawable(Image, Position, DrawingPriority);
        }
    }
}