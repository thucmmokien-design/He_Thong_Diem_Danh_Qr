using QRCoder;
using System.IO;
using System.Windows.Media.Imaging;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class QRService
    {
        public BitmapImage GenerateQrImage(string url)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            
            PngByteQRCode qr = new PngByteQRCode(data);
            byte[] bytes = qr.GetGraphic(20);
            
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.StreamSource = ms;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                
                return img;
            }
        }
    }
}
