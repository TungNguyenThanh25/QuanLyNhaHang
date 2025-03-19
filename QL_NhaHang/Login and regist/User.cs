using QL_NhaHang.Chức_năng_chính_để_quản_lý;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QL_NhaHang.Login_and_regist
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        private void btn_kiemTraKetNoi_Click(object sender, EventArgs e)
        {
            // Get the connection information
            string dataSource = DatabaseConnection.con.DataSource;
            string database = DatabaseConnection.con.Database;
            string connectionStringUsed = DatabaseConnection.con.ConnectionString;
            //string serverVersion = DatabaseConnection.con.ServerVersion;
            string state = DatabaseConnection.con.State.ToString();

            // Prepare the message to display
            string message = $"Data Source: {dataSource}\n" +
                                $"Database: {database}\n" +
                                $"Connection String: {connectionStringUsed}\n" +

                                $"Connection State: {state}";

            MessageBox.Show(message, "Connecting information");
        }

        private void btn_dangXuat_Click(object sender, EventArgs e)
        {
            //Dừng time lại
            timer_logout.Stop();

            //Xoá dữ liệu database của tài khoản đăng nhập sau khi đăng xuất
            string query = $"exec SP_Delete_UserSession '{DangNhap.dangNhap}'";
            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            cmd.ExecuteNonQuery();

            //Thực hiện ngắt kết nối và chuyển về trang Login
            Login login = new Login();
            DatabaseConnection.con = null;
            this.DestroyHandle();
            login.Show();

            
            ///Console.WriteLine("Stop the checking!");
        }

        bool isExiting = false;
        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isExiting)
            {
                return;
            }

            DialogResult r = MessageBox.Show("Bạn có chắc chắn không?", "Đóng toàn bộ chương trình", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                //Xoá dữ liệu UserID
                string query = $"exec SP_Delete_UserSession '{DangNhap.dangNhap}'";
                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                cmd.ExecuteNonQuery();

                isExiting = true;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            timer_logout.Start();
            txt_NguoiDung.Text = $"Chào mừng bạn '{DangNhap.dangNhap}' quay trở lại";
        }

        private void timer_logout_Tick(object sender, EventArgs e)
        {
            try
            {
                //Thủ tục đọc dữ liệu
                string query = $"exec SP_Select_UserSession '{DangNhap.dangNhap}'";
                SqlDataAdapter sda = new SqlDataAdapter(query, DatabaseConnection.con);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);

                if (dTable.Rows.Count == 0)
                {
                    //timer_logout.Stop();
                    //Login login = new Login();
                    //DatabaseConnection.con = null;
                    //login.Show();
                    //this.DestroyHandle();
                    btn_dangXuat.PerformClick();
                    MessageBox.Show($"Người dùng '{DangNhap.dangNhap}' đã đăng xuất", "Thông báo!");
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error---> " + ex.Message);
                timer_logout.Stop();
                MessageBox.Show("Tài khoản của bạn đã bị xoá");
                Login login = new Login();
                login.Show();
                this.DestroyHandle();
            }
            //Console.WriteLine("We are checking login status!");
        }

        private void btn_datMonAn_Click(object sender, EventArgs e)
        {
            try
            {
                //Hàm bắt lỗi khi người dùng không được cấp quyền
                string query = "exec SP_Select_MonAn_LoaiMon"; // Lấy đại dữ liệu cần bắt lỗi mà có tương tác tới người dùng
                using (SqlCommand cmd = new SqlCommand(query,DatabaseConnection.con))
                {
                    cmd.ExecuteNonQuery();
                }

                timer_logout.Stop();
                MenuChonMonAn menu = new MenuChonMonAn();
                menu.Show();
                this.DestroyHandle();
            }
            catch
            {
                MessageBox.Show("Không thể thao tác đặt món khi người dùng không được cấp quyền");
            }
        }
    }
}
