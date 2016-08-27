using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using dshow;
using dshow.Core;
using System.Diagnostics;

namespace PhotoBooth
{
    public partial class SimpleSetup : Form
    {
        public SimpleSetup()
        {
            InitializeComponent();
        }

        private FilterCollection cameraFilters = null;

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.justmeasuringup.com");
        }

        private void SimpleSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.justmeasuringup.com");
        }

        private void BTN_Start_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Photo Booth will now start.  Press the 'Q' key to leave at any time.", "About to start...", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Setup.Log("Starting photo booth from simple mode:\r\n\tImagePath: " + Settings.ImagePath + "\r\n\tOverlaysPath: " + Settings.OverlaysPath + "\r\n\tRounds: " + Settings.Rounds + "\r\n\tSecondsToWait: " + Settings.SecondsToWait + "\r\n\tSecondsToPreview: " + Settings.SecondsToPreview);

            this.Hide();
            CameraForm.Instance.ReloadSettings();
            CameraForm.Instance.StartCamera(cameraFilters[0].MonikerString);
            CameraForm.Instance.Show();
        }

        private void BTN_ViewImages_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.ImagePath);
        }

        private void BTN_Advanced_Click(object sender, EventArgs e)
        {
            this.Hide();
            CameraForm.Instance.LastSetupForm = CameraForm.Instance.SetupForm;
            CameraForm.Instance.SetupForm.Show();
        }

        private void SimpleSetup_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    cameraFilters = new FilterCollection(FilterCategory.VideoInputDevice);
                }
                catch (Exception exc)
                {
                    Setup.Log("Could not retrieve camera devices", exc);
                }

                if (cameraFilters == null || cameraFilters.Count == 0)
                {
                    MessageBox.Show(this, "Sorry!  We could not detect a web camera.  Are you sure it's plugged in?\n\nDouble check your web camera settings and try \"Easy Photo Booth\" again.", "No web camera detected", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                Setup.Log("Cannot load simple setup form", ex);
                MessageBox.Show(this, "Could not start application!  Please send us your Log.txt file for troubleshooting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
