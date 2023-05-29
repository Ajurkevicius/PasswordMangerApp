using Emgu.CV;
using Emgu.CV.Structure;
using ImageRecognition.Data;
using ImageRecognition.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageRecognition.UIWindows
{
    /// <summary>
    /// Interaction logic for FaceRecognitionWindow.xaml
    /// </summary>
    public partial class FaceRecognitionWindow : Window
    {

        Account _account;
        bool _streaming = false;
        VideoCapture _capture;
        bool captureInProgress = false;
        bool replace_images = false;
        public FaceRecognitionWindow()
        {
            InitializeComponent();
        }
        public FaceRecognitionWindow(Account account)
        {
            _account = account;
            InitializeComponent();
        }
        public FaceRecognitionWindow(bool replaceImages)
        {
            replace_images = replaceImages;
            InitializeComponent();
        }

        private async void Capture_Click(object sender, RoutedEventArgs e)
        {
            if (replace_images == false)
            {
                
                var img = _capture.QueryFrame().ToImage<Bgr, byte>();
                var bmp = img.ToBitmap();
                bmp.Save("", ImageFormat.Jpeg); // link to input_image
                string validation = await ModelController.run_cmd("a", "a");
                string replacement = Regex.Replace(validation, @"\t|\n|\r", "");
                char trueOrFalseValidation = replacement[replacement.Length - 1];
                Debug.WriteLine("This is the validation: " + validation);
                Debug.WriteLine("This is the value:" + trueOrFalseValidation);
                if (Char.Equals(trueOrFalseValidation, 'f'))
                {
                    MessageBox.Show("Wrong credentials");
                }
                else
                {
                    SelectedPasswordWindow selectedPasswordWindow = new SelectedPasswordWindow(_account);
                    this.Hide();
                    selectedPasswordWindow.Show();
                }
            }
            else
            {
                for (int i =0; i<=10; i++)
                {
                    string baseDirectory = ""; // link to validation folder
                    var img = _capture.QueryFrame().ToImage<Bgr, byte>();
                    var bmp = img.ToBitmap();
                    string currentImage = "\\image" + i.ToString()+".jpg";
                    string wholeDirectory = baseDirectory + currentImage;
                    bmp.Save(wholeDirectory, ImageFormat.Jpeg);
                    Thread.Sleep(500);
                }
            }
            
        }

        private void Camera_Load(object sender, RoutedEventArgs e)
        {
            _streaming = true;
           
            Thread.Sleep(1000);
            StartCapture();
            
        }
        private void StartCapture()
        {
            if (_capture == null)
            {
                _capture = new VideoCapture(0);
            }
            if (captureInProgress)
            {
               
                return;
            }
            _capture.ImageGrabbed += Capture_ImageGrabbed;

            _capture.Start();
            captureInProgress = true;
        }

        private void capturing(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            _capture.Retrieve(frame, 0);

            var frameImage = _capture.QueryFrame().ToImage<Bgr, byte>();
            var bitmapSource = frameImage.ToBitmapSource();
            cameraCaptureWindow.Source = bitmapSource;

        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            Mat frame = new Mat();
            _capture.Retrieve(frame);
            
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>();
            BitmapSource source = image.ToBitmapSource();
            BitmapEncoder encoder = new PngBitmapEncoder();

           
            source.Freeze();
           
            if (source != null)
            {
                Dispatcher.Invoke(new Action(() =>
                cameraCaptureWindow.Source = source));
            }
            else
            {
                MessageBox.Show("No camera input");
            }

        }

        private void MasterPasswordClicked(object sender, RoutedEventArgs e)
        {
            OldPassword oldPassword = new OldPassword(true, _account);
            this.Hide();
            oldPassword.Show();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ManagedPasswordsWindow managedPasswordsWindow = new ManagedPasswordsWindow();
            this.Hide();
            managedPasswordsWindow.Show();
        }
    }
}
