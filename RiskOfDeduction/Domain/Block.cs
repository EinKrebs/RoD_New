using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public class Block : IGameObject
    {
        public float X { get; }
        public float Y { get; }
        public int Width { get; }
        public int Height { get; }
        public bool DiesInColliding(IGameObject other)
        {
            return false;
        }

        public Block(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Math.Abs((X.GetHashCode() * 397) ^
                       (Y.GetHashCode() * 239) ^
                       (Width.GetHashCode() * 601) ^
                       (Height.GetHashCode() * 443));
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Block block &&
                   Math.Abs(block.X - X) < float.Epsilon &&
                   Math.Abs(block.Y - Y) < float.Epsilon &&
                   block.Width == Width &&
                   block.Height == Height;
        }
    }
}
