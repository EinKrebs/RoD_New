using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class ShotDrawer
    {
        private static Image Image { get; } = Images.Shot;

        public Drawable GetDrawable(Shot shot)
        {
            return new Drawable(Image, shot.GetRect(), 1000);
        }
    }
}