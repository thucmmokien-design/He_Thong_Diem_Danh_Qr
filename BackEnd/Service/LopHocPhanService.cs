using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class LopHocPhanService
    {
        private readonly LopHocPhanDao _lopHocPhanDao = new LopHocPhanDao();
        public List<LopHocPhan> LayTatCaLopHoc(int gv_id)
        {
            return _lopHocPhanDao.GetAll(gv_id);
        }
        public LopHocPhan TimTheoMaLop(string classId)
        {
            return _lopHocPhanDao.GetByClassId(classId);
        }
    }
}