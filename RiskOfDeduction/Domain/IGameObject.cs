using System;
using System.Drawing;

namespace RiskOfDeduction.Domain
{
    public interface IGameObject
    {
        float X { get; }
        float Y { get; }
        int Width { get; }
        int Height { get; }

        bool DiesInColliding(IGameObject other);
    }

    public static class GameObjectExtension
    {
        public static RectangleF GetRect(this IGameObject gameObject)
        {
            return new RectangleF(gameObject.X, gameObject.Y, gameObject.Width, gameObject.Height);
        }
    }
}
