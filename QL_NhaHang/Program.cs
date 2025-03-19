using Demo_BCP;
using QL_NhaHang.Chức_năng_chính_để_quản_lý;
using QL_NhaHang.Login_and_regist;
using QL_NhaHang.Method;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //string connectionString = $"Data Source={DatabaseConnection.IP_Address};Initial Catalog={DatabaseConnection.dataBase_Name};User ID=sa;Password=123456789;TrustServerCertificate=True";
            //DatabaseConnection.InitializeConnection(connectionString);

            //DatabaseConnection.con.Open();

            Application.Run(new Login());
        }
    }
}
