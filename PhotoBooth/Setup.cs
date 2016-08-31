using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using dshow;
using dshow.Core;
using System.Threading;
using System.Net;

namespace PhotoBooth
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private FilterCollection cameraFilters = null;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.justmeasuringup.com");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = TB_ImagePath.Text;
            if (fbd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                TB_ImagePath.Text = fbd.SelectedPath;
            }
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            try
            {
                TB_ImagePath.Text = Settings.ImagePath;
                TB_Overlays.Text = Settings.OverlaysPath;
                NUD_Rounds.Value = Settings.Rounds;
                NUD_SecondsToPreview.Value = Settings.SecondsToPreview;
                NUD_SecondsToWait.Value = Settings.SecondsToWait;
                CB_CycleFrames.Checked = Settings.CycleOverlays;

                try
                {
                    cameraFilters = new FilterCollection(FilterCategory.VideoInputDevice);
                }
                catch (Exception exc)
                {
                    Log("Could not retrieve camera devices", exc);
                }

                if (cameraFilters == null || cameraFilters.Count == 0)
                {
                    Setup.LogStat(StatTypes.Error, "No cameras found (AdvancedMode)");
                    DDL_VideoCamera.Items.Add("NO CAMERAS FOUND");
                    BTN_Start.Enabled = false;                    
                }
                else
                {
                    foreach (Filter filter in cameraFilters)
                    {
                        DDL_VideoCamera.Items.Add(filter.Name);
                    }
                }
                DDL_VideoCamera.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Log("Cannot load setup form", ex);
                Setup.LogStat(StatTypes.Error, "Setup_Load Exception");
                MessageBox.Show(this, "Could not start application!  Please send us your Log.txt file for troubleshooting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = TB_Overlays.Text;
            if (fbd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                TB_Overlays.Text = fbd.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Setup.LogStat(StatTypes.StartPhotoBoothAdvanced);
            try
            {
                string imagesPath = TB_ImagePath.Text.Trim();
                if (String.IsNullOrEmpty(imagesPath)) throw new Exception("missing imagespath");
                if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);

                string overlaysPath = TB_Overlays.Text.Trim();
                if (String.IsNullOrEmpty(overlaysPath)) throw new Exception("missing overlayspath");
                if (!Directory.Exists(overlaysPath)) Directory.CreateDirectory(overlaysPath);

                Settings.ImagePath = imagesPath;
                Settings.OverlaysPath = overlaysPath;
                Settings.CycleOverlays = CB_CycleFrames.Checked;
                Settings.Rounds = (int)NUD_Rounds.Value;
                Settings.SecondsToPreview = (int)NUD_SecondsToPreview.Value;
                Settings.SecondsToWait = (int)NUD_SecondsToWait.Value;

                MessageBox.Show(this, "Photo Booth will now start.  Press the 'Q' key to leave at any time.", "About to start...", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Log("Starting photo booth from advanced mode:\r\n\tImagePath: " + Settings.ImagePath+"\r\n\tOverlaysPath: " + Settings.OverlaysPath + "\r\n\tRounds: " + Settings.Rounds + "\r\n\tSecondsToWait: " + Settings.SecondsToWait + "\r\n\tSecondsToPreview: " + Settings.SecondsToPreview);

                this.Hide();
                CameraForm.Instance.ReloadSettings();
                CameraForm.Instance.StartCamera(cameraFilters[DDL_VideoCamera.SelectedIndex].MonikerString);
                CameraForm.Instance.Show();
            }
            catch (Exception ex)
            {
                Log("cannot start", ex);
                Setup.LogStat(StatTypes.Error, "StartPhotoBooth (Advanced) Exception");
                MessageBox.Show(this, "Please double check your settings!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static void Log(string msg) { Log(msg, null);  }

        public static void Log(string msg, Exception ex)
        {
            try
            {
                string line = DateTime.Now.ToString() + " - " + msg;
                if (ex != null) line += "\t" + ex.ToString();
                string logFile = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
                line += "\r\n";
                File.AppendAllText(logFile, line);
            }
            catch
            {

            }
        }

        public static void LogStat(StatTypes statType) { LogStat(statType, null); }
        public static void LogStat(StatTypes statType, string data)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    WebClient wc = new WebClient();
                    string url = "http://my.justmeasuringup.com/Software.aspx?ID=1&Action=" + (int)statType + "&Data=" + Uri.EscapeUriString(data ?? "");
                    string logFile = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
                    if (statType == StatTypes.Error && File.Exists(logFile)) wc.UploadFile(url, logFile);
                    else wc.DownloadString(url);
                    
                }
                catch (Exception ex)
                {

                }
            });
            t.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string imagesPath = TB_ImagePath.Text.Trim();
            try
            {
                if (!String.IsNullOrEmpty(imagesPath))
                {
                    if (!Directory.Exists(imagesPath)) Directory.CreateDirectory(imagesPath);
                    Process.Start(imagesPath);
                }
            }
            catch (Exception ex)
            {
                Log("Could not open imagesPath", ex);
                Setup.LogStat(StatTypes.Error, "ViewImages (Advanced) Exception");
                MessageBox.Show(this, "Please double check your settings!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string overlaysPath = TB_Overlays.Text.Trim();
            try
            {
                if (!String.IsNullOrEmpty(overlaysPath))
                {
                    if (!Directory.Exists(overlaysPath)) Directory.CreateDirectory(overlaysPath);
                    Process.Start(overlaysPath);
                }
            }
            catch (Exception ex)
            {
                Log("Could not open overlaysPath", ex);
                MessageBox.Show(this, "Please double check your settings!", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BTN_Back_Click(object sender, EventArgs e)
        {
            Setup.LogStat(StatTypes.BackToSimpleMode);
            this.Hide();
            CameraForm.Instance.LastSetupForm = CameraForm.Instance.SimpleSetupForm;
            CameraForm.Instance.SimpleSetupForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Setup.LogStat(StatTypes.WebsiteClick);
            System.Diagnostics.Process.Start("http://www.justmeasuringup.com");
        }
    }

    public enum StatTypes
    {
        Launch = 2,
        StartPhotoBoothSimple = 3,
        ViewImagesSimple = 4,
        AdvancedMode = 5,
        BackToSimpleMode = 6,
        StartPhotoBoothAdvanced = 7,
        WebsiteClick = 8,
        TakingPictures = 9,
        Error = 999
    }
}
