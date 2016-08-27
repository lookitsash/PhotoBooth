namespace PhotoBooth
{
    partial class CameraForm
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
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.cameraPropertyValue = new System.Windows.Forms.ComboBox();
            this.cameraPropertyRangeValue = new System.Windows.Forms.Label();
            this.cameraPropertyValueTypeSelection = new System.Windows.Forms.ComboBox();
            this.cameraPropertyValueValue = new System.Windows.Forms.NumericUpDown();
            this.cameraPropertyValueAuto = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxDisplay.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.Size = new System.Drawing.Size(684, 566);
            this.pictureBoxDisplay.TabIndex = 13;
            this.pictureBoxDisplay.TabStop = false;
            this.pictureBoxDisplay.Click += new System.EventHandler(this.pictureBoxDisplay_Click);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(185, 515);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(153, 21);
            this.comboBoxCameras.TabIndex = 12;
            this.comboBoxCameras.Visible = false;
            // 
            // cameraPropertyValue
            // 
            this.cameraPropertyValue.Enabled = false;
            this.cameraPropertyValue.FormattingEnabled = true;
            this.cameraPropertyValue.Location = new System.Drawing.Point(64, 542);
            this.cameraPropertyValue.Name = "cameraPropertyValue";
            this.cameraPropertyValue.Size = new System.Drawing.Size(156, 21);
            this.cameraPropertyValue.TabIndex = 15;
            this.cameraPropertyValue.Visible = false;
            //this.cameraPropertyValue.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValue_SelectedIndexChanged);
            //this.cameraPropertyValue.EnabledChanged += new System.EventHandler(this.cameraPropertyValue_EnabledChanged);
            // 
            // cameraPropertyRangeValue
            // 
            this.cameraPropertyRangeValue.AutoSize = true;
            this.cameraPropertyRangeValue.Location = new System.Drawing.Point(344, 545);
            this.cameraPropertyRangeValue.Name = "cameraPropertyRangeValue";
            this.cameraPropertyRangeValue.Size = new System.Drawing.Size(45, 13);
            this.cameraPropertyRangeValue.TabIndex = 19;
            this.cameraPropertyRangeValue.Text = "<value>";
            this.cameraPropertyRangeValue.Visible = false;
            // 
            // cameraPropertyValueTypeSelection
            // 
            this.cameraPropertyValueTypeSelection.Enabled = false;
            this.cameraPropertyValueTypeSelection.FormattingEnabled = true;
            this.cameraPropertyValueTypeSelection.Items.AddRange(new object[] {
            "Value",
            "Percentage"});
            this.cameraPropertyValueTypeSelection.Location = new System.Drawing.Point(457, 542);
            this.cameraPropertyValueTypeSelection.Name = "cameraPropertyValueTypeSelection";
            this.cameraPropertyValueTypeSelection.Size = new System.Drawing.Size(55, 21);
            this.cameraPropertyValueTypeSelection.TabIndex = 20;
            this.cameraPropertyValueTypeSelection.Visible = false;
            //this.cameraPropertyValueTypeSelection.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValueTypeSelection_SelectedIndexChanged);
            // 
            // cameraPropertyValueValue
            // 
            this.cameraPropertyValueValue.Enabled = false;
            this.cameraPropertyValueValue.Location = new System.Drawing.Point(518, 543);
            this.cameraPropertyValueValue.Name = "cameraPropertyValueValue";
            this.cameraPropertyValueValue.Size = new System.Drawing.Size(61, 20);
            this.cameraPropertyValueValue.TabIndex = 21;
            this.cameraPropertyValueValue.Visible = false;
            //this.cameraPropertyValueValue.ValueChanged += new System.EventHandler(this.cameraPropertyValueValue_ValueChanged);
            //this.cameraPropertyValueValue.EnabledChanged += new System.EventHandler(this.cameraPropertyValueValue_EnabledChanged);
            // 
            // cameraPropertyValueAuto
            // 
            this.cameraPropertyValueAuto.AutoSize = true;
            this.cameraPropertyValueAuto.Enabled = false;
            this.cameraPropertyValueAuto.Location = new System.Drawing.Point(585, 544);
            this.cameraPropertyValueAuto.Name = "cameraPropertyValueAuto";
            this.cameraPropertyValueAuto.Size = new System.Drawing.Size(54, 17);
            this.cameraPropertyValueAuto.TabIndex = 22;
            this.cameraPropertyValueAuto.Text = "Auto?";
            this.cameraPropertyValueAuto.UseVisualStyleBackColor = true;
            this.cameraPropertyValueAuto.Visible = false;
            //this.cameraPropertyValueAuto.CheckedChanged += new System.EventHandler(this.cameraPropertyValueAuto_CheckedChanged);
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 590);
            this.Controls.Add(this.cameraPropertyRangeValue);
            this.Controls.Add(this.cameraPropertyValueAuto);
            this.Controls.Add(this.cameraPropertyValueValue);
            this.Controls.Add(this.cameraPropertyValueTypeSelection);
            this.Controls.Add(this.cameraPropertyValue);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.pictureBoxDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 520);
            this.Name = "MainForm";
            this.Text = "Photo Booth";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CameraForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CameraForm_FormClosed);
            this.Load += new System.EventHandler(this.CameraForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CameraForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CameraForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CameraForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.ComboBox cameraPropertyValue;
        private System.Windows.Forms.ComboBox cameraPropertyValueTypeSelection;
        private System.Windows.Forms.NumericUpDown cameraPropertyValueValue;
        private System.Windows.Forms.CheckBox cameraPropertyValueAuto;
        private System.Windows.Forms.Label cameraPropertyRangeValue;
    }
}