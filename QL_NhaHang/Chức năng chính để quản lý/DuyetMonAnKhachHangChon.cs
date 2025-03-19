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
    public partial class DuyetMonAnKhachHangChon : Form
    {
        SqlCommand cmd;
        SqlDataReader reader;
        SqlDataAdapter sda;
        DataTable dt;

        public DuyetMonAnKhachHangChon()
        {
            InitializeComponent();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AllowUserToAddRows = false;
        }
        //--------Load dữ liệu---------
        //Load mã khách hàng đang đợi đặt (Trường hợp rỗng là ẩn nút 'XemThongTinDatMon')
        void load_gridView_maKHDangDoiDat()
        {
            try
            {
                //Cho dữ liệu vào dataTable về thông tin khách hàng đang đợi đặt
                cmd = new SqlCommand("exec SP_Select_DuyetThucDon_TimKHDangDoiDatMon", DatabaseConnection.con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                    throw new Exception("Hiện tại chưa có khách hàng nào đặt!");//--> Catch lỗi trường hợp ko có dữ liệu gì hết
                dataGridView1.DataSource = dt; 

                //Load các thông tin khách hàng vào textBox
                txt_maKH.Text = dt.Rows[0][0].ToString();
                load_textBox_ThongTinKhachHang();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                btn_xemThongTinKHDatMon.Enabled = false;
            }
        }

        //Load thông tin khách hàng đang đợi đặt vào textBox
        void load_textBox_ThongTinKhachHang()
        {
            //Cho dữ liệu vào dataTable
            cmd = new SqlCommand($"exec SP_Select_KhachHang_MaKH '{txt_maKH.Text.Trim()}'", DatabaseConnection.con);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            //Load dữ liệu vào textBox
            txt_tenKH.Text = dt.Rows[0][1].ToString();
            txt_gioiTinh.Text = dt.Rows[0][2].ToString();
            txt_sdt.Text = dt.Rows[0][3].ToString();
            txt_email.Text = dt.Rows[0][4].ToString();
        }

        //Load danh sách món ăn mà khách hàng 'Đang đợi duyệt'
        void load_gridView_DSMonAnDangDoiDuyet()
        {
            try
            {
                //Tìm và thực hiện theo mã khách hàng
                cmd = new SqlCommand($"SP_Select_DuyetThucDon_XuatThongTinCanThiet_DangDoiDuyet '{txt_maKH.Text}'", DatabaseConnection.con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //--------Form chức năng---------
        //Load dữ liệu cần thiết
        private void DuyetMonAnKhachHangChon_Load(object sender, EventArgs e)
        {
            load_gridView_maKHDangDoiDat();

            //Trường hợp load dữ liệu rỗng thì ẩn hết tất cả các nút
            if(dataGridView1.Rows.Count == 0)
            {
                btn_xemThongTinKHDatMon.Enabled = false;
                btn_duyet.Enabled = false;
            }
        }

        //Mỗi lần click chuột vào dataGridView thì sẽ hiện thông tin khách hàng lên textBox
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //btn_xemThongTinKHDatMon.Enabled = true;
                if (e.RowIndex >= 0)
                {
                    if(btn_quayLai.Enabled == false)
                    {
                        int index = e.RowIndex;
                        txt_maKH.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                        load_textBox_ThongTinKhachHang();
                    }
                }
            }
            catch (Exception ex)
            {
                //Trường hợp ko có dữ liệu gì hết thì cho các textBox rỗng hết
                //MessageBox.Show(ex.Message);
                btn_xemThongTinKHDatMon.Enabled = false;
                txt_maKH.Text = "";
                txt_tenKH.Text = "";
                txt_gioiTinh.Text = "";
                txt_email.Text = "";
                txt_sdt.Text = "";
            }
        }

        private void btn_xemThongTinKHDatMon_Click(object sender, EventArgs e)
        {
            //Load dữ liệu gridView về danh sách các món ăn của khách hàng đã đặt thông qua mã KH
            load_gridView_DSMonAnDangDoiDuyet();

            //Ẩn nút 'Xem thông tin khách hàng đặt món'
            btn_xemThongTinKHDatMon.Enabled = false;

            //Hiện nút 'Duyệt thông tin' và 'Quay lại'
            btn_duyet.Enabled = true;
            btn_quayLai.Enabled = true;
        }

        private void btn_quayLai_Click(object sender, EventArgs e)
        {
            try
            {
                //Load dữ liệu khách hàng đang đợi đặt trở lại như ban đầu chạy
                load_gridView_maKHDangDoiDat();

                //Trường hợp load dữ liệu rỗng thì ẩn hết tất cả các nút và return như cũ
                if (dataGridView1.Rows.Count == 1) // Lý do bằng 1 thì nó đếm luôn cả cột rỗng
                {
                    btn_xemThongTinKHDatMon.Enabled = false;
                    btn_duyet.Enabled = false;
                    return;
                }

                //Hiện nút 'Xem thông tin khách hàng đặt món'
                btn_xemThongTinKHDatMon.Enabled = true;

                //Ẩn nút 'Duyệt thông tin'
                btn_duyet.Enabled = false;
                
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Sau tất cả thì cũng phải ẩn nút 'Quay lại'
                btn_quayLai.Enabled = false;
            }
        }

        private void btn_duyet_Click(object sender, EventArgs e)
        {
            try
            {
                //B1: Thêm mã khách hàng vào bảng DatMon với thời gian hiện tại (Mã đặt món tự tạo)
                cmd = new SqlCommand($"exec SP_Insert_DatMon_TheoThoiGianHienTai '{txt_maKH.Text}'", DatabaseConnection.con);
                cmd.ExecuteNonQuery();

                //B2: Thêm các món ăn vào ChiTietDatMon
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Kiểm tra hàng không phải là hàng trống
                    if (!row.IsNewRow)
                    {
                        //Thực hiện lấy dữ liệu từ dataGridView
                        int index = row.Index;
                        string maMon = dataGridView1.Rows[index].Cells[0].Value.ToString().Trim(); // Lấy mã món tại cột 0 dòng index
                        string tenMon = dataGridView1.Rows[index].Cells[1].Value.ToString().Trim(); // Lấy tên món tại cột 1 dòng index
                        int soLuong = int.Parse(dataGridView1.Rows[index].Cells[5].Value.ToString().Trim()); // Lấy số lượng tại cột 0 dòng index

                        //thực hiện thêm các món ăn vào mã đặt món trong ChiTietDatMon với mã đặt món mới nhất vừa tạo
                        cmd = new SqlCommand($"exec SP_Insert_ChiTietDatMon_ThemMonAnVoiMaDatMonMoiNhat '{maMon}', N'{tenMon}', {soLuong}", DatabaseConnection.con);
                        cmd.ExecuteNonQuery();
                    }
                }

                //B3: Thêm vào hoá đơn
                using (InputPhuongThucThanhToan inputForm = new InputPhuongThucThanhToan()) //Mở 1 form nhỏ để nhập thông tin 'Phương thức thanh toán'
                {
                    if (inputForm.ShowDialog()==DialogResult.OK)
                    {
                        // Nhận giá trị 'Phương thức thanh toán' từ form nhỏ thông qua thuộc tính UserInput
                        string phuongThucThanhToan = inputForm.UserInput.Trim();

                        //Thêm thông tin đó vào HoaDon
                        cmd = new SqlCommand($"exec SP_Insert_HoaDon_ThemMaDatMonMoiNhat N'{phuongThucThanhToan}'", DatabaseConnection.con);
                        cmd.ExecuteNonQuery();
                    }
                }

                //B4: Cập nhật trạng thái duyệt thực đơn khi nhân viên duyệt 'Đã duyệt' (Dùng khi nhân viên ấn nút 'Duyệt thông tin')
                string maKH = txt_maKH.Text.Trim();
                cmd = new SqlCommand($"exec SP_Update_DuyetThucDon_DaDuyet '{maKH}'", DatabaseConnection.con);
                cmd.ExecuteNonQuery();

                //B5: Ấn nút 'quay lại' tự động trở về mục chọn mã khách hàng
                btn_quayLai.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(btn_quayLai.Enabled == false)
            {
                load_gridView_maKHDangDoiDat();

                //Trường hợp load dữ liệu rỗng thì ẩn hết tất cả các nút
                if (dataGridView1.Rows.Count == 1) // Lý do bằng 1 thì nó đếm luôn cả tên cột
                {
                    btn_xemThongTinKHDatMon.Enabled = false;
                    btn_duyet.Enabled = false;
                }
                else
                {
                    //Chỉ hiện mỗi nút này vì hiện tại ở bảng load thông tin chọn mã khách hàng nên chỉ có nút này thực hiện duy nhất
                    //Mấy nút khác chỉ được phép hiện khi ấn nút 'Xem thông tin khách hàng đặt món'
                    btn_xemThongTinKHDatMon.Enabled = true;
                }
            }
        }

        private void DuyetMonAnKhachHangChon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Dispose();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
