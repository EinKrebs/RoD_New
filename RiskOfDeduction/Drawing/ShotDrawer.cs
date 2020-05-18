using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class ShotDrawer : IDrawer
    {
        public IGameObject MainItem => Shot;
        public int DrawingPriority { get; } = 5;

        private static Image ShotImage { get; } = Images.Shot;
        private Shot Shot { get; }

        public ShotDrawer(Shot shot)
        {
            Shot = shot;
        }

        public void DrawItem(Graphics g)
        {
            g.DrawImage(ShotImage, new RectangleF(Shot.X, Shot.Y, Shot.Width, Shot.Height));
        }
    }
}