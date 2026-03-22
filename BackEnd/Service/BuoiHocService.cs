using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class BuoiHocService
    {
        private readonly BuoiHocDao _buoiHocDao = new BuoiHocDao();

        public List<BuoiHoc> LayTatCaBuoiHoc()
        {
            try { return _buoiHocDao.GetAll(); }
            catch (Exception ex) { throw new Exception("Lỗi khi lấy danh sách buổi học: " + ex.Message); }
        }
        public string ThemBuoiHoc(string class_id, DateTime end_time, string codeqr)
        {
            var dsBuoiHoc = _buoiHocDao.GetAll();
            DateTime today = DateTime.Today;
            foreach (var bh in dsBuoiHoc)
            {
                if (bh.class_id == class_id && bh.ngay_hoc.Date == today)
                {
                    return "Buổi học đã tồn tại trong ngày này!";
                }
            }
            bool result = _buoiHocDao.InsertBuoiHocnew(class_id, today, end_time, codeqr);

            if (result)
                return "Tạo Qr Thành Công";
            else
                return "Buổi Học Đã Tồn Tại";
        }

        public string TaoMaQr(int sessionId)
        {
            string secret = Guid.NewGuid().ToString().Substring(0, 8);

            BuoiHoc bh = _buoiHocDao.GetById(sessionId);
            if (bh != null)
            {
                bh.qr_secret = secret;
                bh.is_active = true;
                if (_buoiHocDao.Update(bh))
                {
                    return secret; 
                }
            }
            return null;
        }
    }
}