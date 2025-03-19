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
using QL_NhaHang.Login_and_regist;

namespace QL_NhaHang.Chức_năng_chính_để_quản_lý
{
    public partial class XemDoanhThu : Form
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public XemDoanhThu()
        {
            InitializeComponent();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AllowUserToAddRows = false;
        }

        private void XemDoanhThu_Load(object sender, EventArgs e)
        {
            //Thêm dữ liệu tháng vào combo Box
            string[] s = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            cbo_thang.Items.AddRange(s);
        }

        private void btn_loc_Click(object sender, EventArgs e)
        {
            try
            {
                string thang = cbo_thang.Text;
                string nam = txt_nam.Text.Trim();

                if (thang != "" && nam != "")
                {
                    //Hiện thông báo tổng doanh thu của tháng
                    cmd = new SqlCommand($"exec SP_MonthlyReport {thang}, {nam}", DatabaseConnection.con);
                    cmd.ExecuteNonQuery();

                    //Hiện thông tin dataGrid doanh thu theo tháng
                    cmd = new SqlCommand($"select * from dbo.FUNC_LietKeHoaDon_TheoThang({thang}, {nam})", DatabaseConnection.con);
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Dữ liệu nhập ngày tháng rỗng");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void XemDoanhThu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.DestroyHandle();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
