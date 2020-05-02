using System;
using System.Linq;
using NUnit.Framework;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Tests
{
    [TestFixture]
    public class ShotTest
    {
        private static Game game;
        private static int blockSize;
        private static int sceneWidth;

        [SetUp]
        public static void SetUp()
        {
            game = new Game(500, 500);
            sceneWidth = 500;
            blockSize = 50;
        }

        [Test]
        public static void ShotTest_Creation_ByPlayer()
        {
            var textLevel = new[]
            {
                "##########",
                "          ",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            game.Crosshair.Move(2 * blockSize, 1 * blockSize);
            
            game.Player.Shoot();
            game.Update();

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Shot));
        }

        [Test]
        public static void ShotTest_Creation_ByTank()
        {
            var textLevel = new[]
            {
                "##########",
                "      ML  ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);

            var tank = game.CurrentLevel.CurrentScene.GetActives().First(active => active is Tank) as Tank;
            tank.Shoot();
            game.Update();

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Shot));
        }

        [Test]
        public static void ShotTest_Creation_ByTurret()
        {
            var textLevel = new[]
            {
                "##########",
                "      T   ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);

            var turret = game.CurrentLevel.CurrentScene.GetActives().First(active => active is Turret) as Turret;
            turret.Shoot();
            game.Update();

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Shot));
        }

        [Test]
        public static void ShotTest_KillingTurret_ByPlayer()
        {
            var textLevel = new[]
            {
                "##########",
                "  T       ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.Crosshair.Move(2 * blockSize, 1 * blockSize);

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Turret));

            for (var i = 0; i < 20; i++)
            {
                game.Player.Shoot();
                game.Update();
            }

            Assert.IsFalse(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Turret));
        }

        [Test]
        public static void ShotTest_KillingTank_ByPlayer()
        {
            var textLevel = new[]
            {
                "##########",
                "  ML      ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.Crosshair.Move(2 * blockSize, 1 * blockSize);

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Tank));

            for (var i = 0; i < 20; i++)
            {
                game.Player.Shoot();
                game.Update();
            }

            Assert.IsFalse(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Tank));
        }

        [Test]
        public static void ShotTest_KillingPlayer_ByTank()
        {
            var textLevel = new[]
            {
                "##########",
                "  ML      ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Tank));
            var tank = game.CurrentLevel.CurrentScene.GetActives().First(active => active is Tank) as Tank;

            for (var i = 0; i < 20; i++)
            {
                tank.Shoot();
                game.Update();
            }

            Assert.IsFalse(game.Running);
        }

        [Test]
        public static void ShotTest_KillingPlayer_ByTurret()
        {
            var textLevel = new[]
            {
                "##########",
                "  T       ",
                "##########"
            };
            game.InitializePlayer(0 * blockSize, 1 * blockSize, blockSize, blockSize);
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);

            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Turret));
            var turret = game.CurrentLevel.CurrentScene.GetActives().First(active => active is Turret) as Turret;

            for (var i = 0; i < 20; i++)
            {
                turret.Shoot();
                game.Update();
            }

            Assert.IsFalse(game.Running);
        }
    }
}