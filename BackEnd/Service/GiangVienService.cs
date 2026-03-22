using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class GiangVienService
    {
        private readonly GiangVienDao _giangVienDao = new GiangVienDao();

        public GiangVien DangNhap(string email, string password)
        {
            List<GiangVien> dsGiangVien = _giangVienDao.GetAll();

            foreach (var gv in dsGiangVien)
            {
                if (gv.email.Equals(email, StringComparison.OrdinalIgnoreCase)
                    && gv.password == password)
                {
                    return gv;
                }
            }
            return null; 
        }
        public bool CapNhatThongTin(GiangVien gv)
        {
            return _giangVienDao.Update(gv);
        }
    }
}