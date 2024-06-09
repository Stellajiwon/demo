using System;
using System.Drawing;

namespace Yolov8Net.Scorer
{
    public class YoloLabel
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                SetColor();
            }
        }
        public YoloLabelKind Kind { get; set; }
        public Color Color { get; set; }

        public YoloLabel()
        {
            
        }

        private void SetColor()
        {
            switch (_name)
            {
                case "smoke":
                    Color = Color.Yellow;
                    break;
                case "fire":
                    Color = Color.Blue;
                    break;
                default:
                    int hash = _name.GetHashCode();
                    Color = Color.FromArgb((hash & 0xFF0000) >> 16, (hash & 0x00FF00) >> 8, hash & 0x0000FF);
                    break;
            }
        }
    }
}
