using He_Thong_Diem_Danh_Qr.BackEnd.Model;
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
    /// Interaction logic for PageHome.xaml
    /// </summary>
    public partial class PageHome : Window
    {
        GiangVien giangVien;
        public PageHome(GiangVien gv)
        {
            InitializeComponent();
            giangVien = gv;
        }
        bool isMenuExpanded = true;
        private void btnMenu_Click(object sender, RoutedEventArgs e) {
            if (isMenuExpanded) {
                SidebarColumn.Width = new GridLength(65);
                txtMenu.Visibility = Visibility.Collapsed;
                txtHome.Visibility = Visibility.Collapsed;
                txtQuanLyBuoiHoc.Visibility = Visibility.Collapsed;
                txtKiemTraChuyenCan.Visibility = Visibility.Collapsed;
                txtSetting.Visibility = Visibility.Collapsed;
            }
            else
            {
                SidebarColumn.Width = new GridLength(210);
                txtMenu.Visibility = Visibility.Visible;
                txtHome.Visibility = Visibility.Visible;
                txtQuanLyBuoiHoc.Visibility =Visibility.Visible;
                txtKiemTraChuyenCan.Visibility =Visibility.Visible;
                txtSetting.Visibility = Visibility.Visible;
            }
            isMenuExpanded = !isMenuExpanded;
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PageSendQr(giangVien);
        }
        private void btnQuanLyBuoiHoc_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PageQuanLySinhVien(giangVien);
        }
        private void btnKiemTraChuyenCan_Click(object sender, RoutedEventArgs e)
        {
            MainContentArea.Content = new PageQuanLyChuyenCan(giangVien);
        }
        private void btnSetting_Click(object sender, RoutedEventArgs e) {
            MainContentArea.Content = new PageSetting(giangVien);
        }
    }
}
