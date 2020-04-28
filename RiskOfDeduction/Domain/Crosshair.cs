namespace RiskOfDeduction.Domain
{
    public class Crosshair : IGameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; } = 0;
        public int Height { get; } = 0;

        public void Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}