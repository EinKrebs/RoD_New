using System.Collections.Generic;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawer
    {
        private Ground Ground { get; set; }

        private List<BlockDrawer> Drawer { get; }

        public GroundDrawer(Ground ground)
        {
            Drawer = new List<BlockDrawer>();
            foreach (var block in ground)
            {
                Drawer.Add(new BlockDrawer(block));
            }
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return Drawer;
        }
    }
}