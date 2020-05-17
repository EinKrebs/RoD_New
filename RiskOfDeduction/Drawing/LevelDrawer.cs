using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class LevelDrawer : IDrawer
    {
        public int DrawingPriority { get; }

        private Level Level { get; }

        public LevelDrawer(Level level)
        {
            Level = level;
        }

        public void DrawItem(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}