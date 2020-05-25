using System;
using System.Drawing;

namespace RiskOfDeduction.Drawing
{
    public class MenuAction
    {
        public RectangleF Rect { get; set; }
        public string Label { get; set; }

        public MenuAction(RectangleF rect, string label)
        {
            Label = label;
            Rect = rect;
        }
    }
}