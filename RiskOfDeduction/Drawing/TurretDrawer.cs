using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TurretDrawer
    {
        private static Image Left { get; } = Images.TurretLeft;
        private static Image Right { get; } = Images.TurretRight; 
        private static Image LeftFiring { get; } = Images.TurretLeftFiring; 
        private static Image RightFiring { get; } = Images.TurretRightFiring;

        public Drawable GetDrawable(Turret turret)
        {
            var position = turret.GetRect();
            var image = turret.Firing
                ? turret.Direction == Direction.Left
                    ? LeftFiring
                    : RightFiring
                : turret.Direction == Direction.Left
                    ? Left
                    : Right;
            return new Drawable(image, position, 10);
        }
    }
}