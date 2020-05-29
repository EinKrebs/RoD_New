using System;
using System.ComponentModel.Design;
using System.Media;
using System.Windows.Media;
using NUnit.Framework.Constraints;
using static System.Math;

namespace RiskOfDeduction.Domain
{
    public class Shot : IMovable, IActive
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public float VelocityX { get; }
        public float VelocityY { get; }
        public float G { get; } = 10f;
        public double Angle { get; }
        public ShotSender Sender { get;  }

        private float Speed { get; } = 30f;
        private Game Game { get; }
        private static MediaPlayer PlayerShotSound { get; } = new MediaPlayer();
        private static MediaPlayer EnemyShotSound { get; } = new MediaPlayer();

        public bool DiesInColliding(IGameObject other)
        {
            if (Sender == ShotSender.Player && other is Player
                || (Sender == ShotSender.Tank || Sender == ShotSender.Turret) && (other is Turret || other is Tank))
            {
                return false;
            }

            return !(other is Shot);
        }

        public Shot(float x, float y, double angle, int size, Game game, ShotSender sender)
        {
            VelocityX = (float)Cos(angle) * Speed;
            VelocityY = (float)Sin(angle) * Speed;
            Angle = angle;
            X = x;
            Y = y;
            Width = size;
            Height = size;
            Game = game;
            Game.CurrentLevel.CurrentScene.AddShot(this);
            Sender = sender;
            if (sender == ShotSender.Player)
            {
                PlayerShotSound.Open(new Uri(@"Resources\Sounds\Shot\1.wav", UriKind.Relative));
                PlayerShotSound.Play();
            }
            else
            {
                EnemyShotSound.Open(new Uri(@"Resources\Sounds\Shot\2.wav", UriKind.Relative));
                EnemyShotSound.Play();
            }
        }

        public void Update()
        {
            X += VelocityX;
            Y += VelocityY;
        }
    }
}