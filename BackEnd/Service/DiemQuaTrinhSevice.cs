using He_Thong_Diem_Danh_Qr.BackEnd.Dao;
using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Service
{

    class DiemQuaTrinhSevice
    {
        DiemQuaTrinhDao diemquatrinhdao = new DiemQuaTrinhDao();
        public String themhocsinh(string class_id)
        {
            bool check = diemquatrinhdao.InsertLop(class_id);
            if (check)
            {
                return "Thêm Học Sinh Thành Công";
            }
            return "Thêm Học Sinh Thất Bại";
        }
        public List<DiemQuaTrinh> searchName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new List<DiemQuaTrinh>();
            }
            
            return diemquatrinhdao.searchName(name);
        }
        
        public List<DiemQuaTrinh> searchClass(string classId)
        {
            if (string.IsNullOrWhiteSpace(classId))
            {
                return new List<DiemQuaTrinh>();
            }
            
            return diemquatrinhdao.searchClass(classId);
        }
        
        public List<DiemQuaTrinh> searchNameAndClass(string name, string classId)
        {
            return diemquatrinhdao.searchNameAndClass(name, classId);
        }
        
        public List<DiemQuaTrinh> LayTatCaDiemQuaTrinh()
        {
            return diemquatrinhdao.getAll();
        }
        public string updateDiem(string name ,string class_id, float diem)
        {
            bool check = diemquatrinhdao.upDiemQuaTrinh(name, class_id, diem);
            if (check)
            {
                return "Update Điểm Thành Công";
            }
            return "";
        }
        public string CapNhatSoLanLenBang(string msv, string classId, int soLanLenBang)
        {
            bool check = diemquatrinhdao.UpdateSoLanLenBang(msv, classId, soLanLenBang);
            if (check)
            {
                return "Cập Nhật Số Lần Lên Bảng Thành Công";
            }
            return "Cập Nhật Số Lần Lên Bảng Thất Bại";
        }

        public string CapNhatGhiChu(string msv, string classId, string ghiChu)
        {
            bool check = diemquatrinhdao.UpdateGhiChu(msv, classId, ghiChu);
            if (check)
            {
                return "Cập Nhật Ghi Chú Thành Công";
            }
            return "Cập Nhật Ghi Chú Thất Bại";
        }
        public void updateDanhSachVang()
        {
            List<DiemDanh> danhsachdiemdanh = diemquatrinhdao.tongSoBuoiVangSinhVien();
            foreach(DiemDanh ds in danhsachdiemdanh)
            {
                diemquatrinhdao.upDateSoBuoiVang(ds);
            }
        }
    }
}
