using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class BuoiHocDao
    {
        // lay danh sach buoi hoc
        public List<BuoiHoc> GetAll()
        {
            List<BuoiHoc> list = new List<BuoiHoc>();

            string sql = "SELECT * FROM buoi_hoc";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BuoiHoc bh = new BuoiHoc
                    {
                        session_id = Convert.ToInt32(reader["session_id"]),
                        class_id = reader["class_id"].ToString(),
                        ngay_hoc = Convert.ToDateTime(reader["ngay_hoc"]),
                        start_time = (TimeSpan)reader["start_time"],
                        end_time = (TimeSpan)reader["end_time"],
                        qr_secret = reader["qr_secret"].ToString(),
                        is_active = Convert.ToBoolean(reader["is_active"])
                    };

                    list.Add(bh);
                }
            }

            return list;
        }

        // them buoi hoc
        public bool Insert(BuoiHoc bh)
        {
            string sql = "INSERT INTO buoi_hoc(class_id,ngay_hoc,start_time,end_time,qr_secret,is_active) " +
                         "VALUES(@class_id,@ngay_hoc,@start_time,@end_time,@qr_secret,@is_active)";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@class_id", bh.class_id);
                cmd.Parameters.AddWithValue("@ngay_hoc", bh.ngay_hoc);
                cmd.Parameters.AddWithValue("@start_time", bh.start_time);
                cmd.Parameters.AddWithValue("@end_time", bh.end_time);
                cmd.Parameters.AddWithValue("@qr_secret", bh.qr_secret);
                cmd.Parameters.AddWithValue("@is_active", bh.is_active);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // cap nhat buoi hoc
        public bool Update(BuoiHoc bh)
        {
            string sql = "UPDATE buoi_hoc " +
                         "SET class_id=@class_id,ngay_hoc=@ngay_hoc,start_time=@start_time,end_time=@end_time,qr_secret=@qr_secret,is_active=@is_active " +
                         "WHERE session_id=@session_id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@session_id", bh.session_id);
                cmd.Parameters.AddWithValue("@class_id", bh.class_id);
                cmd.Parameters.AddWithValue("@ngay_hoc", bh.ngay_hoc);
                cmd.Parameters.AddWithValue("@start_time", bh.start_time);
                cmd.Parameters.AddWithValue("@end_time", bh.end_time);
                cmd.Parameters.AddWithValue("@qr_secret", bh.qr_secret);
                cmd.Parameters.AddWithValue("@is_active", bh.is_active);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // xoa buoi hoc
        public bool Delete(int session_id)
        {
            string sql = "DELETE FROM buoi_hoc " +
                         "WHERE session_id=@session_id";
            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@session_id", session_id);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // lay buoi hoc theo id
        public BuoiHoc GetById(int session_id)
        {
            string sql = "SELECT * FROM buoi_hoc " +
                         "WHERE session_id=@session_id";

            using (SqlConnection conn = ConnectDB.GetConnection())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@session_id", session_id);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new BuoiHoc
                    {
                        session_id = Convert.ToInt32(reader["session_id"]),
                        class_id = reader["class_id"].ToString(),
                        ngay_hoc = Convert.ToDateTime(reader["ngay_hoc"]),
                        start_time = (TimeSpan)reader["start_time"],
                        end_time = (TimeSpan)reader["end_time"],
                        qr_secret = reader["qr_secret"].ToString(),
                        is_active = Convert.ToBoolean(reader["is_active"])
                    };
                }
            }

            return null;
        }
    }
}
