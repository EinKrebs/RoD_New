using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class HeroDrawer : IDrawable
    {
        public Image Image { get; private set; }
        public RectangleF Frame { get; private set; }

        private Player Player { get; }
        private int Count { get; } = 1;
        private int CurrentFrame { get; set; }
        private Direction Direction { get; set; }
        private float PlayerOldX { get; set; }
        private Image LeftSprite { get; set; }
        private Image RightSprite { get; set; }

        public HeroDrawer(Player player)
        {
            Player = player;
            RightSprite = Image.FromFile("Resources/Hero.png");
            LeftSprite = Image.FromFile("Resources/HeroLeft.png");
            PlayerOldX = Player.X;
        }

        public void Update()
        {
            if (Player.X - PlayerOldX > 0)
            {
                if (Direction == Direction.Right)
                {
                    CurrentFrame = (CurrentFrame + 1) % Count;
                }
                else
                {
                    CurrentFrame = 0;
                    Direction = Direction.Right;
                }
            }
            else
            {
                if (Direction == Direction.Left)
                {
                    CurrentFrame = (CurrentFrame + 1) % Count;
                }
                else
                {
                    CurrentFrame = 0;
                    Direction = Direction.Left;
                }
            }

            PlayerOldX = Player.X;
            Image = Direction == Direction.Left ? LeftSprite : RightSprite;
            Frame = new RectangleF(Player.Width * CurrentFrame, 0, Player.Width, Player.Height);
        }
    }
}