using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class DiemDanhService
    {
        private readonly DiemDanhDao _diemDanhDao = new DiemDanhDao();
        //Xử lý Quét mã điểm danh
        public string ThucHienDiemDanh(string msv, int sessionId)
        {
            try
            {
                List<DiemDanh> all = _diemDanhDao.GetAll();
                foreach (var item in all)
                {
                    if (item.msv == msv && item.session_id == sessionId)
                    {
                        return "Bạn đã điểm danh cho buổi học này rồi!";
                    }
                }
                DiemDanh dd = new DiemDanh
                {
                    msv = msv,
                    session_id = sessionId,
                    checkin_time = DateTime.Now,
                    status = "Present"
                };
                return _diemDanhDao.Insert(dd) ? "Điểm danh thành công!" : "Lỗi hệ thống!";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        // Lấy danh sách điểm danh theo Buổi học
        public List<DiemDanh> LayDiemDanhTheoBuoiHoc(int sessionId)
        {
            List<DiemDanh> all = _diemDanhDao.GetAll();
            List<DiemDanh> result = new List<DiemDanh>();

            foreach (var item in all)
            {
                if (item.session_id == sessionId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        // Thống kê chuyên cần 
        public int DemSoLanCoMat(string msv)
        {
            List<DiemDanh> all = _diemDanhDao.GetAll();
            int count = 0;

            foreach (var item in all)
            {
                if (item.msv == msv && item.status == "Present")
                {
                    count++;
                }
            }
            return count;
        }

        public List<DiemDanh> LayTatCa()
        {
            return _diemDanhDao.GetAll();
        }

        public bool XoaDiemDanh(int id)
        {
            return _diemDanhDao.Delete(id);
        }
    }
}