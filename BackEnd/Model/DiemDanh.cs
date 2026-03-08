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

        public DateTime checkin_time { get; set; }

        public string status { get; set; }
    }
}
