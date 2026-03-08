using System;
using System.Collections.Generic;
using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{
    public class LopHocPhanService
    {
        private readonly LopHocPhanDao _lopHocPhanDao = new LopHocPhanDao();
        //Lấy tất cả lớp học phần
        public List<LopHocPhan> LayTatCaLopHoc()
        {
            return _lopHocPhanDao.GetAll();
        }

        //Lấy danh sách lớp học theo giảng viên
        public List<LopHocPhan> LayLopTheoGiangVien(int gvId)
        {
            List<LopHocPhan> all = _lopHocPhanDao.GetAll();
            List<LopHocPhan> result = new List<LopHocPhan>();
            foreach (var item in all)
            {
                if (item.gv_id == gvId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        // Thêm lớp học mới
        public bool ThemLopHoc(LopHocPhan lhp)
        {
            // Kiểm tra xem mã lớp đã tồn tại chưa để tránh trùng lặp
            if (_lopHocPhanDao.GetByClassId(lhp.class_id) != null)
            {
                return false;
            }
            return _lopHocPhanDao.Insert(lhp);
        }

        //Cập nhật thông tin lớp học
        public bool CapNhatLopHoc(LopHocPhan lhp)
        {
            return _lopHocPhanDao.Update(lhp);
        }

        // Xóa lớp học
        public bool XoaLopHoc(string classId)
        {
            return _lopHocPhanDao.Delete(classId);
        }

        // Tìm lớp học theo mã lớp
        public LopHocPhan TimTheoMaLop(string classId)
        {
            return _lopHocPhanDao.GetByClassId(classId);
        }
    }
}