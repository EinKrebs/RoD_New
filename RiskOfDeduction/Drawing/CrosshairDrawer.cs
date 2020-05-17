using System;
using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class CrosshairDrawer : IDrawer
    {
        public int DrawingPriority { get; } = 100;

        private static Image CrossHairImage { get; } = Images.Crosshair;
        private Crosshair Crosshair { get; }

        public CrosshairDrawer(Crosshair crosshair)
        {
            Crosshair = crosshair;
        }

        public void DrawItem(Graphics g)
        {
            g.DrawImage(CrossHairImage, new RectangleF(Crosshair.X, Crosshair.Y, Crosshair.Width, Crosshair.Height));
        }
    }
}