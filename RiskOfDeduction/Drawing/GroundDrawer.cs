using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawer
    {
        public IGameObject MainItem => Ground;
        public int DrawingPriority { get; } = 2;

        private Ground Ground { get; }
        private List<IDrawer> blockDrawers { get; }

        public GroundDrawer(Ground ground)
        {
            Ground = ground;
            blockDrawers = ground.groundBlocks.Select(block => (IDrawer) new BlockDrawer(block)).ToList();
        }

        public void DrawItem(Graphics g)
        {
            blockDrawers.ForEach(drawer => drawer.DrawItem(g));
        }
    }
}