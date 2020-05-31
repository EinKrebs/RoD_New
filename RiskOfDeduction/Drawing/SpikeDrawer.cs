using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class SpikeDrawer : IDrawer
    {
        public IGameObject MainItem => Spikes;
        public int DrawingPriority { get; } = 20;
        private Spikes Spikes { get; }
        private Image Image { get; } = Images.Spikes;

        public SpikeDrawer(Spikes spikes)
        {
            Spikes = spikes;
        }
        
        public void DrawItem(Graphics g)
        {
            g.DrawImage(Image, Spikes.X, Spikes.Y, Spikes.Width, Spikes.Height);
        }
    }
}