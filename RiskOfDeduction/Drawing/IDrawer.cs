using System.Collections.Generic;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawer
    {
        IEnumerable<IDrawable> GetDrawables();
    }
}