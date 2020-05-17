using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TankDrawer : IDrawer
    {
        public int DrawingPriority { get; }
        
        private Tank Tank { get; }
        private static Image TankImage { get; } = Images.TankRight;
        private static Image FiringImage { get; } = Images.TankRightFiring;

        public TankDrawer(Tank tank)
        {
            Tank = tank;
        }

        public void DrawItem(Graphics g)
        {
            DrawFrameConsideringDirection(g, Tank.Firing ? FiringImage : TankImage);
        }

        private void DrawFrameConsideringDirection(Graphics g, Image image)
        {
            if (Tank.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }

            g.DrawImage(image, new RectangleF(Tank.X, Tank.Y, Tank.Width, Tank.Height));

            if (Tank.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }
    }
}