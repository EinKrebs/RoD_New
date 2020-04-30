using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawable
    {
        Image Image { get; }
        RectangleF Position { get; }
        double Angle { get; }
        int DrawingPriority { get; }
        

        void Update();
    }
}