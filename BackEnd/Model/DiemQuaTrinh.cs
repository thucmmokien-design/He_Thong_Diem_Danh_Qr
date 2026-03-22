using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Model
{
    class DiemQuaTrinh
    {
        public string msv { get; set; }
        public string hoten {  get; set; }
        public string class_id { get; set; }
        public float diem_chuyen_can { get; set; }
        public int so_lan_len_bang { get; set; }
        public string ghi_chu { get; set; }
        public int tong_so_buoi_vang { get; set; }
        public static implicit operator List<object>(DiemQuaTrinh v)
        {
            throw new NotImplementedException();
        }
    }
}
