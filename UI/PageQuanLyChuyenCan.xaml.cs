using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using He_Thong_Diem_Danh_Qr.BackEnd.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class PageQuanLyChuyenCan : UserControl
    {
        private GiangVien giangVien;
        private readonly LopHocPhanService lopHocPhanService = new LopHocPhanService();
        private readonly DiemQuaTrinhSevice diemQuaTrinhSevice = new DiemQuaTrinhSevice();
        
        public PageQuanLyChuyenCan(GiangVien gv)
        {
            giangVien = gv;
            InitializeComponent();
            loadcomboxAndaddbuoivang();
            dsChuyenCan.SelectionChanged += dsChuyenCan_SelectionChanged;
        }
        public void loadcomboxAndaddbuoivang()
        {
            diemQuaTrinhSevice.updateDanhSachVang();
            var dslop = lopHocPhanService.LayTatCaLopHoc(giangVien.gv_id);
            comboBoxHocPhan2.ItemsSource = dslop;
            comboBoxHocPhan2.DisplayMemberPath = "class_id";
        }
        
        public void btnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            string name = inputhoten.Text;
            string classId = (comboBoxHocPhan2.SelectedItem as LopHocPhan)?.class_id;
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(classId))
            {
                dsChuyenCan.ItemsSource = diemQuaTrinhSevice.searchNameAndClass(name, classId);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                dsChuyenCan.ItemsSource = diemQuaTrinhSevice.searchName(name);
            }
            else if (!string.IsNullOrEmpty(classId))
            {
                dsChuyenCan.ItemsSource = diemQuaTrinhSevice.searchClass(classId);
            }
        }
        public void btnupdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DiemQuaTrinh sl = (DiemQuaTrinh)dsChuyenCan.SelectedItem;
                string class_id = comboBoxHocPhan2.SelectedValue.ToString();
                string msv = sl.msv;
                float diem = Convert.ToSingle(inputdiemcuyencan.Text);
                string messbox1 = diemQuaTrinhSevice.updateDiem(msv, class_id, diem);
                int soLanLenBang = Convert.ToInt32(inputdonggop.Text);
                string messbox2 = diemQuaTrinhSevice.CapNhatSoLanLenBang(msv, class_id, soLanLenBang);
                string ghiChu = inputdanhgiahoctap.Text;
                string messbox3 = diemQuaTrinhSevice.CapNhatGhiChu(msv, class_id, ghiChu);
                string result = messbox1 + "\n" + messbox2 + "\n" + messbox3;
                MessageBox.Show(result, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                dsChuyenCan.ItemsSource = diemQuaTrinhSevice.LayTatCaDiemQuaTrinh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void dsChuyenCan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dsChuyenCan.SelectedItem != null)
            {
                DiemQuaTrinh selected = (DiemQuaTrinh)dsChuyenCan.SelectedItem;
                inputhoten.Text = selected.hoten;
                inputdonggop.Text = selected.so_lan_len_bang.ToString();
                inputdiemcuyencan.Text = selected.diem_chuyen_can.ToString();
                inputdanhgiahoctap.Text = selected.ghi_chu.ToString();
                var dslop = lopHocPhanService.LayTatCaLopHoc(giangVien.gv_id);
                var lopHoc = dslop.FirstOrDefault(l => l.class_id == selected.class_id);
                if (lopHoc != null)
                {
                    comboBoxHocPhan2.SelectedItem = lopHoc;
                }
            }
        }
    }
}
