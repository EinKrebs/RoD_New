using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class TankDrawer
    {
        private static Image Left { get; } = Images.TankLeft;
        private static Image Right { get; } = Images.TankRight;
        private static Image LeftFiring { get; } = Images.TankLeftFiring;
        private static Image RightFiring { get; } = Images.TankRightFiring;
        
        public Drawable GetDrawable(Tank tank)
        {
            var position = tank.GetRect();
            var image = tank.Firing
                ? tank.Direction == Direction.Left 
                    ? LeftFiring 
                    : RightFiring
                : tank.Direction == Direction.Left
                    ? Left
                    : Right;
            return new Drawable(image, position, 9);
        }
    }
}