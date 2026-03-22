using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class NetworkService
    {
        public string GetLocalIPv4()
        {
            var ips = NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || 
                           n.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                .Where(n => !n.Description.Contains("Virtual") && 
                           !n.Description.Contains("VMware") && 
                           !n.Description.Contains("VirtualBox"))
                .SelectMany(n => n.GetIPProperties().UnicastAddresses)
                .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
                .Where(a => !a.Address.ToString().StartsWith("169.254")) // Bỏ APIPA
                .Select(a => a.Address.ToString())
                .ToList();
            
            // Ưu tiên IP 192.168.x.x hoặc 10.x.x.x
            var preferredIp = ips.FirstOrDefault(ip => ip.StartsWith("192.168.") || ip.StartsWith("10."));
            
            return preferredIp ?? ips.FirstOrDefault();
        }
    }
}
