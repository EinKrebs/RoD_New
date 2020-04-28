using System;
using System.Drawing;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class HeroDrawer : IDrawable
    {
        public Image Image { get; private set; }
        public RectangleF Position { get; private set; }

        private Player Player { get; }
        private int Count { get; } = 2;
        private int FrameTime { get; } = 5;
        private bool Moving { get; set; } = false;
        private int CurrentFrame { get; set; }
        private Direction Direction { get; set; }
        private float PlayerOldX { get; set; }
        private Image LeftSprite { get; set; }
        private Image RightSprite { get; set; }
        private Image[,] MovingFrames { get; set; }
        private Image[] StandingFrames { get; set; }

        public HeroDrawer(Player player)
        {
            Player = player;
            RightSprite = Images.Hero;
            LeftSprite = Images.HeroLeft;
            PlayerOldX = Player.X;
            MovingFrames = new Image[,] {{Images.RightWalk1, Images.RightWalk2}, {Images.LeftWalk1, Images.LeftWalk2}};
            StandingFrames = new Image[]{Images.RightStand, Images.LeftStand};
            Update();
        }

        public void Update()
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
        }
    }
}