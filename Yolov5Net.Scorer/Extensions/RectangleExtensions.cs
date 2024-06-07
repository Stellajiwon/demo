using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Yolov8Net.Scorer.Extensions
{
    public static class RectangleExtensions
    {
        public static float Area(this RectangleF source)
        {
            return source.Width * source.Height;
        }
    }
}
