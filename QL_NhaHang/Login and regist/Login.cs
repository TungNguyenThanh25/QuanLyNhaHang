using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using QL_NhaHang.Login_and_regist;

namespace QL_NhaHang
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (txt_DangNhap.Text != "" && txt_MatKhau.Text != "")
            {
                DangNhap.dangNhap = txt_DangNhap.Text.Trim(); // để dữ liệu dạng public để xử lý tabs
                DangNhap.matKhau = txt_MatKhau.Text;
                string connectionString = $"Data Source={DatabaseConnection.IP_Address};Initial Catalog={DatabaseConnection.dataBase_Name};User ID={DangNhap.dangNhap};Password={DangNhap.matKhau};TrustServerCertificate=True";
                DatabaseConnection.InitializeConnection(connectionString);
                try
                {
                    DatabaseConnection.con.Open();
                    //Sau khi đăng nhập thành công thì tiếp theo là sẽ lưu dữ liệu User vào trong database
                    string query = $"exec SP_Insert_UserSession '{DangNhap.dangNhap}'";
                    SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                    cmd.ExecuteNonQuery();

                    //Phân quyền user
                    if (string.Compare(DangNhap.dangNhap, "sa", true) == 0)
                    {
                        Form ad = new Admin();
                        ad.Show();
                        this.Hide();
                    }
                    else
                    {
                        User us = new User();
                        us.Show();
                        this.Hide();
                    }

                    ///DatabaseConnection.con.Close();

                    ////Kiểm tra kết nối
                    //if (DatabaseConnection.con.State == ConnectionState.Open)
                    //{
                    //    MessageBox.Show("Kết nối thành công");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Không có kết nối");
                    //}
                }
                catch (Exception ex)
                {
                    //Kiểm tra xem sự tồn tại hoặc mật khẩu của tài khoản
                    MessageBox.Show("Tài khoản hoặc mật khẩu chưa chính xác!!!");

                }
            }
            else
                MessageBox.Show("Fill in the blank!");
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void check_showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (check_showPassword.Checked)
            {
                txt_MatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txt_MatKhau.UseSystemPasswordChar = true;
            }
        }

        private void link_dangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Regist regist = new Regist();
            this.Hide();
            regist.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_MatKhau.UseSystemPasswordChar = true;
        }

        private void txt_DangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_kiemTraKetNoi_Click(object sender, EventArgs e)
        {
            if (DatabaseConnection.con != null)
            {
                // Get the connection information
                string dataSource = DatabaseConnection.con.DataSource;
                string database = DatabaseConnection.con.Database;
                string connectionStringUsed = DatabaseConnection.con.ConnectionString;
                ///string serverVersion = DatabaseConnection.con.ServerVersion;
                string state = DatabaseConnection.con.State.ToString();

                // Prepare the message to display
                string message = $"Data Source: {dataSource}\n" +
                                    $"Database: {database}\n" +
                                    $"Connection String: {connectionStringUsed}\n" +

                                    $"Connection State: {state}";

                MessageBox.Show(message, "Connecting information");
            }
            else
                MessageBox.Show("Không có dữ liệu kết nối", "Error");
        }

    }
}
