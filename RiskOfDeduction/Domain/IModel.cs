using System.Collections.Generic;

namespace RiskOfDeduction.Domain
{
    public interface IModel
    {
        IEnumerable<IGameObject> Objects { get; }
        bool AreColliding(IGameObject first, IGameObject second);
        void Update();
        void Remove(IGameObject gameObject);
    }
}