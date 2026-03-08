using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class LopHocPhanDao
    {
        // lay danh sach lop hoc phan
        public List<LopHocPhan> GetAll()
        {
            List<LopHocPhan> list = new List<LopHocPhan>();

            string sql = "SELECT * FROM lop_hoc_phan";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LopHocPhan lhp = new LopHocPhan
                    {
                        class_id = reader["class_id"].ToString(),
                        ten_mon = reader["ten_mon"].ToString(),
                        hoc_ky = reader["hoc_ky"].ToString(),
                        gv_id = Convert.ToInt32(reader["gv_id"])
                    };

                    list.Add(lhp);
                }
            }

            return list;
        }

        // them lop hoc phan
        public bool Insert(LopHocPhan lhp)
        {
            string sql = "INSERT INTO lop_hoc_phan(class_id,ten_mon,hoc_ky,gv_id) " +
                         "VALUES(@class_id,@ten_mon,@hoc_ky,@gv_id)";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@class_id", lhp.class_id);
                cmd.Parameters.AddWithValue("@ten_mon", lhp.ten_mon);
                cmd.Parameters.AddWithValue("@hoc_ky", lhp.hoc_ky);
                cmd.Parameters.AddWithValue("@gv_id", lhp.gv_id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // cap nhat lop hoc phan
        public bool Update(LopHocPhan lhp)
        {
            string sql = "UPDATE lop_hoc_phan " +
                         "SET ten_mon=@ten_mon,hoc_ky=@hoc_ky,gv_id=@gv_id " +
                         "WHERE class_id=@class_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@class_id", lhp.class_id);
                cmd.Parameters.AddWithValue("@ten_mon", lhp.ten_mon);
                cmd.Parameters.AddWithValue("@hoc_ky", lhp.hoc_ky);
                cmd.Parameters.AddWithValue("@gv_id", lhp.gv_id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // xoa lop hoc phan
        public bool Delete(string class_id)
        {
            string sql = "DELETE FROM lop_hoc_phan " +
                         "WHERE class_id=@class_id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@class_id", class_id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // lay lop hoc phan theo class_id
        public LopHocPhan GetByClassId(string class_id)
        {
            string sql = "SELECT * FROM lop_hoc_phan " +
                         "WHERE class_id=@class_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@class_id", class_id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new LopHocPhan
                    {
                        class_id = reader["class_id"].ToString(),
                        ten_mon = reader["ten_mon"].ToString(),
                        hoc_ky = reader["hoc_ky"].ToString(),
                        gv_id = Convert.ToInt32(reader["gv_id"])
                    };
                }
            }

            return null;
        }
    }
}
