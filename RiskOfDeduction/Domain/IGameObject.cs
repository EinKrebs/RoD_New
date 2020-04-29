using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
