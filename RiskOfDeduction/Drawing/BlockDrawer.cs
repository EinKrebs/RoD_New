using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class BlockDrawer : IDrawable
    {
        public Image Image { get; } = Images.Ground;
        public RectangleF Frame { get; } = new RectangleF(0, 0, 50, 50);

        public void Update()
        {
            
        }
    }
}