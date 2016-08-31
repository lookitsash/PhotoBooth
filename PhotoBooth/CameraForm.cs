using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Configuration;
using System.IO;
using System.Media;

using dshow;
using dshow.Core;
using VideoSource;

namespace PhotoBooth
{
    public partial class CameraForm : Form
    {
        public CameraForm()
        {
            InitializeComponent();
        }

        public static CameraForm Instance = null;

        private static string ImagePath = null, SinglesImagePath = null, GroupImagePath = null;
        private string PreviewImage = null;
        private int SecondsToWait = 5, Rounds = 3;
        private bool CycleFrames = false;
        private List<Image> Frames = new List<Image>();
        private int CurrentFrameIndex = -1;
        private Image CurrentFrameImage = null;
        
        private Splash SplashForm = null;
        private Timer SplashTimer = null;
        public Setup SetupForm = null;
        public SimpleSetup SimpleSetupForm = null;

        private SoundPlayer SND_CameraClick;
        private SoundPlayer SND_Tick;
        private SoundPlayer SND_Smile;

        private string CameraDeviceID = null;
        private Camera CurrentCamera = null;

        public Form LastSetupForm = null;

        private void CameraForm_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);

                this.Hide();


                SplashForm = new Splash();
                SplashForm.Show();

                /*
                if (CameraService.AvailableCameras.Count == 0)
                {
                    Setup.Log("CameraForm_Load NO WEB CAMS FOUND");
                    MessageBox.Show(this, "Sorry!  We could not detect a web camera.  Are you sure it's plugged in?\n\nDouble check your web camera settings and try \"easy Photo Booth\" again.", "No web camera detected", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Exit();
                }
                */

                SplashTimer = new Timer();
                SplashTimer.Interval = 5000;
                SplashTimer.Tick += timer_Tick;
                SplashTimer.Start();

                //SetupForm = new Setup();
                //SetupForm.Show();

                SND_CameraClick = new SoundPlayer(Properties.Resources.CameraClick);
                SND_CameraClick.Load();
                SND_Tick = new SoundPlayer(Properties.Resources.Tick);
                SND_Tick.Load();
                SND_Smile = new SoundPlayer(Properties.Resources.FastTick);
                SND_Smile.Load();

                

                Setup.Log("Current directory: " + Directory.GetCurrentDirectory());

                /*
                if (!DesignMode)
                {
                    // Refresh the list of available cameras
                    comboBoxCameras.Items.Clear();
                    foreach (Camera cam in CameraService.AvailableCameras)
                        comboBoxCameras.Items.Add(cam);

                    if (comboBoxCameras.Items.Count > 0)
                        comboBoxCameras.SelectedIndex = 0;

                    ReloadSettings();

                    if (comboBoxCameras.Items.Count > 0)
                    {
                        CameraFound = true;

                        // start camera
                        if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                            return;

                        thrashOldCamera();
                        startCapturing();
                    }
                    else
                    {
                        pictureBoxDisplay.Visible = false;
                        _latestFrame = new Bitmap(pictureBoxDisplay.Width, pictureBoxDisplay.Height);
                    }
                }
                 * */
            }
            catch (Exception ex)
            {
                Setup.Log("CameraForm_Load Exception", ex);
                Setup.LogStat(StatTypes.Error, "CameraForm_Load Exception");
                MessageBox.Show(this, "Could not start application!  Please send us your Log.txt file for troubleshooting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        public void StartCamera(string cameraID)
        {
            try
            {
                BoothState = BoothStates.WaitingForCamera;
                _latestFrame = null;
                pictureBoxDisplay.Invalidate();

                StopCamera();

                CameraDeviceID = cameraID;

                // create video source
                CaptureDevice localSource = new CaptureDevice();
                localSource.VideoSource = CameraDeviceID;

                // open it
                OpenVideoSource(localSource);
            }
            catch (Exception ex)
            {
                Setup.Log("StartCamera Exception", ex);
                Setup.LogStat(StatTypes.Error, "StartCamera Exception");
                MessageBox.Show(this, "Could not start camera!  Please send us your Log.txt file for troubleshooting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void StopCamera()
        {
            if (CurrentCamera != null)
            {
                // signal camera to stop
                CurrentCamera.SignalToStop();
                // wait for the camera
                CurrentCamera.WaitForStop();
                CurrentCamera = null;
            }
        }

        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            //this.Cursor = Cursors.WaitCursor;

            // create camera
            CurrentCamera = new Camera(source);
            // start camera
            CurrentCamera.Start();

            // attach camera to camera window
            //cameraWindow.Camera = camera;

            // reset statistics
            //statIndex = statReady = 0;

            // set event handlers
            CurrentCamera.NewFrame += new EventHandler(camera_NewFrame);

            // start timer
            //timer.Start();

            this.Cursor = Cursors.Default;
        }

        // On new frame
        private void camera_NewFrame(object sender, System.EventArgs e)
        {
            CurrentCamera.Lock();
            _latestFrame = CurrentCamera.LastFrame;
            CurrentCamera.Unlock();

            if (BoothState == BoothStates.WaitingForCamera) BoothState = BoothStates.Idle;
            pictureBoxDisplay.Invalidate();
        }

        public void ReloadSettings()
        {
            try
            {
                ImagePath = Settings.ImagePath;
                if (!Directory.Exists(ImagePath)) Directory.CreateDirectory(ImagePath);
                SinglesImagePath = Path.Combine(ImagePath, "Individual");
                if (!Directory.Exists(SinglesImagePath)) Directory.CreateDirectory(SinglesImagePath);
                GroupImagePath = Path.Combine(ImagePath, "FilmStrip");
                if (!Directory.Exists(GroupImagePath)) Directory.CreateDirectory(GroupImagePath);

                Frames = new List<Image>();
                string framesPath = Settings.OverlaysPath;
                if (!Directory.Exists(framesPath)) Directory.CreateDirectory(framesPath);
                foreach (string framePath in Directory.GetFiles(framesPath, "*.png"))
                {
                    Frames.Add(Image.FromFile(framePath));
                }
                CycleFrames = Settings.CycleOverlays;

                SecondsToWait = Settings.SecondsToWait;
                Rounds = Settings.Rounds;

                BoothState = BoothStates.Idle;
                roundsRemaining = Rounds;
                secondsRemaining = SecondsToWait;
            }
            catch (Exception ex)
            {
                Setup.Log("ReloadSettings Exception", ex);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            SplashTimer.Stop();
            SplashTimer = null;
            SplashForm.Close();
            SplashForm = null;

            LastSetupForm = SimpleSetupForm = new SimpleSetup();
            SimpleSetupForm.Show();

            SetupForm = new Setup();
            //SetupForm.Show();
        }

        private void CameraForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thrashOldCamera();
            StopCamera();
        }

        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //thrashOldCamera();
        }

        //private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        /*
        private Camera CurrentCamera
        {
            get
            {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }
        */

        /*
        private void btnStart_Click(object sender, EventArgs e)
        {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void startCapturing()
        {
            try
            {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = 640;
                _frameSource.Camera.CaptureHeight = 480;
                _frameSource.Camera.Fps = 50;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBoxDisplay.Paint += new PaintEventHandler(drawLatestImage);
                cameraPropertyValue.Enabled = _frameSource.StartFrameCapture();
            }
            catch (Exception ex)
            {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }
        */
        private void Draw(Graphics g)
        {
            try
            {
                if (BoothState == BoothStates.WaitingForCamera)
                {
                    DrawText("Waiting for camera to start...", g, 32, Color.Green, FontPosition.Middle);
                    return;
                }

                if (_latestFrame == null) return;

                if (NextTickDate != null && NextTickDate.Value <= DateTime.Now)
                {
                    onTick();
                }
                // Draw the latest image from the active camera
                float width = _latestFrame.Width;
                float height = _latestFrame.Height;
                float ratio = height / width;
                float newWidth = pictureBoxDisplay.Width;
                float newHeight = newWidth * ratio;
                if (newHeight > pictureBoxDisplay.Height)
                {
                    float scale = (float)pictureBoxDisplay.Height / newHeight;
                    newHeight *= scale;
                    newWidth *= scale;
                }
                float xPos = (pictureBoxDisplay.Width - newWidth) / 2;
                float yPos = (pictureBoxDisplay.Height - newHeight) / 2;
                //e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
                //float ratio = (float)_latestFrame.Height / (float)_latestFrame.Width;
                g.DrawImage(_latestFrame, xPos, yPos, newWidth, newHeight);

                if (CurrentFrameImage != null)
                {
                    g.DrawImage(CurrentFrameImage, xPos, yPos, newWidth, newHeight);
                }

                //e.Graphics.DrawString("CLICK TO TAKE PICTURES", FontStyl
                if (BoothState == BoothStates.Idle)
                {
                    DrawText("LEFT CLICK TO TAKE PICTURES", g, 32, Color.Red, FontPosition.Middle);
                    if (Frames.Count > 0) DrawText("Right click for different overlays", g, 25, Color.Blue, FontPosition.Bottom);
                }
                else if (BoothState == BoothStates.GetReady) DrawText("GET READY...", g, 32, Color.Green, FontPosition.Middle);
                else if (BoothState == BoothStates.Countdown)
                {
                    DrawText(secondsRemaining.ToString(), g, 64, Color.Green, FontPosition.Top);
                    DrawText("Taking photo " + (Rounds - roundsRemaining + 1) + " of " + Rounds + "...", g, 25, Color.Blue, FontPosition.Middle);
                }
                else if (BoothState == BoothStates.TakePicture) DrawText("SMILE!", g, 64, Color.Yellow, FontPosition.Top);

                if (FlashEndDate != null)
                {
                    if (FlashEndDate.Value >= DateTime.Now)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), 0, 0, pictureBoxDisplay.Width, pictureBoxDisplay.Height);
                    }
                    else FlashEndDate = null;
                }
                else if (PreviewEndDate != null)
                {
                    if (PreviewEndDate.Value >= DateTime.Now && ImagesSaved != null && ImagesSaved.Count > 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(230, 200, 200, 200)), 0, 0, pictureBoxDisplay.Width, pictureBoxDisplay.Height);
                        Image img = Image.FromFile(PreviewImage);
                        width = img.Width;
                        height = img.Height;
                        ratio = height / width;
                        newWidth = pictureBoxDisplay.Width;
                        newHeight = newWidth * ratio;
                        xPos = 0;
                        yPos = (pictureBoxDisplay.Height - newHeight) / 2;
                        g.DrawImage(img, xPos, yPos, newWidth, newHeight);
                    }
                    else
                    {
                        PreviewEndDate = null;
                        BoothState = BoothStates.Idle;
                    }
                }

                if (!String.IsNullOrEmpty(LogMsg))
                {
                    if (LogMsgEndDate != null && LogMsgEndDate.Value >= DateTime.Now)
                    {
                        DrawText(LogMsg, g, 20, Color.Blue, FontPosition.Bottom);
                    }
                    else
                    {
                        LogMsg = null;
                        LogMsgEndDate = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Setup.Log("Draw Exception", ex);
            }
        }

        private void drawLatestImage(object sender, PaintEventArgs e)
        {
            if (_latestFrame != null)
            {
                Draw(e.Graphics);
            }
        }

        private void DrawText(string text, Graphics g, int fontSize, Color fontColor, FontPosition fontPosition)
        {
            // Create string to draw.
            String drawString = text;

            // Create font and brush.
            Font drawFont = new Font("Arial", fontSize, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(fontColor);
            SizeF fontDimensions = g.MeasureString(text, drawFont);

            float xPos = (pictureBoxDisplay.Width - fontDimensions.Width) / 2;
            float yPos = (pictureBoxDisplay.Height - fontDimensions.Height) / 2;
            if (fontPosition == FontPosition.Top) yPos = fontDimensions.Height + 40;
            else if (fontPosition == FontPosition.Bottom) yPos = pictureBoxDisplay.Height - fontDimensions.Height - 40;
            // Create point for upper-left corner of drawing.
            PointF drawPoint = new PointF(xPos, yPos);

            // Draw string to screen.
            g.FillRectangle(new SolidBrush(Color.FromArgb(180, 200, 200, 200)), xPos - 20, yPos - 20, fontDimensions.Width + 40, fontDimensions.Height + 40);
            g.DrawRectangle(Pens.Black, xPos - 20, yPos - 20, fontDimensions.Width + 40, fontDimensions.Height + 40);
            g.DrawString(drawString, drawFont, drawBrush, drawPoint);
        }

        /*
        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps)
        {
            _latestFrame = frame.Image;
            pictureBoxDisplay.Invalidate();
        }
        
        private void setFrameSource(CameraFrameSource cameraFrameSource)
        {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        //

        private void thrashOldCamera()
        {
            // Trash the old camera
            if (_frameSource != null)
            {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBoxDisplay.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        //

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_frameSource == null)
                    return;

                Bitmap current = (Bitmap)_latestFrame.Clone();
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "*.png|*.png";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        current.Save(sfd.FileName);
                    }
                }

                current.Dispose();
            }
            catch (Exception ex)
            {
                Setup.Log("SaveImage Exception", ex);
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            // snap camera
            if (_frameSource != null)
                _frameSource.Camera.ShowPropertiesDialog();
        }
        */

        /*
        #region Camera Property Controls
        private IDictionary<String, CameraProperty> displayPropertyValues;

        private IDictionary<String, CameraProperty> DisplayPropertyValues
        {
            get
            {
                if (displayPropertyValues == null)
                    displayPropertyValues = new Dictionary<String, CameraProperty>()
                 {
                    { "Pan (Degrees)", CameraProperty.Pan_degrees }, 
                    { "Tilt (Degrees)", CameraProperty.Tilt_degrees }, 
                    { "Roll (Degrees)", CameraProperty.Roll_degrees }, 
                    { "Zoom (mm)", CameraProperty.Zoom_mm }, 
                    { "Exposure (log2(seconds))", CameraProperty.Exposure_lgSec }, 
                    { "Iris (10f)", CameraProperty.Iris_10f }, 
                    { "Focal Length (mm)", CameraProperty.FocalLength_mm }, 
                    { "Flash", CameraProperty.Flash }, 
                    { "Brightness", CameraProperty.Brightness }, 
                    { "Contrast", CameraProperty.Contrast }, 
                    { "Hue", CameraProperty.Hue }, 
                    { "Saturation", CameraProperty.Saturation }, 
                    { "Sharpness", CameraProperty.Sharpness }, 
                    { "Gamma", CameraProperty.Gamma }, 
                    { "Color Enable", CameraProperty.ColorEnable }, 
                    { "White Balance", CameraProperty.WhiteBalance }, 
                    { "Backlight Compensation", CameraProperty.BacklightCompensation }, 
                    { "Gain", CameraProperty.Gain }, 
                 };

                return displayPropertyValues;
            }
        }

        private IDictionary<CameraProperty, CameraPropertyCapabilities> CurrentCameraPropertyCapabilities
        {
            get;
            set;
        }

        private IDictionary<CameraProperty, CameraPropertyRange> CurrentCameraPropertyRanges
        {
            get;
            set;
        }

        private CameraProperty SelectedCameraProperty
        {
            get
            {
                Int32 selectedIndex = cameraPropertyValue.SelectedIndex;
                String selectedItem = cameraPropertyValue.Items[selectedIndex] as String;

                CameraProperty result = DisplayPropertyValues[selectedItem];
                return result;
            }
        }

        private Boolean IsSelectedCameraPropertySupported
        {
            get;
            set;
        }

        private Boolean IsCameraPropertyValueTypeValue
        {
            get
            {
                return ((String)cameraPropertyValueTypeSelection.SelectedItem) == "Value";
            }
        }

        private Boolean IsCameraPropertyValueTypePercentage
        {
            get
            {
                return ((String)cameraPropertyValueTypeSelection.SelectedItem) == "Percentage";
            }
        }

        private Int32 CameraPropertyValue
        {
            get
            {
                Decimal value = cameraPropertyValueValue.Value;

                Int32 result;
                if (IsCameraPropertyValueTypeValue || IsCameraPropertyValueTypePercentage)
                {
                    value = Math.Round(value);

                    result = Convert.ToInt32(value);
                }
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                return result;
            }
        }

        private Boolean IsCameraPropertyAuto
        {
            get
            {
                return cameraPropertyValueAuto.Checked;
            }
        }

        private Boolean SuppressCameraPropertyValueValueChangedEvent
        {
            get;
            set;
        }

        private Boolean CameraPropertyControlInitializationComplete
        {
            get;
            set;
        }

        private void InitializeCameraPropertyControls()
        {
            CameraPropertyControlInitializationComplete = false;

            CurrentCameraPropertyCapabilities = CurrentCamera.CameraPropertyCapabilities;
            CurrentCameraPropertyRanges = new Dictionary<CameraProperty, CameraPropertyRange>();

            cameraPropertyValueTypeSelection.SelectedIndex = 0;

            cameraPropertyValue.Items.Clear();
            cameraPropertyValue.Items.AddRange(DisplayPropertyValues.Keys.ToArray());

            CameraPropertyControlInitializationComplete = true;

            cameraPropertyValue.SelectedIndex = 0;
        }

        private void UpdateCameraPropertyRange(CameraPropertyCapabilities propertyCapabilities)
        {
            String text;
            if (IsSelectedCameraPropertySupported && propertyCapabilities.IsGetRangeSupported && propertyCapabilities.IsGetSupported)
            {
                CameraPropertyRange range = CurrentCamera.GetCameraPropertyRange(SelectedCameraProperty);
                text = String.Format("[ {0}, {1} ], step: {2}", range.Minimum, range.Maximum, range.Step);

                Int32 decimalPlaces;
                Decimal minimum, maximum, increment;
                if (IsCameraPropertyValueTypeValue)
                {
                    minimum = range.Minimum;
                    maximum = range.Maximum;
                    increment = range.Step;
                    decimalPlaces = 0;
                }
                else if (IsCameraPropertyValueTypePercentage)
                {
                    minimum = 0;
                    maximum = 100;
                    increment = 0.01M;
                    decimalPlaces = 2;
                }
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                cameraPropertyValueValue.Minimum = minimum;
                cameraPropertyValueValue.Maximum = maximum;
                cameraPropertyValueValue.Increment = increment;
                cameraPropertyValueValue.DecimalPlaces = decimalPlaces;

                if (CurrentCameraPropertyRanges.ContainsKey(SelectedCameraProperty))
                    CurrentCameraPropertyRanges[SelectedCameraProperty] = range;
                else
                    CurrentCameraPropertyRanges.Add(SelectedCameraProperty, range);

                CameraPropertyValue value = CurrentCamera.GetCameraProperty(SelectedCameraProperty, IsCameraPropertyValueTypeValue);

                SuppressCameraPropertyValueValueChangedEvent = true;
                cameraPropertyValueValue.Value = value.Value;
                cameraPropertyValueAuto.Checked = value.IsAuto;
                SuppressCameraPropertyValueValueChangedEvent = false;
            }
            else
                text = "N/A";

            cameraPropertyRangeValue.Text = text;
        }

        private void cameraPropertyValueTypeSelection_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                CameraPropertyCapabilities propertyCapabilities = CurrentCameraPropertyCapabilities[SelectedCameraProperty];

                CameraPropertyRange range = CurrentCameraPropertyRanges[SelectedCameraProperty];

                Decimal previousValue = cameraPropertyValueValue.Value;

                UpdateCameraPropertyRange(propertyCapabilities);

                Decimal newValue;
                if (IsCameraPropertyValueTypeValue) // The previous value was a percentage.
                    newValue = range.DomainSize * previousValue / 100 + range.Minimum;
                else if (IsCameraPropertyValueTypePercentage) // The previous value was a value.
                    newValue = (previousValue - range.Minimum) * 100 / range.DomainSize;
                else
                    throw new NotSupportedException(String.Format("Camera property value type '{0}' is not supported.", (String)cameraPropertyValueTypeSelection.SelectedItem));

                newValue = Math.Round(newValue);

                if (newValue > range.Maximum)
                    newValue = range.Maximum;
                else if (newValue < range.Minimum)
                    newValue = range.Minimum;

                SuppressCameraPropertyValueValueChangedEvent = true;
                cameraPropertyValueValue.Value = newValue;
                SuppressCameraPropertyValueValueChangedEvent = false;
            }
        }

        private void cameraPropertyValueValue_ValueChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete && !SuppressCameraPropertyValueValueChangedEvent)
            {
                CameraPropertyValue value = new CameraPropertyValue(IsCameraPropertyValueTypePercentage, CameraPropertyValue, IsCameraPropertyAuto);
                CurrentCamera.SetCameraProperty(SelectedCameraProperty, value);
            }
        }

        private void cameraPropertyValueAuto_CheckedChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                CameraPropertyValue value = new CameraPropertyValue(IsCameraPropertyValueTypePercentage, CameraPropertyValue, IsCameraPropertyAuto);
                CurrentCamera.SetCameraProperty(SelectedCameraProperty, value);
            }
        }

        private void cameraPropertyValue_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete)
            {
                IsSelectedCameraPropertySupported = CurrentCamera.IsCameraPropertySupported(SelectedCameraProperty);
                CameraPropertyCapabilities propertyCapabilities = CurrentCameraPropertyCapabilities[SelectedCameraProperty];

                UpdateCameraPropertyRange(propertyCapabilities);

                cameraPropertyValueAuto.Enabled = cameraPropertyValueValue.Enabled = cameraPropertyValueTypeSelection.Enabled = IsSelectedCameraPropertySupported && propertyCapabilities.IsFullySupported;
            }
        }

        private void cameraPropertyValueValue_EnabledChanged(Object sender, EventArgs e)
        {
            if (CameraPropertyControlInitializationComplete && !SuppressCameraPropertyValueValueChangedEvent && cameraPropertyValueValue.Enabled)
            {
                CameraPropertyValue value = CurrentCamera.GetCameraProperty(SelectedCameraProperty, IsCameraPropertyValueTypeValue);
                cameraPropertyValueValue.Value = value.Value;
                cameraPropertyValueAuto.Checked = value.IsAuto;
            }
        }

        private void cameraPropertyValue_EnabledChanged(Object sender, EventArgs e)
        {
            if (cameraPropertyValue.Enabled)
                InitializeCameraPropertyControls();
        }
        #endregion
        */

        private BoothStates BoothState = BoothStates.Idle;
        private int secondsRemaining = 0;
        private int roundsRemaining = 0;
        private DateTime? FlashEndDate = null, PreviewEndDate = null, LogMsgEndDate = null, NextTickDate = null;
        private List<string> ImagesSaved = null;
        private string LogMsg;

        private void Log(string msg)
        {
            LogMsg = msg;
            LogMsgEndDate = DateTime.Now.AddSeconds(2);
            Setup.Log(msg);
        }

        private void pictureBoxDisplay_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == System.Windows.Forms.MouseButtons.Left && BoothState == BoothStates.Idle)
            {
                Setup.LogStat(StatTypes.TakingPictures);
                if (CycleFrames) NextFrame(false);
                BoothState = BoothStates.GetReady;
                //drawLatestImage(null, new PaintEventArgs(this.Get
                //this.Repa
                secondsRemaining = 2;
                roundsRemaining = Rounds;
                ImagesSaved = new List<string>();
                NextTickDate = DateTime.Now.AddSeconds(1);
                //PictureTimer = new Timer();
                //PictureTimer.Interval = 1000;
                //PictureTimer.Tick += new EventHandler(t_Tick);
                //PictureTimer.Start();
            }
            else if (me.Button == System.Windows.Forms.MouseButtons.Right && BoothState != BoothStates.TakePicture)
            {
                NextFrame(true);
            }
        }

        private void NextFrame(bool includeNone)
        {
            CurrentFrameIndex++;
            if (CurrentFrameIndex >= Frames.Count)
            {
                if (includeNone)
                {
                    CurrentFrameIndex = -1;
                    CurrentFrameImage = null;
                }
                else CurrentFrameIndex = 0;
            }
            
            if (CurrentFrameIndex >= 0)
            {
                CurrentFrameImage = Frames[CurrentFrameIndex];
            }
        }

        void Flash()
        {
            FlashEndDate = DateTime.Now.AddMilliseconds(500);
        }

        void SavePicture()
        {
            string targetFile = Path.Combine(SinglesImagePath, DateTime.Now.ToString("yyyyMMdd HHmmss") + ".jpg");
            Bitmap current = (Bitmap)_latestFrame.Clone();
            if (CurrentFrameImage != null)
            {
                Graphics g = Graphics.FromImage(current);
                g.DrawImage(CurrentFrameImage, 0, 0, current.Width, current.Height);
            }
            current.Save(targetFile, ImageFormat.Jpeg);
            ImagesSaved.Add(targetFile);
        }

        void SavePictures()
        {
            List<Image> imagesToSave = new List<Image>();
            int padding = 20;
            int width = padding, height = 0;
            foreach (string imagePath in ImagesSaved)
            {
                Image img = Image.FromFile(imagePath);
                width += img.Width + padding;
                height = Math.Max(height, img.Height);
                imagesToSave.Add(img);
            }
            height += (padding * 2);

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, bmp.Width, bmp.Height);
            int curX = padding;
            foreach (Image image in imagesToSave)
            {
                g.DrawImage(image, curX, padding);
                curX += image.Width + padding;
            }
            PreviewImage = Path.Combine(GroupImagePath, DateTime.Now.ToString("yyyyMMdd HHmmss GROUP") + ".jpg");
            bmp.Save(PreviewImage, ImageFormat.Jpeg);
        }

        void ShowPreview()
        {
            BoothState = BoothStates.Preview;
            PreviewEndDate = DateTime.Now.AddSeconds(Settings.SecondsToPreview);
        }

        //private Timer PictureTimer = null;

        void onTick()
        {
            try
            {
                if (BoothState == BoothStates.GetReady)
                {
                    secondsRemaining--;
                    if (secondsRemaining == 0)
                    {
                        secondsRemaining = Settings.SecondsToWait;
                        BoothState = BoothStates.Countdown;
                        SND_Tick.Play();
                    }
                }
                else if (BoothState == BoothStates.Countdown)
                {
                    secondsRemaining--;
                    if (secondsRemaining == 0)
                    {
                        secondsRemaining = 2;
                        BoothState = BoothStates.TakePicture;
                        SND_Smile.Play();
                    }
                    else SND_Tick.Play();
                }
                else if (BoothState == BoothStates.TakePicture)
                {
                    secondsRemaining--;
                    if (secondsRemaining == 0)
                    {
                        secondsRemaining = Settings.SecondsToWait;

                        SND_CameraClick.Play();
                        // Take picture and save
                        Flash();
                        SavePicture();

                        roundsRemaining--;
                        if (roundsRemaining == 0)
                        {
                            NextTickDate = null;
                            //PictureTimer.Stop();
                            SavePictures();
                            ShowPreview();
                        }
                        else
                        {
                            BoothState = BoothStates.GetReady;
                            secondsRemaining = 2;
                            if (CycleFrames) NextFrame(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Setup.Log("onTick Exception", ex);
            }
            if (NextTickDate != null) NextTickDate = DateTime.Now.AddSeconds(1);
        }

        private void CameraForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void CameraForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                SecondsToWait++;
                if (SecondsToWait >= 30) SecondsToWait = 30;
                Log("Delay set to " + SecondsToWait + " sec");
            }
            else if (e.KeyCode == Keys.Down)
            {
                SecondsToWait--;
                if (SecondsToWait <= 0) SecondsToWait = 1;
                Log("Delay set to " + SecondsToWait + " sec");
            }
            else if (e.KeyCode == Keys.Right)
            {
                Rounds++;
                if (Rounds >= 11) Rounds = 10;
                Log("Rounds set to " + Rounds);
            }
            else if (e.KeyCode == Keys.Left)
            {
                Rounds--;
                if (Rounds <= 0) Rounds = 1;
                Log("Rounds set to " + Rounds);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SecondsToWait = Settings.SecondsToWait;
                Rounds = Settings.Rounds;
                Log("Settings restored");
            }
            else if (e.KeyCode == Keys.Q)
            {
                StopCamera();
                this.Hide();
                LastSetupForm.Show();
            }
        }

        private void CameraForm_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    public enum BoothStates
    {
        WaitingForCamera,
        Idle,
        GetReady,
        Countdown,
        TakePicture,
        Preview
    }

    public enum FontPosition
    {
        Top,
        Middle,
        Bottom
    }
}
