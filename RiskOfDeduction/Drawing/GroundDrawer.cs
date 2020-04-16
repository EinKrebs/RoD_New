using System.Collections;
using System.Collections.Generic;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IEnumerable<BlockDrawer>
    {
        private Ground Ground { get; set; }

        private List<BlockDrawer> Drawer { get; set; }

        public GroundDrawer(Ground ground)
        {
            foreach (var block in ground)
            {
                Drawer.Add(new BlockDrawer());
            }
        }
        
        public IEnumerator<BlockDrawer> GetEnumerator()
        {
            return ((IEnumerable<BlockDrawer>) Drawer).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}