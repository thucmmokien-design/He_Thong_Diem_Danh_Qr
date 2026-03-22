using He_Thong_Diem_Danh_Qr.BackEnd.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class LopHocPhanDao
    {
        // lay danh sach lop hoc phan
        public List<LopHocPhan> GetAll(int gv_id)
        {
            List<LopHocPhan> list = new List<LopHocPhan>();

            string sql = "SELECT *\r\nFROM lop_hoc_phan lhp\r\nINNER JOIN giang_vien gv ON lhp.gv_id = gv.gv_id\r\nWHERE gv.gv_id = @gv_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gv_id",gv_id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    LopHocPhan lhp = new LopHocPhan
                    {
                        class_id = reader["class_id"].ToString(),
                        ten_mon = reader["ten_mon"].ToString(),
                        hoc_ky = reader["hoc_ky"].ToString(),
                    };

                    list.Add(lhp);
                }
            }

            return list;
        }
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
