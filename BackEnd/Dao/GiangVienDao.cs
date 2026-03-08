using System;
using System.Collections.Generic;
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

        // them giang vien
        public bool Insert(GiangVien gv)
        {
            string sql = "INSERT INTO giang_vien(hoten,email,password) " +
                         "VALUES(@hoten,@email,@password)";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@hoten", gv.hoten);
                cmd.Parameters.AddWithValue("@email", gv.email);
                cmd.Parameters.AddWithValue("@password", gv.password);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // cap nhat giang vien
        public bool Update(GiangVien gv)
        {
            string sql = "UPDATE giang_vien SET hoten=@hoten, email=@email, password=@password " +
                         "WHERE gv_id=@gv_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@gv_id", gv.gv_id);
                cmd.Parameters.AddWithValue("@hoten", gv.hoten);
                cmd.Parameters.AddWithValue("@email", gv.email);
                cmd.Parameters.AddWithValue("@password", gv.password);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // xoa giang vien
        public bool Delete(int gv_id)
        {
            string sql = "DELETE FROM giang_vien " +
                         "WHERE gv_id=@gv_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@gv_id", gv_id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // lay giang vien theo id
        public GiangVien GetById(int gv_id)
        {
            string sql = "SELECT * FROM giang_vien " +
                         "WHERE gv_id=@gv_id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("@gv_id", gv_id);
                
                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    return new GiangVien
                    {
                        gv_id = Convert.ToInt32(reader["gv_id"]),
                        hoten = reader["hoten"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    };
                }
            }
            
            return null;
        }
    }
}
