using System;
using System.Collections.Generic;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawer
    {
        private Ground Ground { get; }

        private BlockDrawer Drawer { get; } = new BlockDrawer();
        private List<Drawable> ImagesAndPositions { get; }

        public GroundDrawer(Ground ground)
        {
            Ground = ground;
            ImagesAndPositions = new List<Drawable>();
            var width = Ground.Objects.GetLength(0);
            var height = Ground.Objects.GetLength(1);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (Ground.Objects[i, j] == StaticObject.Block)
                    {
                        ImagesAndPositions.Add(Drawer.GetDrawable(new Block(
                            Ground.BlockSize * i,
                            Ground.BlockSize * j,
                            Ground.BlockSize,
                            Ground.BlockSize)));
                    }
                }
            }
        }

        public IEnumerable<Drawable> GetDrawables()
        {
            return ImagesAndPositions;
        }
    }
}