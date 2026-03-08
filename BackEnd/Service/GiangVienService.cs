using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class GiangVienService
    {
        private readonly GiangVienDao _giangVienDao = new GiangVienDao();

        // Đăng nhập
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

        //Quản lý thông tin giảng viên
        public List<GiangVien> LayTatCaGiangVien()
        {
            return _giangVienDao.GetAll();
        }

        //Thêm mới giảng viên
        public bool ThemGiangVien(GiangVien gv)
        {
            // Kiểm tra email đã tồn tại chưa trước khi thêm
            List<GiangVien> ds = _giangVienDao.GetAll();
            foreach (var item in ds)
            {
                if (item.email == gv.email) return false;
            }
            return _giangVienDao.Insert(gv);
        }

        //Cập nhật thông tin cá nhân
        public bool CapNhatThongTin(GiangVien gv)
        {
            return _giangVienDao.Update(gv);
        }

        // Xóa tài khoản giảng viên
        public bool XoaGiangVien(int id)
        {
            return _giangVienDao.Delete(id);
        }

        //Lấy thông tin giảng viên theo ID để hiển thị lên Profile
        public GiangVien TimTheoId(int id)
        {
            return _giangVienDao.GetById(id);
        }
    }
}