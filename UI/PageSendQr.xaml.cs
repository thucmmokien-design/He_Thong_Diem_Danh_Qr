using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using He_Thong_Diem_Danh_Qr.BackEnd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace He_Thong_Diem_Danh_Qr.UI
{
    public partial class PageSendQr : UserControl
    {
        GiangVien giangVien;
        LopHocPhanService lopHocPhanService = new LopHocPhanService();
        DangKiLopService dangKiLopService = new DangKiLopService();
        BuoiHocService buoiHocService = new BuoiHocService();
        DiemDanhService diemDanhService = new DiemDanhService();
        QRService qrService = new QRService();
        NetworkService networkService = new NetworkService();
        HttpServerService httpServerService = new HttpServerService();
        
        public PageSendQr(GiangVien gv)
        {
            InitializeComponent();
            giangVien = gv;
            loadcomboxAndtxt();
        }
        
        private void loadcomboxAndtxt()
        {
            txtNameGiaoVien.Text = "Giảng Viên: " + giangVien.hoten;
            var dslop = lopHocPhanService.LayTatCaLopHoc(giangVien.gv_id);
            ComboxLhp.ItemsSource = dslop;
            ComboxLhp.DisplayMemberPath = "class_id";
        }
        
        private void comboBoxHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (LopHocPhan)ComboxLhp.SelectedItem;
            if (selectedItem != null)
            {
                txtTenMonHoc.Text = "Môn Học : " + selectedItem.ten_mon;
            }
        }
        
        private async void btnsendqr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var items = (LopHocPhan)ComboxLhp.SelectedItem;
                if (items == null)
                {
                    MessageBox.Show("Vui lòng chọn lớp học phần!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                string classId = items.class_id;
                string messagedkmonhoc = dangKiLopService.checkLophocPhan(classId);
                if (!string.IsNullOrEmpty(messagedkmonhoc))
                {
                    MessageBox.Show(messagedkmonhoc, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                
                // Kiểm tra xem đã có buổi học hôm nay chưa
                BuoiHocDao buoiHocDao = new BuoiHocDao();
                var dsBuoiHoc = buoiHocDao.GetAll();
                var buoiHocHomNay = dsBuoiHoc
                    .Where(bh => bh.class_id == classId && bh.ngay_hoc.Date == DateTime.Today)
                    .OrderByDescending(bh => bh.session_id)
                    .FirstOrDefault();
                
                int sessionId = 0;
                
                if (buoiHocHomNay != null)
                {
                    // Đã có buổi học hôm nay, sử dụng lại QR code
                    sessionId = buoiHocHomNay.session_id;
                    MessageBox.Show("Đã tìm thấy buổi học hôm nay. Hiển thị lại QR code!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Chưa có buổi học, tạo mới
                    string codeQr = Guid.NewGuid().ToString().Substring(0, 8);
                    DateTime endTime = DateTime.Now.AddHours(17);
                    string message = buoiHocService.ThemBuoiHoc(classId, endTime, codeQr);
                    
                    if (message != "Tạo Qr Thành Công")
                    {
                        MessageBox.Show(message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Tạo danh sách điểm danh
                    SinhVienDao sinhVienDao = new SinhVienDao();
                    List<SinhVien> dsSinhVien = sinhVienDao.GetAll();
                    diemDanhService.TaoDanhSachDiemDanh(classId, dsSinhVien);
                    
                    // Lấy session_id vừa tạo
                    dsBuoiHoc = buoiHocDao.GetAll();
                    sessionId = dsBuoiHoc
                        .Where(bh => bh.class_id == classId && bh.ngay_hoc.Date == DateTime.Today)
                        .OrderByDescending(bh => bh.session_id)
                        .FirstOrDefault()?.session_id ?? 0;
                    
                    if (sessionId == 0)
                    {
                        MessageBox.Show("Không tìm thấy buổi học!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                
                // Hiển thị QR code
                string localIp = networkService.GetLocalIPv4();
                if (localIp == null)
                {
                    MessageBox.Show("Không tìm thấy IP mạng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                string url = $"http://{localIp}:5000/?session={sessionId}";
                MessageBox.Show($"IP: {localIp}\nSession ID: {sessionId}\nURL: {url}", "Thông tin QR", MessageBoxButton.OK, MessageBoxImage.Information);
                qrImage.Source = qrService.GenerateQrImage(url);
                
                // Khởi động server
                _ = httpServerService.StartServer(sessionId, 5000);
                MessageBox.Show("QR code đã sẵn sàng và server đang chạy!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
