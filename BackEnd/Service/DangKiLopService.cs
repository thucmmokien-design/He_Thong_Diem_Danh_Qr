using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    class DangKiLopService
    {
        private readonly DangKiLopDao dangKiLopDao = new DangKiLopDao();
        public string checkLophocPhan(string class_id)
        {
            bool coSinhVien = dangKiLopDao.checkLopHocPhan(class_id);
            if (!coSinhVien) 
            {
                return "Lớp Học Phần Chưa Có Sinh Viên Đăng Ký!";
            }
            return "";
        }
    }
}
