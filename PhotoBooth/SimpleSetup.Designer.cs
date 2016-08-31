namespace PhotoBooth
{
    partial class SimpleSetup
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleSetup));
            this.label6 = new System.Windows.Forms.Label();
            this.BTN_Start = new System.Windows.Forms.Button();
            this.BTN_ViewImages = new System.Windows.Forms.Button();
            this.BTN_Advanced = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(123, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(344, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Thanks for using our free photo booth software!";
            // 
            // BTN_Start
            // 
            this.BTN_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Start.ForeColor = System.Drawing.Color.ForestGreen;
            this.BTN_Start.Image = ((System.Drawing.Image)(resources.GetObject("BTN_Start.Image")));
            this.BTN_Start.Location = new System.Drawing.Point(59, 12);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(135, 135);
            this.BTN_Start.TabIndex = 19;
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            // 
            // BTN_ViewImages
            // 
            this.BTN_ViewImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_ViewImages.ForeColor = System.Drawing.Color.ForestGreen;
            this.BTN_ViewImages.Image = ((System.Drawing.Image)(resources.GetObject("BTN_ViewImages.Image")));
            this.BTN_ViewImages.Location = new System.Drawing.Point(224, 12);
            this.BTN_ViewImages.Name = "BTN_ViewImages";
            this.BTN_ViewImages.Size = new System.Drawing.Size(135, 135);
            this.BTN_ViewImages.TabIndex = 20;
            this.BTN_ViewImages.UseVisualStyleBackColor = true;
            this.BTN_ViewImages.Click += new System.EventHandler(this.BTN_ViewImages_Click);
            // 
            // BTN_Advanced
            // 
            this.BTN_Advanced.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Advanced.ForeColor = System.Drawing.Color.ForestGreen;
            this.BTN_Advanced.Image = ((System.Drawing.Image)(resources.GetObject("BTN_Advanced.Image")));
            this.BTN_Advanced.Location = new System.Drawing.Point(393, 12);
            this.BTN_Advanced.Name = "BTN_Advanced";
            this.BTN_Advanced.Size = new System.Drawing.Size(135, 135);
            this.BTN_Advanced.TabIndex = 21;
            this.BTN_Advanced.UseVisualStyleBackColor = true;
            this.BTN_Advanced.Click += new System.EventHandler(this.BTN_Advanced_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 211);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(57, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(476, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "Send any questions or comments to eileen@justmeasuringup.com";
            // 
            // SimpleSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(590, 314);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BTN_Advanced);
            this.Controls.Add(this.BTN_ViewImages);
            this.Controls.Add(this.BTN_Start);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SimpleSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Easy Photo Booth - www.justmeasuringup.com";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleSetup_FormClosed);
            this.Load += new System.EventHandler(this.SimpleSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Button BTN_ViewImages;
        private System.Windows.Forms.Button BTN_Advanced;
        private System.Windows.Forms.Label label1;
    }
}