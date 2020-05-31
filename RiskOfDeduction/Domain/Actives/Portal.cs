namespace RiskOfDeduction.Domain
{
    public class Portal : IActive
    {
        public float X { get; }
        public float Y { get; }
        public int Width { get; }
        public int Height { get; }
        private Game Game { get; }

        public Portal(int x, int y, Game game)
        {
            X = x;
            Y = y;
            Game = game;
            Width = Game.BlockSize;
            Height = Game.BlockSize;
        }
        
        public bool DiesInColliding(IGameObject other)
        {
            if (other is Player)
            {
                Game.CurrentLevel.Finished = true;
                return true;
            }

            return false;
        }

        public void Update()
        {
            return;
        }
    }
}