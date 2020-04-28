using System.Collections.Generic;

namespace RiskOfDeduction.Domain
{
    public interface IModel
    {
        IEnumerable<IGameObject> Objects { get; }
        void Update();
        void Remove(IGameObject gameObject);
    }
}