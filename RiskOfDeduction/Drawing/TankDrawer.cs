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
            if (Tank.Firing)
            {
                g.DrawImage(FiringImage, new RectangleF());
            }
        }
    }
}