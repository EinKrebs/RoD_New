using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class BlockDrawer
    {
        private Image Image { get; } = Images.Ground;

        public Drawable GetDrawable(Block block)
        {
            return new Drawable(Image, block.GetRect(), 100);
        }
    }
}