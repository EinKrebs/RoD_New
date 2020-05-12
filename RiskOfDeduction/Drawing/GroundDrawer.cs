using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawer
    {
        private Ground Ground { get; }

        private List<BlockDrawer> Drawers { get; }

        public GroundDrawer(Ground ground)
        {
            Ground = ground;
            Drawers = new List<BlockDrawer>();
            var width = Ground.Objects.GetLength(0);
            var height = Ground.Objects.GetLength(1);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (Ground.Objects[i, j] == StaticObject.Block)
                    {
                        Drawers.Add(new BlockDrawer(
                            new RectangleF(
                                i * Ground.BlockSize,
                                j * Ground.BlockSize,
                                Ground.BlockSize,
                                Ground.BlockSize),
                            i > 0 && Ground.Objects[i - 1, j] == StaticObject.Nothing 
                        ));
                    }
                }
            }
        }

        public IEnumerable<IDrawable> GetDrawables()
        {
            return Drawers;
        }
    }
}