using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class GroundDrawer : IDrawable
    {
        public Image Image { get; } = Image.FromFile("Resources/Ground.png");
        public RectangleF Frame { get; } = new RectangleF(0, 0, 50, 50);

        public void Update()
        {
            
        }
    }
}