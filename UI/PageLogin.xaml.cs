using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using He_Thong_Diem_Danh_Qr.BackEnd.Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace He_Thong_Diem_Danh_Qr.UI
{
    public partial class PageLogin : Window
    {
        GiangVien giangVien = new GiangVien();
        GiangVienService giangVienService = new GiangVienService();
        public PageLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Bạn điền thiếu thông tin");
                return;
            }

            giangVien = giangVienService.DangNhap(txtUsername.Text, txtPassword.Password);

            if (giangVien != null)
            {
                MessageBox.Show("Login Thành Công");

                PageHome home = new PageHome(giangVien);
                home.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }
    }
}