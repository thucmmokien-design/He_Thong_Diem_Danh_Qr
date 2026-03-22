using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using Microsoft.Data.SqlClient;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    class DangKiLopDao
    {
        public List<DangKiLop> getAll()
        {
            List<DangKiLop> list = new List<DangKiLop>();
            string sql = "SELECT * FROM dang_ky_lop\r\n";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DangKiLop dkl = new DangKiLop
                    {
                        msv = reader["msv"].ToString(),
                        class_id = reader["class_id"].ToString(),
                    };
                    list.Add(dkl);
                }
                return list;
            }
        }
        public bool checkLopHocPhan(String class_id)
        {
            string sql = "SELECT COUNT(*) \r\nFROM dang_ky_lop\r\nWHERE class_id = @class_id;";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@class_id", class_id);
                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
    }
}
