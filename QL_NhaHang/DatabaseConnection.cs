using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Nơi mà liên quan đến chuỗi kết nối (Connection string) mà các thông tin khác có sự thay đổi
/*
Thay đổi
    + Data Source (Server)
    + Initial Catalog (Database)
 */
namespace QL_NhaHang
{
    static class DatabaseConnection
    {
        public static SqlConnection con = null;

        public static string IP_Address = "26.219.81.74"; // Server name
        
        public static string dataBase_Name = "QL_NHAHANG"; // Database

        // Khởi tạo chuỗi kết nối
        public static void InitializeConnection(string connectionString)
        {
            con = new SqlConnection(connectionString);
            con.InfoMessage += new SqlInfoMessageEventHandler(OnInfoMessage);// Lấy dữ liệu print trong sql khi xử lý
        }

        // Hiện thông báo tự động khi bên sql có print
        private static void OnInfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
