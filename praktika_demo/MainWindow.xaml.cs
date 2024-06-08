using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;

namespace praktika_demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int user_id;
        public MainWindow(int user_id)
        {
            InitializeComponent();
            GenerateQRCode();
        }
        private void GenerateQRCode()
        {

            string googleFormUrl = "https://docs.google.com/forms/d/e/1FAIpQLSdHb7SsdLn9oGX9lh4Uri-ICDk82hmQMc258UpER61cougZOw/viewform?usp=sf_link";


            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 200,
                    Width = 200
                }
            };
            var result = writer.Write(googleFormUrl);


            var bitmap = new BitmapImage();
            using (var stream = new System.IO.MemoryStream())
            {
                result.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            QrCodeImage.Source = bitmap;
        }
        private void sozdat(object sender, RoutedEventArgs e)
        {
            sozdat_zayvku sozdat_Zayvku = new sozdat_zayvku(user_id);
            sozdat_Zayvku.Show();
            this.Close();
        }
        private void redactirovat(object sender, RoutedEventArgs e)
        {
            redaktirovat_zayvki redaktirovat_Zayvki = new redaktirovat_zayvki(user_id);
            redaktirovat_Zayvki.Show();
            this.Close();
        }

        private void exist(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GenerateQRCode_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(user_id);
            mainWindow.Show();
            this.Close();
        }

        private void zayavki(object sender, RoutedEventArgs e)
        {
            zakashik zakashik = new zakashik(user_id);
            zakashik.Show();
            this.Close();

        }
    }
}
