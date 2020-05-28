using System;
using System.Drawing;
using System.IO;
using System.Linq;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Drawing
{
    public class HeroDrawer : IDrawer
    {
        public IGameObject MainItem => Player;
        public int DrawingPriority { get; } = 0;

        private Player Player { get; }
        private static Image[] MovingFrames { get; } = {Images.HeroRight};
        private static Image[] StandingFrames { get; } = {Images.HeroRight};
        private static Image[] JumpingFrames { get; } = {Images.HeroRight};
        private int CurrentStandingIndex { get; set; }
        private int CurrentMovingIndex { get; set; }
        private int CurrentJumpingIndex { get; set; }

        static HeroDrawer()
        {
            MovingFrames = Directory.GetFiles(@"Resources\Hero\Moving", "*.png")
                .OrderBy(str => str)
                .Select(Image.FromFile)
                .ToArray();

            StandingFrames = Directory.GetFiles(@"Resources\Hero\Standing", "*.png")
                .OrderBy(str => str)
                .Select(Image.FromFile)
                .ToArray();

            JumpingFrames = Directory.GetFiles(@"Resources\Hero\Jumping", "*.png")
                .OrderBy(str => str)
                .Select(Image.FromFile)
                .ToArray();
        }

        public HeroDrawer(Player player)
        {
            Player = player;
        }

        public void DrawItem(Graphics g)
        {
            HpBarDrawer.DrawHp(Player, g);
            if (Player.InJump)
            {
                CurrentStandingIndex = 0;
                CurrentMovingIndex = 0;

                DrawFrameConsideringDirection(g, JumpingFrames[CurrentJumpingIndex]);

                CurrentJumpingIndex = (CurrentJumpingIndex + 1) % JumpingFrames.Length;
            }
            else if (Player.IsMoving)
            {
                CurrentJumpingIndex = 0;
                CurrentStandingIndex = 0;

                DrawFrameConsideringDirection(g, MovingFrames[CurrentMovingIndex]);

                CurrentMovingIndex = (CurrentMovingIndex + 1) % MovingFrames.Length;
            }
            else
            {
                CurrentJumpingIndex = 0;
                CurrentMovingIndex = 0;

                DrawFrameConsideringDirection(g, StandingFrames[CurrentStandingIndex / 20]);

                CurrentStandingIndex = (CurrentStandingIndex + 1) % (StandingFrames.Length * 20);

            }
        }

        private void DrawFrameConsideringDirection(Graphics g, Image image)
        {
            if (Player.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }

            g.DrawImage(image, new RectangleF(Player.X, Player.Y, Player.Width, Player.Height));

            if (Player.Direction == Direction.Left)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }
    }
}