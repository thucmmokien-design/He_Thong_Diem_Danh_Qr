using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class SinhVienService
    {
        private readonly SinhVienDao _sinhVienDao = new SinhVienDao();

        public SinhVien DangNhap(string msv, string password)
        {
            List<SinhVien> dsSinhVien = _sinhVienDao.GetAll();

            foreach (var sv in dsSinhVien)
            {
                if (sv.msv == msv && sv.password == password)
                {
                    return sv; 
                }
            }
            return null; 
        }

        public SinhVien TimTheoMSV(string msv)
        {
            return _sinhVienDao.GetById(msv);
        }

        public bool ThemSinhVien(SinhVien sv)
        {
            if (_sinhVienDao.GetById(sv.msv) != null)
            {
                return false;
            }
            return _sinhVienDao.Insert(sv);
        }

        // Cập nhật thông tin sinh viên
        public bool CapNhatSinhVien(SinhVien sv)
        {
            return _sinhVienDao.Update(sv);
        }

        //Xóa sinh viên
        public bool XoaSinhVien(string msv)
        {
            return _sinhVienDao.Delete(msv);
        }

        // Lấy toàn bộ danh sách sinh viên
        public List<SinhVien> LayTatCaSinhVien()
        {
            return _sinhVienDao.GetAll();
        }
    }
}