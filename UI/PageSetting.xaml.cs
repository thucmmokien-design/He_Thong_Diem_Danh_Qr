using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using He_Thong_Diem_Danh_Qr.BackEnd.Service;
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
    public partial class PageSetting : UserControl
    {
        GiangVien giangVien;
        GiangVienService giangVienService = new GiangVienService();
        public PageSetting(GiangVien gv)
        {
            InitializeComponent();
            giangVien = gv;
        }
        public void btn_xacnhan_Click(object sender, RoutedEventArgs e)
        {
            if(txtPasswordold.Text != giangVien.password)
            {
                MessageBox.Show("Mật khẩu cũ không đúng");
                return;
            }
            if(txtPasswordnew.Text != txtPasswordnewss.Text)
            {
                MessageBox.Show("Mật khẩu mới không khớp với nhau");
                return;
            }
            giangVien.password = txtPasswordnewss.Text;
            if (giangVienService.CapNhatThongTin(giangVien))
            {
                MessageBox.Show("Đổi mật khẩu thành công");
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại");
            }
        }
    }
}
