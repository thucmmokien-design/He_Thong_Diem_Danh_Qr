using System;
using System.Collections.Generic;
using System.Windows.Media.Converters;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class DiemDanhService
    {
        private readonly DiemDanhDao _diemDanhDao = new DiemDanhDao();
        public List<DiemDanh> LayDanhSach()
        {
            return _diemDanhDao.GetAll();
        }
        public List<DiemDanh> TronLoc(string class_id, string status)
        {
            return _diemDanhDao.getChonLoc(class_id, status);
        }
        public void TaoDanhSachDiemDanh(string classId, List<SinhVien> dsSinhVien)
        {
            BuoiHocDao buoiHocDao = new BuoiHocDao();
            List<BuoiHoc> dsBuoiHoc = buoiHocDao.GetAll();
            DateTime today = DateTime.Today;
            int sessionIdMoiNhat = 0;

            foreach (var bh in dsBuoiHoc)
            {
                if (bh.ngay_hoc.Date == today && bh.class_id == classId)
                {
                        sessionIdMoiNhat = bh.session_id;
                }
            }
            foreach (var sv in dsSinhVien)
            {
                _diemDanhDao.InsertTTBanDau(sessionIdMoiNhat, sv);
            }
        }

        public bool CapNhatDiemDanh(int sessionId, string msv)
        {
            return _diemDanhDao.CapNhatDiemDanhQR(sessionId, msv);
        }
    }
}