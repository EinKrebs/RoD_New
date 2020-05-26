using System;
using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class MenuAction
    {
        public RectangleF Rect { get; set; }
        public string Label { get; set; }
        public Image Image { get; set; }

        public MenuAction(RectangleF rect, string label)
        {
            Label = label;
            Rect = rect;
        }

        public void SetImage(Image image)
        {
            Image = image;
        }

        public void SetRect(RectangleF rect)
        {
            Rect = rect;
        }
    }
}