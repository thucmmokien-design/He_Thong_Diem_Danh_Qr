using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace He_Thong_Diem_Danh_Qr.BackEnd.Dao
{
    public class ConnectDB
    {
        private static string connectionString =
            "Server=QuocLui\\SQLEXPRESS;" +
            "Database=qr_attendance_system;" + 
            "Integrated Security=True;" +
            "TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
