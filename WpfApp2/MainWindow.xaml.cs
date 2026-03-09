using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private HttpListener listener;
        private string localIp;
        private HashSet<string> checkedStudents = new HashSet<string>();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            localIp = GetLocalIPv4();

            if (localIp == null)
            {
                txtStatus.Text = "Không tìm thấy IP mạng!";
                return;
            }
            string random = Guid.NewGuid().ToString();
            string url = $"http://{localIp}:5000/?id={random}";

            txtIp.Text = $"Mã truy cập hiện tại: {url}";
            txtStatus.Text = "Đang khởi động server...";

            GenerateQr(url);

            _ = StartServer();
        }

        private async Task StartServer()
        {
            try
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://+:5000/");
                listener.Start();

                Dispatcher.Invoke(() =>
                {
                    txtStatus.Text = "Server đang chạy 🚀";
                });

                while (listener.IsListening)
                {
                    var context = await listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(context));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() =>
                {
                    txtStatus.Text = "Lỗi: " + ex.Message;
                });
            }
        }

        private async Task HandleRequest(HttpListenerContext context) // hàm nhận và gửi dữ liệu dữ liệu
        {
            string responseString;

            if (context.Request.HttpMethod == "POST") // nhận
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    string body = await reader.ReadToEndAsync();

                    string name = ParseFormValue(body, "name");
                    string msv = ParseFormValue(body, "msv");
                    string numbtele = ParseFormValue(body, "numbtele");

                    bool success =
                    !string.IsNullOrWhiteSpace(name) &&
                    !string.IsNullOrWhiteSpace(msv) &&
                    !string.IsNullOrWhiteSpace(numbtele) &&
                    !checkedStudents.Contains(msv);

                    if (success)
                    {
                        checkedStudents.Add(msv);

                     //  SaveToDatabase(name, msv, numbtele);  // chỗ gọi hàm servi database để lưu 

                        Dispatcher.Invoke(() =>
                        {
                            lstStudents.Items.Insert(0, $"{name} - {msv}");
                        });

                        responseString = $@"
<html>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<body style='font-family:Arial;text-align:center;margin-top:40px'>
<h2 style='color:green'>🎉 Điểm danh thành công!</h2>
<p>Tên: {name}</p>
<p>Mã sinh viên: {msv}</p>
<p>SĐT: {numbtele}</p>

<br>
<a href='/' style='font-size:18px'>Quay lại</a>

</body>
</html>";
                    }
                    else
                    {
                        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "indexfail.html");
                        responseString = File.ReadAllText(path);
                    }
                }

                Log($"POST từ {context.Request.RemoteEndPoint}");
            }
            else // gửi
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "index.html");
                responseString = File.ReadAllText(path);

                Log($"GET từ {context.Request.RemoteEndPoint}");
            }

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            context.Response.ContentType = "text/html; charset=utf-8";
            context.Response.ContentLength64 = buffer.Length;

            await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            context.Response.Close();
        }
        private string ParseFormValue(string body, string key)
        {
            var pairs = body.Split('&');

            foreach (var pair in pairs)
            {
                var parts = pair.Split('=');
                if (parts.Length == 2 && parts[0] == key)
                {
                    return WebUtility.UrlDecode(parts[1]);
                }
            }

            return "";
        }

        private void Log(string message)
        {
            Dispatcher.Invoke(() =>
            {
                txtLog.AppendText(message + Environment.NewLine);
                txtLog.ScrollToEnd();
            });
        }

        private string GetLocalIPv4()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .SelectMany(n => n.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Select(a => a.Address.ToString())
                .FirstOrDefault();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            listener?.Stop();
            listener?.Close();
        }
        void GenerateQr(string url)
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

                qrImage.Source = img;
            }
        }
    }
}