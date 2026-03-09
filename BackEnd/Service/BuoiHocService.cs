using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class BuoiHocService
    {
        private readonly BuoiHocDao _buoiHocDao = new BuoiHocDao();

        //Lấy toàn bộ danh sách buổi học
        public List<BuoiHoc> LayTatCaBuoiHoc()
        {
            try { return _buoiHocDao.GetAll(); }
            catch (Exception ex) { throw new Exception("Lỗi khi lấy danh sách buổi học: " + ex.Message); }
        }

        //Thêm buổi học mới
        public bool ThemBuoiHoc(BuoiHoc bh)
        {
            if (string.IsNullOrEmpty(bh.class_id)) return false;
            return _buoiHocDao.Insert(bh);
        }

        //Cập nhật thông tin buổi học
        public bool CapNhatBuoiHoc(BuoiHoc bh)
        {
            return _buoiHocDao.Update(bh);
        }

        //Xóa buổi học
        public bool XoaBuoiHoc(int id)
        {
            return _buoiHocDao.Delete(id);
        }

        //Tìm buổi học theo ID
        public BuoiHoc TimTheoId(int id)
        {
            return _buoiHocDao.GetById(id);
        }

        // Kiểm tra buổi học có đang diễn ra không
        public bool KiemTraBuoiHocKhaDung(int id)
        {
            BuoiHoc bh = _buoiHocDao.GetById(id);
            return bh != null && bh.is_active;
        }

        public string TaoMaQr(int sessionId)
        {
            // Tạo chuỗi ngẫu nhiên 8 ký tự làm QR cho buổi học
            string secret = Guid.NewGuid().ToString().Substring(0, 8);

            BuoiHoc bh = _buoiHocDao.GetById(sessionId);
            if (bh != null)
            {
                bh.qr_secret = secret;
                bh.is_active = true; // Kích hoạt điểm danh

                // Cập nhật lại vào DB qua DAO
                if (_buoiHocDao.Update(bh))
                {
                    return secret; // Trả về chuỗi này để bên UI vẽ thành hình QR
                }
            }
            return null;
        }
    }
}