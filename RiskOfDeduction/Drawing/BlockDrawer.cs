using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class BlockDrawer : IDrawable
    {
        public Image Image { get; } = Images.Ground;
        public RectangleF Position { get; }
        public double Angle { get; } = 0;
        public int DrawingPriority { get; } = 0;

        private Block Block { get; }

        public BlockDrawer(Block block)
        {
            Block = block;
            Position = new RectangleF(Block.X, Block.Y, Block.Width, Block.Height);
        }

        public BlockDrawer(RectangleF position, bool grass)
        {
            Position = position;
            if (grass)
            {
                Image = Images.Grass;
            }
            else
            {
                Image = Images.Ground;
            }
        }

        public void Update()
        {
            
        }
    }
}