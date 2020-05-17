using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class SceneDrawer : IDrawer
    {
        public int DrawingPriority { get; }

        private Scene Scene { get; }

        public SceneDrawer(Scene scene)
        {
            Scene = scene;
        }

        public void DrawItem(Graphics g)
        {
            throw new NotImplementedException();
        }
    }
}