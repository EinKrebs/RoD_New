using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawable
    {
        Tuple<Image, RectangleF> GetImageAndPosition(IGameObject obj);
    }
}