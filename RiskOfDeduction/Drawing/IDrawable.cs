using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawable
    {
        Image Image { get; }
        RectangleF Frame { get; }

        void Update();
    }
}