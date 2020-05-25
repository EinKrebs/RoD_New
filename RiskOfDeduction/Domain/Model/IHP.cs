using System.Windows.Forms.VisualStyles;

namespace RiskOfDeduction.Domain
{
    public interface IHp
    {
        int Hp { get; }
        int MaxHP { get; }
    }
}