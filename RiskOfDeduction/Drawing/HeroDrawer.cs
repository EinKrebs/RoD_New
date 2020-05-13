using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class HeroDrawer
         {
        public Image Image { get; private set; }
        public RectangleF Position { get; private set; }
        public double Angle { get; } = 0;
        public int DrawingPriority { get; } = 0;

        private Player Player { get; }
        private int Count { get; } = 1;
        private int FrameTime { get; } = 5;
        private bool Moving { get; set; }
        private int CurrentFrame { get; set; }
        private Direction Direction { get; set; }
        private float PlayerOldX { get; set; }
        private Image LeftSprite { get; }
        private Image RightSprite { get; }
        private Image[,] MovingFrames { get; }
        private Image[] StandingFrames { get; }

        public HeroDrawer(Player player)
        {
            Player = player;
            RightSprite = Images.HeroRight;
            LeftSprite = Images.HeroLeft;
            PlayerOldX = Player.X;
            MovingFrames = new Image[,] {{Images.HeroRight}, {Images.HeroLeft}};
            StandingFrames = new Image[]{Images.HeroRight, Images.HeroLeft};
            GetDrawable();
        }

        public Drawable GetDrawable()
        {
            if (Player.X - PlayerOldX > 0)
            {
                if (Direction == Direction.Right)
                {
                    CurrentFrame = (CurrentFrame + 1) % (Count * FrameTime);
                }
                else
                {
                    CurrentFrame = 0;
                    Direction = Direction.Right;
                }
                Moving = true;
            }
            else if (Player.X - PlayerOldX < 0)
            {
                if (Direction == Direction.Left)
                {
                    CurrentFrame = (CurrentFrame + 1) % (Count * FrameTime);
                }
                else
                {
                    CurrentFrame = 0;
                    Direction = Direction.Left;
                }
                Moving = true;
            }
            else
            {
                Moving = false;
            }

            PlayerOldX = Player.X;
            Image = Direction == Direction.Left 
                ? (Moving ? MovingFrames[1, CurrentFrame / FrameTime] : StandingFrames[1]) 
                : (Moving ? MovingFrames[0, CurrentFrame / FrameTime] : StandingFrames[0]);
            Position = new RectangleF(Player.X, Player.Y, Player.Width, Player.Height);
            return new Drawable(Image, Position, DrawingPriority);
        }
    }
}