using System;
using System.Collections.Generic;
using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawer
    {
        IEnumerable<Drawable> GetDrawables();
    }
}