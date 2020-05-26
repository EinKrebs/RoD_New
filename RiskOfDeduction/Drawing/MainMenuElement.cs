using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class MainMenuElement
    {
        public Image Selected { get; set; }
        public Image Image { get; set; }
        public RectangleF Rect { get; set; }
        public string Label { get; set; }

        public MainMenuElement(Image selected, Image image, RectangleF rect, string label)
        {
            Selected = selected;
            Image = image;
            Rect = rect;
            Label = label;
        }
    }
}