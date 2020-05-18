using System;
using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawer
    {
        IGameObject MainItem { get; }
        int DrawingPriority { get; }
        void DrawItem(Graphics g);
    }
}