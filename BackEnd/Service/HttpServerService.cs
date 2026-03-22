using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class HttpServerService
    {
        private HttpListener listener;
        private readonly DiemDanhService diemDanhService;
        private int currentSessionId;
        
        public HttpServerService()
        {
            diemDanhService = new DiemDanhService();
        }
        
        public async Task StartServer(int sessionId, int port = 5000)
        {
            currentSessionId = sessionId;
            
            try
            {
                if (listener != null && listener.IsListening)
                {
                    listener.Stop();
                }
                
                listener = new HttpListener();
                listener.Prefixes.Add($"http://+:{port}/");
                listener.Start();
                
                while (listener.IsListening)
                {
                    var context = await listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(context));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khởi động server: {ex.Message}");
            }
        }
        
        public void StopServer()
        {
            if (listener != null && listener.IsListening)
            {
                listener.Stop();
                listener.Close();
            }
        }
        
        private async Task HandleRequest(HttpListenerContext context)
        {
            string responseString;
            
            if (context.Request.HttpMethod == "POST")
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    string body = await reader.ReadToEndAsync();
                    string msv = ParseFormValue(body, "msv");
                    
                    if (!string.IsNullOrWhiteSpace(msv))
                    {
                        bool success = diemDanhService.CapNhatDiemDanh(currentSessionId, msv);
                        
                        if (success)
                        {
                            responseString = GenerateSuccessPage(msv);
                        }
                        else
                        {
                            responseString = GenerateAlreadyCheckedInPage(msv);
                        }
                    }
                    else
                    {
                        responseString = "<html><body><h2>Dữ liệu không hợp lệ</h2></body></html>";
                    }
                }
            }
            else
            {
                responseString = GenerateFormPage();
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
        
        private string GenerateFormPage()
        {
            return @"
<html>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<head>
<style>
body { font-family: Arial; text-align: center; margin-top: 40px; background: #f5f5f5; }
.container { max-width: 400px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
h2 { color: #333; }
input { width: 100%; padding: 12px; margin: 10px 0; font-size: 16px; border: 2px solid #ddd; border-radius: 5px; box-sizing: border-box; }
button { width: 100%; padding: 15px; font-size: 18px; background: #4CAF50; color: white; border: none; border-radius: 5px; cursor: pointer; }
button:hover { background: #45a049; }
</style>
</head>
<body>
<div class='container'>
<h2>📱 Điểm danh QR</h2>
<form method='POST'>
<input type='text' name='msv' placeholder='Nhập mã sinh viên' required autofocus>
<button type='submit'>Điểm danh</button>
</form>
</div>
</body>
</html>";
        }
        
        private string GenerateSuccessPage(string msv)
        {
            return $@"
<html>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<head>
<style>
body {{ font-family: Arial; text-align: center; margin-top: 40px; background: #f5f5f5; }}
.container {{ max-width: 400px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
h2 {{ color: #4CAF50; }}
a {{ display: inline-block; margin-top: 20px; padding: 10px 20px; background: #2196F3; color: white; text-decoration: none; border-radius: 5px; }}
</style>
</head>
<body>
<div class='container'>
<h2>🎉 Điểm danh thành công!</h2>
<p>Mã sinh viên: <strong>{msv}</strong></p>
<p>Thời gian: {DateTime.Now:HH:mm:ss dd/MM/yyyy}</p>
<a href='/'>Quay lại</a>
</div>
</body>
</html>";
        }
        
        private string GenerateAlreadyCheckedInPage(string msv)
        {
            return $@"
<html>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<head>
<style>
body {{ font-family: Arial; text-align: center; margin-top: 40px; background: #f5f5f5; }}
.container {{ max-width: 400px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
h2 {{ color: #FF9800; }}
p {{ font-size: 16px; color: #555; }}
.warning {{ background: #FFF3E0; padding: 15px; border-radius: 5px; margin: 15px 0; border-left: 4px solid #FF9800; }}
a {{ display: inline-block; margin-top: 20px; padding: 10px 20px; background: #2196F3; color: white; text-decoration: none; border-radius: 5px; }}
</style>
</head>
<body>
<div class='container'>
<h2>⚠️ Đã điểm danh rồi!</h2>
<div class='warning'>
<p><strong>Mã sinh viên: {msv}</strong></p>
<p>Bạn đã điểm danh cho buổi học này rồi.</p>
<p>Không thể điểm danh lại!</p>
</div>
<a href='/'>Quay lại</a>
</div>
</body>
</html>";
        }
        
        private string GenerateFailurePage()
        {
            return @"
<html>
<meta name='viewport' content='width=device-width, initial-scale=1.0'>
<head>
<style>
body { font-family: Arial; text-align: center; margin-top: 40px; background: #f5f5f5; }
.container { max-width: 400px; margin: 0 auto; background: white; padding: 30px; border-radius: 10px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
h2 { color: #f44336; }
a { display: inline-block; margin-top: 20px; padding: 10px 20px; background: #2196F3; color: white; text-decoration: none; border-radius: 5px; }
</style>
</head>
<body>
<div class='container'>
<h2>❌ Điểm danh thất bại!</h2>
<p>Mã sinh viên không tồn tại trong hệ thống</p>
<a href='/'>Thử lại</a>
</div>
</body>
</html>";
        }
    }
}
