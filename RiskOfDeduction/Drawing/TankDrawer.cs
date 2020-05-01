using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TankDrawer : IDrawable
    {
        public Image Image { get; private set; }
        public RectangleF Position { get; private set; }
        public double Angle { get; } = 0;
        public int DrawingPriority { get; } = 10;
        private Tank Tank { get; }

        public TankDrawer(Tank tank)
        {
            Tank = tank;
            Update();
        }
        
        public void Update()
        {
            Position = new RectangleF(Tank.X, Tank.Y, Tank.Width, Tank.Height);
            Image = Tank.Direction == Direction.Left ? Images.TankLeft : Images.TankRight;
        }
    }
}