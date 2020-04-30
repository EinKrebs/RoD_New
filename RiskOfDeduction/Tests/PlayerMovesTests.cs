using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Tests
{
    [TestFixture]
    public static class PlayerMovesTests
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
        public static void BasicMoves_MoveToRight_WhenMapIsClear()
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

            var oldX = game.Player.X;
            game.Player.MoveTo(Direction.Right);
            var newX = game.Player.X;

            Assert.AreEqual(game.Player.VelocityX * game.OneTick, newX - oldX, float.Epsilon);
        }

        [Test]
        public static void BasicMoves_MoveToLeft_WhenMapIsClear()
        {
            var textLevel = new[]
            {
                "##########",
                "#        #",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(2 * blockSize, 1 * blockSize, blockSize, blockSize);

            var oldX = game.Player.X;
            game.Player.MoveTo(Direction.Left);
            var newX = game.Player.X;

            Assert.AreEqual(-game.Player.VelocityX * game.OneTick, newX - oldX, float.Epsilon);
        }

        [Test]
        public static void BasicMoves_DontMoveToRight_GroundToRight()
        {
            var textLevel = new[]
            {
                "##########",
                "# #       ",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(2 * blockSize, 1 * blockSize, blockSize, blockSize);

            var oldX = game.Player.X;
            game.Player.MoveTo(Direction.Right);
            var newX = game.Player.X;

            Assert.AreEqual(0, newX - oldX, float.Epsilon);
        }

        [Test]
        public static void BasicMoves_DontMoveToLeft_GroundToLeft()
        {
            var textLevel = new[]
            {
                "##########",
                "# #       ",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(2 * blockSize, 1 * blockSize, blockSize, blockSize);

            var oldX = game.Player.X;
            game.Player.MoveTo(Direction.Left);
            var newX = game.Player.X;

            Assert.AreEqual(0, newX - oldX, float.Epsilon);
        }

        [Test]
        public static void YMoves_DontFallOnGround_GroundIsUnderLegs()
        {
            var textLevel = new[]
            {
                "          ",
                "          ",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(2 * blockSize, 1 * blockSize, blockSize, blockSize);

            var oldY = game.Player.Y;
            for (var i = 0; i < 10; i++)
            {
                game.Player.Update();
            }
            var newY = game.Player.Y;

            Assert.AreEqual(0, newY - oldY, float.Epsilon);
        }

        [Test]
        public static void YMoves_StopsAtGroundWhenFalling_GroundUnderLegsAtSomDistance()
        {
            var textLevel = new[]
            {
                "          ",
                "          ",
                "##########"
            };
            var level = Level.GenerateLevelFromStringArray(textLevel, sceneWidth, blockSize, game);
            game.AddLevel(level);
            game.InitializePlayer(2 * blockSize, 0 * blockSize, blockSize, blockSize);

            var oldY = game.Player.Y;
            for (var i = 0; i < 100; i++)
            {
                game.Player.Update();
            }
            var newY = game.Player.Y;

            Assert.AreEqual(50, newY - oldY, float.Epsilon);
        }
    }
}
