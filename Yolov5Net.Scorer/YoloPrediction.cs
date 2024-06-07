using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Yolov8Net.Scorer
{
    public class YoloPrediction
    {
        public YoloLabel Label { get; set; }
        public RectangleF Rectangle { get; set; }
        public float Score { get; set; }

        public YoloPrediction() { }

        public YoloPrediction(YoloLabel label, float confidence) : this(label)
        {
            Score = confidence;
        }

        public YoloPrediction(YoloLabel label)
        {
            Label = label;
        }
    }
}
