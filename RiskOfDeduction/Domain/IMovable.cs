using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskOfDeduction.Domain
{
    public interface IMovable : IGameObject
    {
        float VelocityX { get; }
        float VelocityY { get; }
        float G { get; }
    }
}
