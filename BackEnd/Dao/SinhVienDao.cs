using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class SinhVienDao
    {
        // lay danh sach sinh vien
        public List<SinhVien> GetAll()
        {
            List<SinhVien> list = new List<SinhVien>();

            string sql = "SELECT * FROM sinh_vien";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SinhVien sv = new SinhVien
                    {
                        msv = reader["msv"].ToString(),
                        hoten = reader["hoten"].ToString(),
                        email = reader["email"].ToString(),
                        password = reader["password"].ToString()
                    };

                    list.Add(sv);
                }
            }

            return list;
        }

        // test
        //SinhVienDao dao = new SinhVienDao();

        //List<SinhVien> list = dao.GetAll();

        //foreach (SinhVien sv in list)
        //{
        //    Console.WriteLine(sv.hoten);
        //}

        // them sinh vien
        public bool Insert(SinhVien sv)
        {
            string sql = "INSERT INTO sinh_vien(msv,hoten,email,password) " +
                         "VALUES(@msv,@hoten,@email,@password)";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@msv", sv.msv);
                cmd.Parameters.AddWithValue("@hoten", sv.hoten);
                cmd.Parameters.AddWithValue("@email", sv.email);
                cmd.Parameters.AddWithValue("@password", sv.password);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // cap nhat sinh vien
        public bool Update(SinhVien sv)
        {
            string sql = "UPDATE sinh_vien " +
                         "SET hoten=@hoten,email=@email,password=@password " +
                         "WHERE msv=@msv";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@msv", sv.msv);
                cmd.Parameters.AddWithValue("@hoten", sv.hoten);
                cmd.Parameters.AddWithValue("@email", sv.email);
                cmd.Parameters.AddWithValue("@password", sv.password);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // xoa sinh vien
        public bool Delete(string msv)
        {
            string sql = "DELETE FROM sinh_vien " +
                         "WHERE msv=@msv";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@msv", msv);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // lay sinh vien theo msv
        public SinhVien GetById(string msv)
        {
            string sql = "SELECT * FROM sinh_vien " +
                         "WHERE msv=@msv";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@msv", msv);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new SinhVien
                    {
                        msv = reader["msv"].ToString(),
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
