using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Model
{
    public class BuoiHoc
    {
        public int session_id { get; set; }

        public string class_id { get; set; }

        public DateTime ngay_hoc { get; set; }

        public TimeSpan start_time { get; set; }

        public TimeSpan end_time { get; set; }

        public string qr_secret { get; set; }

        public bool is_active { get; set; }
    }
}
