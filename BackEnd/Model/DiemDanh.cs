using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Model
{
    public class DiemDanh
    {
        public int id { get; set; }

        public int session_id { get; set; }

        public string msv { get; set; }

        public string hoten { get; set; }

        public string email { get; set; }
        
        public string class_id { get; set; }

        public DateTime checkin_time { get; set; }

        public string status { get; set; }

        public int sobuoivang { get; set; }
    }
}
