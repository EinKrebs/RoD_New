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
        private List<SceneDrawer> SceneDrawers { get; }

        public LevelDrawer(Level level)
        {
            Level = level;
            SceneDrawers = level.Scenes.Select(scene => new SceneDrawer(scene)).ToList();
        }

        public void DrawItem(Graphics g)
        {
            SceneDrawers[Level.CurrentSceneIndex].DrawItem(g);
        }
    }
}