using System.Drawing;
using System.Windows.Forms;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class GameDrawer : IDrawer
    {
        private Game Game { get; set; }
        private Image Back { get; set; }
        private HeroDrawer HeroDrawer { get; set; }
        private BlockDrawer BlockDrawer { get; set; }

        public GameDrawer(Game game, string back)
        {
            Game = game;
            Back = Image.FromFile(back);
            HeroDrawer = new HeroDrawer(game.Player);
            BlockDrawer = new BlockDrawer();
        }

        public void DrawTo(object sender, PaintEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}