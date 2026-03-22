using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using He_Thong_Diem_Danh_Qr.BackEnd.Service;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace He_Thong_Diem_Danh_Qr.UI
{
    public partial class PageQuanLySinhVien : UserControl
    {
        GiangVien giangVien;
        DiemDanhService diemDanhService = new DiemDanhService();
        LopHocPhanService lopHocPhanService = new LopHocPhanService();
        
        public PageQuanLySinhVien(GiangVien gv)
        {
            giangVien = gv;
            InitializeComponent();
            loadtableSinhVien();
            loadcombox();
        }

        private void loadcombox()
        {
            var dslop = lopHocPhanService.LayTatCaLopHoc(giangVien.gv_id);
            comboBoxHocPhan.ItemsSource = dslop;
        }

        private void comboBoxHocPhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = comboBoxHocPhan.SelectedItem as LopHocPhan;
            if (selectedItem != null)
            {
                txtMonHoc.Text = "Tên Môn Học Là: " + selectedItem.ten_mon;
            }
        }

        private void loadtableSinhVien()
        {
            try
            {   
                List<DiemDanh> ds = diemDanhService.LayDanhSach();
                if (ds != null)
                {
                    dgSinhVien.ItemsSource = diemDanhService.LayDanhSach();
                }
                else
                {
                    MessageBox.Show("Vui lòng quay lại tạo qr trước khi xem danh sách buổi học ngày hôm nay ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBoxBuoiHoc.SelectedItem == null || comboBoxHocPhan.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn đủ thông tin !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                ComboBoxItem item = (ComboBoxItem)comboBoxBuoiHoc.SelectedItem;
                string txtstatus = item.Content.ToString();
                
                var items = (LopHocPhan)comboBoxHocPhan.SelectedItem;
                string classId = items.class_id;
                dgSinhVien.ItemsSource = diemDanhService.TronLoc(classId, txtstatus);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
