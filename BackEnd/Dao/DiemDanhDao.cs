using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class DiemDanhDao
    {
        // lay danh sach diem danh
        public List<DiemDanh> GetAll()
        {
            List<DiemDanh> list = new List<DiemDanh>();

            string sql = "SELECT * FROM diem_danh";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DiemDanh dd = new DiemDanh
                    {
                        id = Convert.ToInt32(reader["id"]),
                        msv = reader["msv"].ToString(),
                        session_id = Convert.ToInt32(reader["session_id"]),
                        checkin_time = Convert.ToDateTime(reader["checkin_time"]),
                        status = reader["status"].ToString()
                    };

                    list.Add(dd);
                }
            }

            return list;
        }

        // them diem danh
        public bool Insert(DiemDanh dd)
        {
            string sql = "INSERT INTO diem_danh(msv,session_id,checkin_time,status) " +
                         "VALUES(@msv,@session_id,@checkin_time,@status)";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@msv", dd.msv);
                cmd.Parameters.AddWithValue("@session_id", dd.session_id);
                cmd.Parameters.AddWithValue("@checkin_time", dd.checkin_time);
                cmd.Parameters.AddWithValue("@status", dd.status);

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

        // xoa diem danh
        public bool Delete(int id)
        {
            string sql = "DELETE FROM diem_danh " +
                         "WHERE id=@id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // lay danh sach diem danh theo id
        public DiemDanh GetById(int id)
        {
            string sql = "SELECT * FROM diem_danh " +
                         "WHERE id=@id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new DiemDanh
                    {
                        id = Convert.ToInt32(reader["id"]),
                        msv = reader["msv"].ToString(),
                        session_id = Convert.ToInt32(reader["session_id"]),
                        checkin_time = Convert.ToDateTime(reader["checkin_time"]),
                        status = reader["status"].ToString()
                    };
                }
            }
            return null;
        }
    }
}
