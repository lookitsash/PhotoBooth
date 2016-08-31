namespace PhotoBooth
{
    partial class Setup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.BTN_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_ImagePath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Overlays = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.NUD_Rounds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NUD_SecondsToWait = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.NUD_SecondsToPreview = new System.Windows.Forms.NumericUpDown();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.CB_CycleFrames = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.DDL_VideoCamera = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BTN_Back = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Rounds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SecondsToWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SecondsToPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BTN_Start
            // 
            this.BTN_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTN_Start.ForeColor = System.Drawing.Color.ForestGreen;
            this.BTN_Start.Location = new System.Drawing.Point(433, 173);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(135, 111);
            this.BTN_Start.TabIndex = 1;
            this.BTN_Start.Text = "Start Photo Booth";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Saved Images";
            // 
            // TB_ImagePath
            // 
            this.TB_ImagePath.Location = new System.Drawing.Point(88, 79);
            this.TB_ImagePath.Name = "TB_ImagePath";
            this.TB_ImagePath.Size = new System.Drawing.Size(335, 20);
            this.TB_ImagePath.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 78);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Overlays";
            // 
            // TB_Overlays
            // 
            this.TB_Overlays.Location = new System.Drawing.Point(89, 109);
            this.TB_Overlays.Name = "TB_Overlays";
            this.TB_Overlays.Size = new System.Drawing.Size(334, 20);
            this.TB_Overlays.TabIndex = 6;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(429, 107);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Browse...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // NUD_Rounds
            // 
            this.NUD_Rounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUD_Rounds.Location = new System.Drawing.Point(287, 173);
            this.NUD_Rounds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_Rounds.Name = "NUD_Rounds";
            this.NUD_Rounds.Size = new System.Drawing.Size(120, 30);
            this.NUD_Rounds.TabIndex = 8;
            this.NUD_Rounds.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(232, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Seconds in between each snapshot";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(153, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Snapshots per reel";
            // 
            // NUD_SecondsToWait
            // 
            this.NUD_SecondsToWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUD_SecondsToWait.Location = new System.Drawing.Point(287, 214);
            this.NUD_SecondsToWait.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_SecondsToWait.Name = "NUD_SecondsToWait";
            this.NUD_SecondsToWait.Size = new System.Drawing.Size(120, 30);
            this.NUD_SecondsToWait.TabIndex = 11;
            this.NUD_SecondsToWait.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(47, 260);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Seconds to show completed reel for";
            // 
            // NUD_SecondsToPreview
            // 
            this.NUD_SecondsToPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NUD_SecondsToPreview.Location = new System.Drawing.Point(287, 254);
            this.NUD_SecondsToPreview.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUD_SecondsToPreview.Name = "NUD_SecondsToPreview";
            this.NUD_SecondsToPreview.Size = new System.Drawing.Size(120, 30);
            this.NUD_SecondsToPreview.TabIndex = 13;
            this.NUD_SecondsToPreview.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CB_CycleFrames
            // 
            this.CB_CycleFrames.AutoSize = true;
            this.CB_CycleFrames.Location = new System.Drawing.Point(89, 143);
            this.CB_CycleFrames.Name = "CB_CycleFrames";
            this.CB_CycleFrames.Size = new System.Drawing.Size(221, 17);
            this.CB_CycleFrames.TabIndex = 16;
            this.CB_CycleFrames.Text = "Cycle through overlays on each snapshot";
            this.CB_CycleFrames.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(509, 78);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "Open";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(509, 107);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "Open";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // DDL_VideoCamera
            // 
            this.DDL_VideoCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DDL_VideoCamera.FormattingEnabled = true;
            this.DDL_VideoCamera.Location = new System.Drawing.Point(89, 50);
            this.DDL_VideoCamera.Name = "DDL_VideoCamera";
            this.DDL_VideoCamera.Size = new System.Drawing.Size(334, 21);
            this.DDL_VideoCamera.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Video Camera";
            // 
            // BTN_Back
            // 
            this.BTN_Back.Location = new System.Drawing.Point(7, 9);
            this.BTN_Back.Name = "BTN_Back";
            this.BTN_Back.Size = new System.Drawing.Size(152, 23);
            this.BTN_Back.TabIndex = 21;
            this.BTN_Back.Text = "<- Back to Simple Mode";
            this.BTN_Back.UseVisualStyleBackColor = true;
            this.BTN_Back.Click += new System.EventHandler(this.BTN_Back_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-8, 296);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(606, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(590, 399);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BTN_Back);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DDL_VideoCamera);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.CB_CycleFrames);
            this.Controls.Add(this.NUD_SecondsToPreview);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NUD_SecondsToWait);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NUD_Rounds);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TB_Overlays);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.TB_ImagePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_Start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Easy Photo Booth - www.justmeasuringup.com";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Setup_FormClosed);
            this.Load += new System.EventHandler(this.Setup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NUD_Rounds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SecondsToWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUD_SecondsToPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_ImagePath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Overlays;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown NUD_Rounds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown NUD_SecondsToWait;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown NUD_SecondsToPreview;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox CB_CycleFrames;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox DDL_VideoCamera;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BTN_Back;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}