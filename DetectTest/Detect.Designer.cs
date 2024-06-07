
namespace DetectTest
{
    partial class Detect
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detect));
            btnDetect = new System.Windows.Forms.Button();
            btnUpload = new System.Windows.Forms.Button();
            pBoxResult1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)pBoxResult1).BeginInit();
            SuspendLayout();
            // 
            // btnDetect
            // 
            btnDetect.Location = new System.Drawing.Point(623, 11);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new System.Drawing.Size(107, 39);
            btnDetect.TabIndex = 3;
            btnDetect.Text = "Detect";
            btnDetect.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            btnUpload.Location = new System.Drawing.Point(510, 11);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new System.Drawing.Size(107, 39);
            btnUpload.TabIndex = 4;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            // 
            // pBoxResult1
            // 
            pBoxResult1.Image = (System.Drawing.Image)resources.GetObject("pBoxResult1.Image");
            pBoxResult1.Location = new System.Drawing.Point(12, 56);
            pBoxResult1.Name = "pBoxResult1";
            pBoxResult1.Size = new System.Drawing.Size(718, 480);
            pBoxResult1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pBoxResult1.TabIndex = 0;
            pBoxResult1.TabStop = false;
            // 
            // Detect
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(742, 548);
            Controls.Add(btnUpload);
            Controls.Add(btnDetect);
            Controls.Add(pBoxResult1);
            Name = "Detect";
            Text = "AI Detect";
            ((System.ComponentModel.ISupportInitialize)pBoxResult1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btnDetect;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.PictureBox pBoxResult1;
    }
}

