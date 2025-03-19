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
using ExcelDataReader.Log;

namespace QL_NhaHang.Method
{
    public partial class Xem : Form
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Xem()
        {
            InitializeComponent();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AllowUserToAddRows = false;
        }

        //Các chức năng hỗ trợ-------------------------------------------------------------


        //Form-----------------------------------------------------------------------------

            

        private void TruyVanDuLieu_Load(object sender, EventArgs e)
        {
            string[] insert_cbo = null;
            string tableName = TableName.tableName;

            switch(tableName)
            {
                case "HoaDon":
                    //Đặt tên
                    radioButton1.Text = "Liệt kê các hóa đơn và món ăn được đặt cho từng khách hàng";
                    groupBox1.Text = "Doanh thu";

                    //Thêm dữ liệu tháng vào combo Box
                    insert_cbo = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
                    cbo_thang.Items.AddRange(insert_cbo);

                    //Ẩn các nút còn lại
                    radioButton2.Visible = false;
                    break;

                case "MonAn":
                    //Đặt tên cho radioButton1
                    radioButton1.Text = "Liệt kê các món ăn chưa bao giờ được đặt";

                    //Ẩn các nút còn lại
                    radioButton2.Visible = false;
                    groupBox1.Visible = false;
                    break;

                case "KhachHang":
                    //Đặt tên cho radioButton1
                    radioButton1.Text = "Liệt kê các khách hàng có tổng số tiền đã chi lớn hơn mức trung bình";

                    //Ẩn các nút còn lại
                    radioButton2.Visible = false;
                    groupBox1.Visible = false;
                    break;

                case "NhanVien":
                    //Đặt tên cho radioButton1
                    radioButton1.Text = "Lấy ra danh sách tất cả các nhân viên và các nhân viên quản lý họ";

                    //Ẩn các nút còn lại
                    radioButton2.Visible = false;
                    groupBox1.Visible = false;
                    break;

                case "DatBan":
                    //Đặt tên cho radioButton1
                    radioButton1.Text = "Liệt kê các bàn được đặt trong ngày hôm nay cùng với khách hàng đặt";

                    //Ẩn các nút còn lại
                    radioButton2.Visible = false;
                    groupBox1.Visible = false;
                    break;
            }
            
        }



        private void TruyVanDuLieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DestroyHandle();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //cmd = new SqlCommand("select * from dbo.FUNC_LietKeNhanVienCoQuanLyTrucTiep()", DatabaseConnection.con);
            //sda = new SqlDataAdapter(cmd);
            //dt = new DataTable();
            //sda.Fill(dt);

            //dataGridView1.DataSource = dt;
            if(radioButton1.Checked)
            {
                txt_nam.Text = "";
                cbo_thang.SelectedItem = null;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //cmd = new SqlCommand("select * from dbo.FUNC_LietKe_HoaDon_Va_MonAn()", DatabaseConnection.con);
            //sda = new SqlDataAdapter(cmd);
            //dt = new DataTable();
            //sda.Fill(dt);

            //dataGridView1.DataSource = dt;
        }

        private void btn_loc_Click(object sender, EventArgs e)
        {
            string tableName = TableName.tableName;
            switch(tableName)
            {
                case "HoaDon":
                    {
                        string thang = cbo_thang.Text;
                        string nam = txt_nam.Text.Trim();

                        if (thang != "" && nam != "")
                        {
                            //Hiện thông báo tổng tiền trong tháng
                            cmd = new SqlCommand($"exec SP_MonthlyReport {thang}, {nam}", DatabaseConnection.con);
                            cmd.ExecuteNonQuery();

                            //Liệt kê thông tin liên quan về tiền trong tháng
                            cmd = new SqlCommand($"select * from dbo.FUNC_LietKeHoaDon_TheoThang({thang}, {nam})", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                        else if (radioButton1.Checked)
                        {
                            cmd = new SqlCommand($"select * from dbo.FUNC_LietKe_HoaDon_Va_MonAn()", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu nhập bị rỗng");
                        }
                    }
                    break;
                case "MonAn":
                    {
                        if(radioButton1.Checked)
                        {
                            cmd = new SqlCommand($"select * from dbo.FUNC_MonAnChuaDuocDat()", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);
                            
                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu nhập bị rỗng");
                        }
                    }
                    break;
                case "KhachHang":
                    {
                        if (radioButton1.Checked)
                        {
                            cmd = new SqlCommand($"select * from dbo.FUNC_KhachHang_ChiNhieuHonTrungBinh()", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu nhập bị rỗng");
                        }
                    }
                    break;
                case "NhanVien":
                    {
                        if(radioButton1.Checked)
                        {
                            cmd = new SqlCommand($"select * from dbo.FUNC_LietKeNhanVienQuanLy()", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu nhập bị rỗng");
                        }
                    }
                    break;

                case "DatBan":
                    {
                        if (radioButton1.Checked)
                        {
                            cmd = new SqlCommand($"select * from dbo.FUNC_LietKeBanDatHomNay()", DatabaseConnection.con);
                            sda = new SqlDataAdapter(cmd);
                            dt = new DataTable();
                            sda.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                        else
                        {
                            MessageBox.Show("Dữ liệu nhập bị rỗng");
                        }
                    }
                    break;
            }
        }

        private void cbo_thang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbo_thang.Text != "")
            {
                radioButton1.Checked = false;
            }
        }

        private void txt_nam_TextChanged(object sender, EventArgs e)
        {
            if(txt_nam.Text.Trim() != "")
            {
                radioButton1.Checked = false;
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
