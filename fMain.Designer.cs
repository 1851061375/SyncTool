using System.Drawing;
using System.Windows.Forms;

namespace SyncToolOld
{
    partial class fMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            pictureOrigin = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            PictureResize = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureOrigin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PictureResize).BeginInit();
            SuspendLayout();
            // 
            // pictureOrigin
            // 
            pictureOrigin.Location = new Point(56, 12);
            pictureOrigin.Name = "pictureOrigin";
            pictureOrigin.Size = new Size(544, 361);
            pictureOrigin.TabIndex = 0;
            pictureOrigin.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 15);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 1;
            label1.Text = "Origin:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(623, 17);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 2;
            label2.Text = "Resize:";
            // 
            // PictureResize
            // 
            PictureResize.Location = new Point(699, 17);
            PictureResize.Name = "PictureResize";
            PictureResize.Size = new Size(280, 356);
            PictureResize.TabIndex = 3;
            PictureResize.TabStop = false;
            // 
            // fMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 420);
            Controls.Add(PictureResize);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureOrigin);
            Name = "fMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SyncTool";
            ((System.ComponentModel.ISupportInitialize)pictureOrigin).EndInit();
            ((System.ComponentModel.ISupportInitialize)PictureResize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureOrigin;
        private Label label1;
        private Label label2;
        private PictureBox PictureResize;
    }
}

