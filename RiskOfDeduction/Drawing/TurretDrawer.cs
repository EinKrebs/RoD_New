using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TurretDrawer : IDrawable
    {
        public Image Image { get; private set; }
        public RectangleF Position { get; private set; }
        public double Angle { get; } = 0;
        public int DrawingPriority { get; } = 10;
        private Turret Turret { get; }

        public TurretDrawer(Turret turret)
        {
            Turret = turret;
            Position = new RectangleF(Turret.X, Turret.Y, Turret.Width, Turret.Height);
            Update();
        }
        
        public void Update()
        {
            Image = Turret.Direction == Direction.Left 
                ? Turret.Firing ? Images.TurretLeftFiring : Images.TurretLeft
                : Turret.Firing ? Images.TurretRightFiring : Images.TurretRight;
            Position = new RectangleF(Turret.X, Turret.Y, Turret.Width, Turret.Height);
        }
    }
}