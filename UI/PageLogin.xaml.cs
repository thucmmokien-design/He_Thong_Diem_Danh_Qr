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
using System.Windows.Shapes;

namespace He_Thong_Diem_Danh_Qr.UI
{
    /// <summary>
    /// Interaction logic for PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Window
    {
        public PageLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Bạn điền thiếu thông tin");
                return;
            }
            if (txtUsername.Text == "admin" && txtPassword.Password == "123")
            {
                MessageBox.Show("Login Thanh Cong");
                PageHome home = new PageHome();
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
