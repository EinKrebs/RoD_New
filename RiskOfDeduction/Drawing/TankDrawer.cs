using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TankDrawer : IDrawer
    {
        public int DrawingPriority { get; }
        
        private Tank Tank { get; }
        private static Image TankImage { get; }

        public TankDrawer(Tank tank)
        {
            Tank = tank;
        }

        public void DrawItem(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}