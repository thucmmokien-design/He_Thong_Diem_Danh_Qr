using System;
using System.Collections.Generic;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class ConnectDB
    {
        private static string connectionString =
            "Server=localhost;" +
            "Database=DiemDanhQR;" + // ten database trong may
            "Integrated Security=True;" +
            "TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
