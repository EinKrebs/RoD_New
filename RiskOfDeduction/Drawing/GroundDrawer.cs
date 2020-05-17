using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawer
    {
        public int DrawingPriority { get; }

        private Ground Ground { get; }

        public GroundDrawer(Ground ground)
        {
            Ground = ground;
        }

        public void DrawItem(Graphics g)
        {
            throw new System.NotImplementedException();
        }
    }
}