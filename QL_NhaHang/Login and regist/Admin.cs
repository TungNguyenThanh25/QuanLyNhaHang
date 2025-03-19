using Demo_BCP;
using OfficeOpenXml;
using QL_NhaHang.Chức_năng_chính_để_quản_lý;
using QL_NhaHang.Method;
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

namespace QL_NhaHang.Login_and_regist
{
    public partial class Admin : Form
    {
        CRUD truyvan = new CRUD();
        public Admin()
        {
            InitializeComponent();
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
        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (isExiting)
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
            catch
            {
                
            }
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

        private void Admin_Load(object sender, EventArgs e)
        {
            timer_logout.Start();
            txt_NguoiDung.Text = $"Chào mừng bạn '{DangNhap.dangNhap}' quay trở lại";
        }

        //Vấn đề về timer khi tắt truy vấn và mở lại cái này
        private void timer_logout_Tick(object sender, EventArgs e)
        {
            try
            {
                //Lấy các dòng dữ liệu
                string query = $"exec SP_Select_UserSession '{DangNhap.dangNhap}'";
                SqlDataAdapter sda = new SqlDataAdapter(query, DatabaseConnection.con);
                DataTable dTable = new DataTable();
                sda.Fill(dTable);
                

                if (dTable.Rows.Count == 0)
                {
                    //timer_logout.Stop(); //kỳ?

                    //Login login = new Login();
                    //DatabaseConnection.con = null;
                    //this.DestroyHandle();
                    //login.Show();
                    btn_dangXuat.PerformClick();
                    MessageBox.Show($"Người dùng '{DangNhap.dangNhap}' đã đăng xuất", "Thông báo!");
                    
                    //Console.WriteLine("Stop the checking!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error---> " + ex.Message);
            }
            //Console.WriteLine("We are checking login status!");
        }

        private void btn_crud_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            this.DestroyHandle();
            truyvan.Show();

        }

        private void btn_import_export_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            ImportExport ie = new ImportExport();
            ie.Show();
            this.DestroyHandle();
        }

        private void btn_datMonAn_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            MenuChonMonAn menu = new MenuChonMonAn();
            menu.Show();
            this.DestroyHandle();
        }

        private void btn_duyetMonAn_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            DuyetMonAnKhachHangChon duyet = new DuyetMonAnKhachHangChon();
            duyet.Show();
            this.DestroyHandle();
        }

        private void btn_XemDoanhThu_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            XemDoanhThu xem = new XemDoanhThu();
            xem.Show();
            this.DestroyHandle();
        }

        private void btn_phanQuyen_Click(object sender, EventArgs e)
        {
            timer_logout.Stop();
            PhanQuyen phanQuyen = new PhanQuyen();
            phanQuyen.Show();
            this.DestroyHandle();
        }
    }
}
