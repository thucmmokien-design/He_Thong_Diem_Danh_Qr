using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class DiemDanhDao
    {
        public List<DiemDanh> GetAll()
        {
            List<DiemDanh> list = new List<DiemDanh>();
            DateTime time = DateTime.Today;
            string sql = "SELECT \r\n    sv.msv,  \r\n    sv.hoten,\r\n    sv.email,\r\n    bh.class_id,\r\n    dd.checkin_time,\r\n    dd.status\r\nFROM diem_danh dd\r\nINNER JOIN sinh_vien sv \r\n    ON dd.msv = sv.msv\r\nINNER JOIN buoi_hoc bh \r\n    ON dd.session_id = bh.session_id\r\nINNER JOIN lop_hoc_phan lhp \r\n    ON bh.class_id = lhp.class_id\r\nWHERE CAST(dd.checkin_time AS DATE) = @time;";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", time);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DiemDanh dd = new DiemDanh
                    {
                        hoten = reader["hoten"].ToString(),
                        msv = reader["msv"].ToString(),
                        class_id = reader["class_id"].ToString(),
                        email = reader["email"].ToString(),
                        checkin_time = Convert.ToDateTime(reader["checkin_time"]),
                        status = reader["status"].ToString()
                    };
                    list.Add(dd);
                }
            }

            return list;
        }
        public List<DiemDanh> getChonLoc(string classId, string status)
        {
            List<DiemDanh> list = new List<DiemDanh>();
            DateTime date = DateTime.Today;

            string sql = @"SELECT 
                        sv.msv,
                        sv.hoten,
                        sv.email,
                        bh.class_id,
                        dd.checkin_time,
                        dd.status
                    FROM diem_danh dd
                    INNER JOIN sinh_vien sv ON dd.msv = sv.msv
                    INNER JOIN buoi_hoc bh ON dd.session_id = bh.session_id
                    INNER JOIN lop_hoc_phan lhp ON bh.class_id = lhp.class_id
                    WHERE bh.class_id = @class_id 
                        AND dd.status = @status
                        AND CAST(dd.checkin_time AS DATE) = @date";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@class_id", classId);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@date", date);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DiemDanh dd = new DiemDanh
                    {
                        hoten = reader["hoten"].ToString(),
                        msv = reader["msv"].ToString(),
                        class_id = reader["class_id"].ToString(),
                        email = reader["email"].ToString(),
                        checkin_time = Convert.ToDateTime(reader["checkin_time"]),
                        status = reader["status"].ToString()
                    };

                    list.Add(dd);
                }
            }

            return list;
        }

        // them diem danh
        public bool InsertTTBanDau(int idbuoihoc, SinhVien sv)
        {
            string sql = "INSERT INTO diem_danh(msv,session_id,status) " +
                         "VALUES(@msv,@session_id,@status)";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@msv", sv.msv);
                cmd.Parameters.AddWithValue("@session_id", idbuoihoc);
                cmd.Parameters.AddWithValue("@status", "Vắng");

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // cap nhat diem danh
        public bool Update(DiemDanh dd)
        {
            string sql = "UPDATE diem_danh " +
                         "SET msv=@msv,session_id=@session_id,checkin_time=@checkin_time,status=@status " +
                         "WHERE id=@id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@msv", dd.msv);
                cmd.Parameters.AddWithValue("@session_id", dd.session_id);
                cmd.Parameters.AddWithValue("@checkin_time", dd.checkin_time);
                cmd.Parameters.AddWithValue("@status", dd.status);
                cmd.Parameters.AddWithValue("@id", dd.id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Kiểm tra sinh viên đã điểm danh chưa
        public bool KiemTraDaDiemDanh(int sessionId, string msv)
        {
            string sql = "SELECT COUNT(*) FROM diem_danh WHERE session_id = @session_id AND msv = @msv AND status != N'Vắng'";
            
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@msv", msv);
                
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public bool CapNhatDiemDanhQR(int sessionId, string msv)
        {
            // Kiểm tra đã điểm danh chưa
            if (KiemTraDaDiemDanh(sessionId, msv))
            {
                return false; // Đã điểm danh rồi
            }
            
            string sql = "UPDATE diem_danh SET status = N'Đã điểm danh', checkin_time = SYSDATETIME() WHERE session_id = @session_id AND msv = @msv AND status = N'Vắng'";
            
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@msv", msv);
                
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
