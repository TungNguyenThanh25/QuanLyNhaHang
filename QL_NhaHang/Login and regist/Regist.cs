using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QL_NhaHang.Login_and_regist
{
    public partial class Regist : Form
    {

        SqlConnection con = new SqlConnection($"Data Source={DatabaseConnection.IP_Address};Initial Catalog={DatabaseConnection.dataBase_Name};User ID=ql_account;Password=123;TrustServerCertificate=True");
        public Regist()
        {
            InitializeComponent();
        }

        private async void btn_dangKy_ClickAsync(object sender, EventArgs e)
        {
            string dangNhap = txt_dangNhap.Text.Trim();
            string matKhau = txt_matKhau.Text.Trim();

            if(dangNhap != "" && matKhau !="")
            {
                try
                {
                    con.Open();

                    if (string.Compare(matKhau, txt_xacNhanMK.Text) == 0)
                    {
                        //Tạo user
                        SqlCommand cmd = new SqlCommand($"exec SP_Create_User '{dangNhap}', '{matKhau}', '{DatabaseConnection.dataBase_Name}'", con);
                        cmd.CommandTimeout = 180; // Giới hạn 5 giây
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand($"exec SP_Insert_TaiKhoan '{dangNhap}'", con);
                        cmd.CommandTimeout = 180; // Giới hạn 5 giây
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đăng ký thành công", "Success!");

                        Login login = new Login();
                        login.Show();
                        this.DestroyHandle();

                        con = null;

                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu xác nhận không chính xác!");
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Tài khoản {dangNhap} đã tồn tại.");
                    //con.Close();
                    MessageBox.Show(ex.Message);
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Fill in blank", "Error");
            }

        }

        bool isExiting = false;
        private void Regist_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isExiting)
            {
                return;
            }

            DialogResult r = MessageBox.Show("Bạn có chắc chắn không?", "Đóng toàn bộ chương trình", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                isExiting = true;
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void link_dangNhap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.DestroyHandle();
        }

        private void Regist_Load(object sender, EventArgs e)
        {
            txt_matKhau.UseSystemPasswordChar = true;
            txt_xacNhanMK.UseSystemPasswordChar = true;
        }

        private void check_showPassword_CheckedChanged(object sender, EventArgs e)
        {
            if(check_showPassword.Checked)
            {
                txt_matKhau.UseSystemPasswordChar = false;
                txt_xacNhanMK.UseSystemPasswordChar = false;
            }
            else
            {
                txt_matKhau.UseSystemPasswordChar = true;
                txt_xacNhanMK.UseSystemPasswordChar = true;
            }
        }

        private void txt_dangNhap_TextChanged(object sender, EventArgs e)
        {
            txt_dangNhap.Focus();
        }
    }
}
