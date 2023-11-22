using System.Threading.Channels;
using System.Windows;

using Microsoft.Win32;

using OpenCvSharp;
using OpenCvSharp.WpfExtensions;

using Window = System.Windows.Window;

namespace OpencvDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel ViewModel = new();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = ViewModel;
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() is false) return;

            var imgPath = ofd.FileName;
            var img = Cv2.ImRead(imgPath);

            ViewModel.MainImage = img.ToBitmapSource();
        }

        private void SaveImage(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new();
            if (sfd.ShowDialog() is false || ViewModel.MainImage is null) return;

            var imgPath = sfd.FileName;
            var mat = ViewModel.MainImage.ToMat();
            Cv2.ImWrite(imgPath, mat);
        }

        private void ShowImage(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainImage is null) return;

            var mat = ViewModel.MainImage.ToMat();
            Cv2.ImShow("Displayed Image", mat);
            Cv2.WaitKey(0);
        }

        private void GetImageInfo(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainImage is null) return;

            var image = ViewModel.MainImage.ToMat();

            ViewModel.Width = image.Width.ToString();
            ViewModel.Height = image.Height.ToString();
            ViewModel.Channel = image.Channels().ToString();
        }

        private void CreateImage(object sender, RoutedEventArgs e)
        {
            Mat newImage = Mat.Ones(new OpenCvSharp.Size(100, 100), MatType.CV_8UC3);
            ViewModel.MainImage = newImage.ToBitmapSource();
        }

        private void CropImage(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainImage is null) return;

            var mat = ViewModel.MainImage.ToMat();
            Mat croppedImage = new Mat(mat, new OpenCvSharp.Rect(100, 100, 1000, 1000));

            Cv2.ImShow("Crop_Img", croppedImage);
            Cv2.WaitKey(0);
        }

        private void ConcatImage(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainImage is null) return;

            var mat = ViewModel.MainImage.ToMat();
            Mat croppedImage = new Mat(mat, new OpenCvSharp.Rect(0, 0, 1000, 1000));
            Mat croppedImage2 = new Mat(mat, new OpenCvSharp.Rect(1000, 0, 1000, 1000));

            Mat croppedImage3 = new Mat(mat, new OpenCvSharp.Rect(0, 0, 1000, 1000));
            Mat croppedImage4 = new Mat(mat, new OpenCvSharp.Rect(0, 1000, 1000, 1000));

            Mat[] imagesToConcat = { croppedImage, croppedImage2 };
            Mat concatenatedImage_H = new Mat();
            Cv2.HConcat(imagesToConcat, concatenatedImage_H);

            Mat[] imagesToConcat_V = { croppedImage3, croppedImage4 };
            Mat concatenatedImage_V = new Mat();
            Cv2.VConcat(imagesToConcat_V, concatenatedImage_V);

            Cv2.ImShow("concatenatedImage_H", concatenatedImage_H);
            Cv2.ImShow("concatenatedImage_V", concatenatedImage_V);

            Cv2.WaitKey(0);
        }

        private void SplitImage(object sender, RoutedEventArgs e)
        {
            if (ViewModel.MainImage is null) return;

            var mat = ViewModel.MainImage.ToMat();
            Mat[] channels;
            Cv2.Split(mat, out channels);

            Cv2.ImShow("channels[0]", channels[0]);
            Cv2.WaitKey(0);

            Mat[] channelsToMerge = { channels[1], channels[0], channels[2] };
            Mat mergedImage = new Mat();
            Cv2.Merge(channelsToMerge, mergedImage);
            ViewModel.MainImage = mergedImage.ToBitmapSource();
        }
    }
}