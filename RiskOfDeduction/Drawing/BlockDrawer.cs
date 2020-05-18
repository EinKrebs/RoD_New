using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class BlockDrawer : IDrawer
    {
        public IGameObject MainItem => Block;
        public int DrawingPriority { get; } = 3;

        private static Image Image { get; set; } = Images.Ground;
        private Block Block { get; }

        public BlockDrawer(Block block)
        { 
            Block = block;
        }

        public void DrawItem(Graphics g)
        {
            g.DrawImage(Image, new RectangleF(Block.X, Block.Y, Block.Width, Block.Height));
        }

        public static void SetBlockImage(Image image)
        {
            Image = image;
        }
    }
}