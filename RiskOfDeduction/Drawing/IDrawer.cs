using System;
using System.Collections.Generic;
using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawer
    {
        int DrawingPriority { get; }
        void DrawItem(Graphics g);
    }
}