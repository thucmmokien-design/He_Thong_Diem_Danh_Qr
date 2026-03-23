﻿﻿using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    class DiemQuaTrinhDao
    {
        public SinhVienDao sinhviendao = new SinhVienDao();
        public bool InsertLop(string class_id)
        {
            List<SinhVien> lssv = sinhviendao.GetAll();
            string sql = "INSERT INTO diem_qua_trinh (msv, class_id) VALUES (@msv, @class_id)";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                foreach (var sv in lssv)
                {
                    using (SqlCommand cd = new SqlCommand(sql, conn))
                    {
                        cd.Parameters.AddWithValue("@msv", sv.msv);
                        cd.Parameters.AddWithValue("@class_id", class_id);
                        cd.ExecuteNonQuery();
                    }
                }
                return true;
            }
        }
        public List<DiemQuaTrinh> getAll()
        {
            List<DiemQuaTrinh> list = new List<DiemQuaTrinh>();
            string sql = "SELECT \r\n    sv.hoten, \r\n    sv.msv, \r\n    lhp.class_id, \r\n    dqt.so_lan_len_bang, \r\n    dqt.diem_chuyen_can, \r\n    dqt.ghi_chu\r\n,  \r\ndqt.tong_so_buoi_vang\r\nFROM diem_qua_trinh dqt\r\nJOIN sinh_vien sv ON sv.msv = dqt.msv\r\nJOIN lop_hoc_phan lhp ON lhp.class_id = dqt.class_id;";
            using(SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) {
                    DiemQuaTrinh dqt = new DiemQuaTrinh {
                        hoten = dr["hoten"].ToString(),
                        msv = dr["msv"].ToString(),
                        class_id = dr["class_id"].ToString(),
                        so_lan_len_bang = Convert.ToInt32(dr["so_lan_len_bang"]),
                        diem_chuyen_can = Convert.ToSingle(dr["diem_chuyen_can"]),
                        tong_so_buoi_vang = Convert.ToInt32(dr["tong_so_buoi_vang"]),
                        ghi_chu = dr["ghi_chu"].ToString()
                    };
                    list.Add(dqt);
                }
            }
            return list;
        }
        public List<DiemQuaTrinh> searchName(String name)
        {
            List<DiemQuaTrinh> list = new List<DiemQuaTrinh>();
            string sql = "SELECT \r\n    sv.hoten, \r\n    sv.msv, \r\n    lhp.class_id, \r\n    dqt.so_lan_len_bang, \r\n    dqt.diem_chuyen_can, \r\n    dqt.ghi_chu\r\n,  \r\ndqt.tong_so_buoi_vang\r\nFROM diem_qua_trinh dqt\r\nJOIN sinh_vien sv ON sv.msv = dqt.msv\r\nJOIN lop_hoc_phan lhp ON lhp.class_id = dqt.class_id\r\nWHERE sv.hoten LIKE @name";
            using (SqlConnection conn = ConnectDB.GetConnection()) {
                conn.Open();
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read()) {
                    DiemQuaTrinh dqt = new DiemQuaTrinh
                    {
                        hoten = rd["hoten"].ToString(),
                        msv = rd["msv"].ToString(),
                        class_id = rd["class_id"].ToString(),
                        so_lan_len_bang = Convert.ToInt32(rd["so_lan_len_bang"]),
                        diem_chuyen_can = Convert.ToSingle(rd["diem_chuyen_can"]),
                        tong_so_buoi_vang = Convert.ToInt32(rd["tong_so_buoi_vang"]),
                        ghi_chu = rd["ghi_chu"].ToString()
                    };
                    list.Add(dqt);
                }
            }
            return list;
        }
        public List<DiemQuaTrinh> searchClass(String classId)
        {
            List<DiemQuaTrinh> list = new List<DiemQuaTrinh>();
            string sql = "SELECT \r\n    sv.hoten, \r\n    sv.msv, \r\n    lhp.class_id, \r\n    dqt.so_lan_len_bang, \r\n    dqt.diem_chuyen_can, \r\n    dqt.ghi_chu\r\n,  \r\ndqt.tong_so_buoi_vang\r\nFROM diem_qua_trinh dqt\r\nJOIN sinh_vien sv ON sv.msv = dqt.msv\r\nJOIN lop_hoc_phan lhp ON lhp.class_id = dqt.class_id\r\nWHERE lhp.class_id = @classId";
            using (SqlConnection conn = ConnectDB.GetConnection()) {
                conn.Open();
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@classId", classId);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read()) {
                    DiemQuaTrinh dqt = new DiemQuaTrinh
                    {
                        hoten = rd["hoten"].ToString(),
                        msv = rd["msv"].ToString(),
                        class_id = rd["class_id"].ToString(),
                        so_lan_len_bang = Convert.ToInt32(rd["so_lan_len_bang"]),
                        diem_chuyen_can = Convert.ToSingle(rd["diem_chuyen_can"]),
                        tong_so_buoi_vang = Convert.ToInt32(rd["tong_so_buoi_vang"]),
                        ghi_chu = rd["ghi_chu"].ToString()
                    };
                    list.Add(dqt);
                }
            }
            return list;
        }

        public List<DiemQuaTrinh> searchNameAndClass(String name, String classId)
        {
            List<DiemQuaTrinh> list = new List<DiemQuaTrinh>();
            string sql = "SELECT \r\n    sv.hoten, \r\n    sv.msv, \r\n    lhp.class_id, \r\n    dqt.so_lan_len_bang, \r\n    dqt.diem_chuyen_can, \r\n    dqt.ghi_chu\r\n, \r\ndqt.tong_so_buoi_vang\r\nFROM diem_qua_trinh dqt\r\nJOIN sinh_vien sv ON sv.msv = dqt.msv\r\nJOIN lop_hoc_phan lhp ON lhp.class_id = dqt.class_id\r\nWHERE sv.hoten LIKE @name AND lhp.class_id = @classId";
            using (SqlConnection conn = ConnectDB.GetConnection()) {
                conn.Open();
                SqlCommand cmd = new SqlCommand (sql, conn);
                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                cmd.Parameters.AddWithValue("@classId", classId);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read()) {
                    DiemQuaTrinh dqt = new DiemQuaTrinh
                    {
                        hoten = rd["hoten"].ToString(),
                        msv = rd["msv"].ToString(),
                        class_id = rd["class_id"].ToString(),
                        so_lan_len_bang = Convert.ToInt32(rd["so_lan_len_bang"]),
                        diem_chuyen_can = Convert.ToSingle(rd["diem_chuyen_can"]),
                        tong_so_buoi_vang = Convert.ToInt32(rd["tong_so_buoi_vang"]),
                        ghi_chu = rd["ghi_chu"].ToString()
                    };
                    list.Add(dqt);
                }
            }
            return list;
        }
        public bool upDiemQuaTrinh(string msv, string classId,  float diem )
        {
            string sql = "UPDATE diem_qua_trinh\r\nSET diem_chuyen_can = @diem\r\nWHERE msv = @msv AND class_id = @classId";
            using(SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@msv", msv);
                cmd.Parameters.AddWithValue("@diem", diem);
                cmd.Parameters.AddWithValue("@classId", classId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateSoLanLenBang(string msv, string classId, int soLanLenBang)
        {
            string sql = "UPDATE diem_qua_trinh SET so_lan_len_bang = @soLanLenBang WHERE msv = @msv AND class_id = @classId";
            
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@msv", msv);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@soLanLenBang", soLanLenBang);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateGhiChu(string msv, string classId, string ghiChu)
        {
            string sql = "UPDATE diem_qua_trinh SET ghi_chu = @ghiChu WHERE msv = @msv AND class_id = @classId"; 
            
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@msv", msv);
                cmd.Parameters.AddWithValue("@classId", classId);
                cmd.Parameters.AddWithValue("@ghiChu", ghiChu ?? "");
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<DiemDanh> tongSoBuoiVangSinhVien()
        {
            List<DiemDanh> list = new List<DiemDanh>();
            List<SinhVien> sinhVienList = sinhviendao.GetAll();
            string sql = "SELECT COUNT(*) FROM diem_danh WHERE msv = @msv AND status = @vang";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                foreach (var sv in sinhVienList)
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@msv", sv.msv);
                    cmd.Parameters.AddWithValue("@vang", "Vắng");

                    int count = (int)cmd.ExecuteScalar();

                    DiemDanh dsdd = new DiemDanh
                    {
                        msv = sv.msv,
                        sobuoivang = count
                    };
                    list.Add(dsdd);
                }
            }
            return list;
        }
        public bool upDateSoBuoiVang(DiemDanh diemDanh)
        {
            string sql = "UPDATE diem_qua_trinh\r\nSET tong_so_buoi_vang = @sobuoivang\r\nWHERE msv = @msv";
            using(SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand (sql, conn);
                command.Parameters.AddWithValue("@msv", diemDanh.msv);
                command.Parameters.AddWithValue("@sobuoivang", diemDanh.sobuoivang);
                return  command.ExecuteNonQuery() > 0;
            }
        }
    }
}
