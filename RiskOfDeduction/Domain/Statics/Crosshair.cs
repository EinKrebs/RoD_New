namespace RiskOfDeduction.Domain
{
    public class Crosshair : IGameObject
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public int Width { get; } = 13;
        public int Height { get; } = 13;

        public bool DiesInColliding(IGameObject other)
        {
            return false;
        }

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}