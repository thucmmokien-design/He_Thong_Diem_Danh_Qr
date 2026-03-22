using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class GiangVienDao
    {
        public List<GiangVien> GetAll()
        {
            List<GiangVien> list = new List<GiangVien>();

            string sql = "SELECT * FROM giang_vien";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GiangVien gv = new GiangVien
                    {
                        gv_id = Convert.ToInt32(reader["gv_id"]),
                        hoten = reader["hoten"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    };

                    list.Add(gv);
                }
            }

            return list;
        }
        public bool Update(GiangVien gv)
        {
            string sql = "UPDATE giang_vien SET password=@password " +
                         "WHERE email=@email";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", gv.email);
                cmd.Parameters.AddWithValue("@password", gv.password);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
