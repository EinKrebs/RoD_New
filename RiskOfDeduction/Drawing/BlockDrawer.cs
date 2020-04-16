using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class BlockDrawer : IDrawable
    {
        public Image Image { get; } = Images.Ground;
        public RectangleF Position { get; }
        
        private Block Block { get; }

        public BlockDrawer(Block block)
        {
            Block = block;
            Position = new RectangleF(Block.X, Block.Y, Block.Width, Block.Height);
        }

        public void Update()
        {
            
        }
    }
}