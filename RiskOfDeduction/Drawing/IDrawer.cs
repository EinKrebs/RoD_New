using System.Windows.Forms;

namespace RiskOfDeduction.Drawing
{
    public interface IDrawer
    {
        void DrawTo(object sender, PaintEventArgs e);
    }
}