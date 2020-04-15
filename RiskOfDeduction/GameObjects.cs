using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RiskOfDeduction
{
    public interface IGameObject
    {
        int X { get; }
        int Y { get; }
        int Height { get; }
        int Width { get; }
        double Angle { get; }
        Image Image { get; }
        int DrawingPriority { get; }
        Action Act();
        bool DiesInConflict(IGameObject gameObject);
        void Update(Action action);
    }
}