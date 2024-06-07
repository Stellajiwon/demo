using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Yolov8Net.Scorer;
using Yolov8Net.Scorer.Models;

namespace DetectTest
{
    public partial class Detect : Form
    {
        private YoloScorer<YoloCocoP5Model> scorer = new YoloScorer<YoloCocoP5Model>("Assets/Weights/best.onnx");

        private Image originalImage;
        private Image lastDetectedImage;

        public Detect()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            btnUpload.Click += btnUpload_Click;
            btnDetect.Click += btnDetect_Click;
            pBoxResult1.Click += pBoxResult_Click;

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "이미지 파일 (*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = Image.FromFile(openFileDialog.FileName);
                pBoxResult1.Image = (Image)originalImage.Clone();
               
                CheckDetectButtonStatus();

            }
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            try
            {
                Image image1 = (Image)pBoxResult1.Image.Clone();

                if (image1 == null )
                {
                    MessageBox.Show("이미지를 불러오지 못했습니다.");
                    return;
                }
                
                List<YoloPrediction> predictions1 = scorer.Predict(image1);
                
                this.Invoke((MethodInvoker)delegate
                {
                    DisplayPredictions(pBoxResult1, image1, predictions1);
                  

                    lastDetectedImage = (Image)pBoxResult1.Image.Clone();
                    CheckDetectButtonStatus();

                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
        }

        private void CheckDetectButtonStatus()
        {
            btnDetect.Enabled = (lastDetectedImage == null || !ImageCompare(lastDetectedImage, pBoxResult1.Image));
        }

        private bool ImageCompare(Image firstImage, Image secondImage)
        {
            using var ms1 = new MemoryStream();
            using var ms2 = new MemoryStream();
            firstImage.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
            secondImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
            return ms1.ToArray().SequenceEqual(ms2.ToArray());
        }

        private void DisplayPredictions(PictureBox pBox, Image image, List<YoloPrediction> predictions)
        {
            Image scaledImage = new Bitmap(image, pBox.Size);

            using var graphics = Graphics.FromImage(scaledImage);
            float xScale = (float)pBox.Width / image.Width;
            float yScale = (float)pBox.Height / image.Height;

            foreach (var prediction in predictions)
            {
                if (prediction.Score >= 0.3)
                {
                    double score = Math.Round(prediction.Score, 2);

                    var scaledRectangle = new Rectangle(
                        (int)(prediction.Rectangle.X * xScale),
                        (int)(prediction.Rectangle.Y * yScale),
                        (int)(prediction.Rectangle.Width * xScale),
                        (int)(prediction.Rectangle.Height * yScale));

                    if (scaledRectangle.Right > scaledImage.Width)
                    {
                        scaledRectangle.Width -= scaledRectangle.Right - scaledImage.Width;
                    }
                    if (scaledRectangle.Bottom > scaledImage.Height)
                    {
                        scaledRectangle.Height -= scaledRectangle.Bottom - scaledImage.Height;
                    }

                    graphics.DrawRectangle(new Pen(prediction.Label.Color, 3), scaledRectangle);

                    if (prediction.Label.Name != "number")
                    {
                        var (x, y) = (scaledRectangle.X - 3, scaledRectangle.Y - 23);

                        if (x < 0) x = 0;
                        if (y < 0) y = 0;

                        y += 8;
                        graphics.DrawString($"{prediction.Label.Name}({score})",
                            new Font("Consolas", 12, GraphicsUnit.Pixel), new SolidBrush(prediction.Label.Color),
                            new PointF(x, y));
                    }
                }
            }

            pBox.Image = scaledImage;
        }



        private void pBoxResult_Click(object sender, EventArgs e)
        {
            Form preview = new Form();

            preview.FormBorderStyle = FormBorderStyle.FixedSingle;
            preview.MaximizeBox = false;

            Panel panel = new Panel();
            PictureBox pBoxPreview = new PictureBox();
            TrackBar trackBar = new TrackBar();
            Label label = new Label();

            panel.Dock = DockStyle.Fill;
            panel.AutoScroll = true;

            pBoxPreview.Dock = DockStyle.None;
            pBoxPreview.SizeMode = PictureBoxSizeMode.Normal;

            Image originalImage = new Bitmap(((PictureBox)sender).Image);

            trackBar.Orientation = Orientation.Horizontal;
            trackBar.BackColor = Color.DarkGray;

            trackBar.Dock = DockStyle.Bottom;
            trackBar.TickFrequency = 20;

            float initialScale = Math.Min(1920f / originalImage.Width, (1080f - trackBar.Height) / originalImage.Height);
            trackBar.Minimum = (int)(initialScale * 100);
            trackBar.Maximum = 300;
            trackBar.Value = trackBar.Minimum;

            pBoxPreview.Image = new Bitmap(originalImage, new Size((int)(originalImage.Width * initialScale), (int)(originalImage.Height * initialScale)));
            pBoxPreview.Size = pBoxPreview.Image.Size;

            panel.Controls.Add(pBoxPreview);

            trackBar.Scroll += new EventHandler((o, ea) =>
            {
                float scale = trackBar.Value / 100f;
                pBoxPreview.Image.Dispose();
                pBoxPreview.Image = new Bitmap(originalImage, new Size((int)(originalImage.Width * scale), (int)(originalImage.Height * scale)));
                pBoxPreview.Size = pBoxPreview.Image.Size;

                pBoxPreview.Location = new Point(Math.Max((panel.Width - pBoxPreview.Width) / 2, 0), Math.Max((panel.Height - pBoxPreview.Height) / 2, 0));
                label.Text = (trackBar.Value / 100f).ToString() + "배";
            });

            label.Dock = DockStyle.Top;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Text = (trackBar.Value / 100f).ToString() + "배";
            preview.Controls.Add(label);

            preview.Controls.Add(panel);
            preview.Controls.Add(trackBar);
            preview.ClientSize = new Size(1920, 1080);
            preview.Text = "Preview";

            preview.Load += new EventHandler((o, ea) =>
            {
                pBoxPreview.Location = new Point((panel.Width - pBoxPreview.Width) / 2, (panel.Height - pBoxPreview.Height) / 2);
            });

            preview.ShowDialog();
        }
    }
}