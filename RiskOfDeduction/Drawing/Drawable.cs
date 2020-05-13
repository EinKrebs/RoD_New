using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class Drawable
    {
        public Image Image { get; }
        public RectangleF Position { get; }
        public int DrawingPriority { get; }

        public Drawable(Image image, RectangleF position, int drawingPriority)
        {
            Image = image;
            Position = position;
            DrawingPriority = drawingPriority;
        }
    }
}