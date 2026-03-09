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
        public PageHome()
        {
            InitializeComponent();
        }
        bool isMenuExpanded = true;
        private void btnMenu_Click(object sender, RoutedEventArgs e) {
            if (isMenuExpanded) {
                SidebarColumn.Width = new GridLength(65);
            }
            else
            {
                SidebarColumn.Width = new GridLength(150);
            }
            isMenuExpanded = !isMenuExpanded;
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainContenArea.Content = new PageSendQr();
        }
        private void btnQuanLyBuoiHoc_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnKiemTraChuyenCan_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnSetting_Click(object sender, RoutedEventArgs e) { 

        }
    }
}
