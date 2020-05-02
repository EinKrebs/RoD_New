using System.Linq;
using NUnit.Framework;
using RiskOfDeduction.Domain;

namespace RiskOfDeduction.Tests
{
    public class ObjectsTest
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
        public static void ObjectTest_OnTheSceneAppearing_TurretCreation()
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

            game.Update();
            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Turret));
        }

        [Test]
        public static void ObjectTest_OnTheSceneAppearing_TankCreation()
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

            game.Update();
            Assert.IsTrue(game.CurrentLevel.CurrentScene.GetActives().Any(active => active is Tank));
        }
    }
}