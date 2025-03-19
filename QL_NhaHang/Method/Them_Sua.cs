using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Collections;

namespace QL_NhaHang.Method
{
    public partial class Them_Sua : Form
    {
        SqlDataAdapter sda;
        DataTable dt;

        public Them_Sua()
        {
            InitializeComponent();
        }
        //Hàm hỗ trợ---------------------------------------------------------------------------------------------

        //Đổi kiểu dữ liệu thời gian phù hợp cho 'dateTimePicker'
        DateTime ParseDate(string dateTime)
        {
            
            // Step 1: Replace 'CH' with 'PM' and 'SA' with 'AM' (if needed)
            string cleanedString = dateTime.Replace("CH", "PM").Replace("SA", "AM");

            // Step 2: Parse the cleaned string into a DateTime object
            DateTime parsedDate = DateTime.ParseExact(cleanedString, "dd/MM/yyyy h:mm:ss tt",
                System.Globalization.CultureInfo.InvariantCulture);

            return parsedDate;
        }

        //Kiểm tra tính hợp lệ của Email
        bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for a valid email address
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);

            // Return true if the email matches the pattern, false otherwise
            return regex.IsMatch(email);
        }

        ////Load combo box về trạng thái của Ban (ko dùng được)
        //void load_cbo_ban_trangthai()
        //{
        //    string query = "exec SP_Select_Ban_TrangThai";
        //    sda = new SqlDataAdapter(query, DatabaseConnection.con);
        //    dt = new DataTable();
        //    sda.Fill(dt);

        //    cbo_trangThai.DataSource = dt;
        //    cbo_trangThai.DisplayMember = "trangthai";
        //}

        ////Load combo box về trạng thái của MonAn (ko dùng được)
        //void load_cbo_monan_trangthai()
        //{
        //    string query = "exec SP_Select_MonAn_TrangThai";
        //    sda = new SqlDataAdapter(query, DatabaseConnection.con);
        //    dt = new DataTable();
        //    sda.Fill(dt);

        //    cbo_trangThai.DataSource = dt;
        //    cbo_trangThai.DisplayMember = "trangthai";
        //}

        ////Load combo box về loại món của MonAn (ko dùng được)
        //void load_cbo_monan_loaimon()
        //{
        //    string query = "exec SP_Select_MonAn_LoaiMon";
        //    sda = new SqlDataAdapter(query, DatabaseConnection.con);
        //    dt = new DataTable();
        //    sda.Fill(dt);

        //    cbo_loaiMon.DataSource = dt;
        //    cbo_loaiMon.DisplayMember = "LoaiMon";
        //}
        ////Load combo box về phương thức thanh toán của HoaDon (ko dùng được)
        //void load_cbo_HoaDon_PTTT()
        //{
        //    string query = "exec SP_Select_HoaDon_PhuongThucThanhToan";
        //    sda = new SqlDataAdapter(query, DatabaseConnection.con);
        //    dt = new DataTable();
        //    sda.Fill(dt);

        //    cbo_trangThai.DataSource = dt;
        //    cbo_trangThai.DisplayMember = "PhuongThucThanhToan";

        //}

        //Form chức năng-----------------------------------------------------------------------------------------
        private void AddData_Load(object sender, EventArgs e)
        {
            string[] insert_cbo = null;
            txt_tableName.Text = TableName.tableName;
            
            cbo_trangThai.Visible = false;
            cbo_loaiMon.Visible = false;
            radio_Nam.Visible = false;
            radio_Nam.Checked = true; 
            radio_nu.Visible = false;
            dateTimePicker1.Visible = false;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Value = DateTime.Now;
            label_tongTien.Visible = false;
            label_maKH.Visible = false;
            label_ma.Visible = false;

            //Xét trường hợp ấn nút 'Thêm' hoặc 'Cập nhật' để thực hiện phương thức phù hợp
            if (CollectingData.txt_box1 == "")
            {
                btn_capNhat.Visible = false;
                btn_them.Location = new Point(139, 48);
            }
            else
            {
                btn_them.Visible = false;
                btn_capNhat.Location = new Point(139, 48);
                label_ma.Visible = true;
                label_ma.Text = $"'{CollectingData.txt_box1}'";
                if (txt_tableName.Text == "ChiTietDatMon")
                    label_ma.Text = label_ma.Text + $"_'{CollectingData.txt_box2}'";
            }

            switch (txt_tableName.Text)
            {
                case "KhachHang":
                    txt_label1.Text = "Tên Khách hàng";
                    txt_label2.Text = "Giới tính"; // radio check box về giới tính
                    txt_label3.Text = "Số điện thoại";
                    txt_label4.Text = "Email";

                    //Ẩn và hiện các nút theo bảng 'KhachHang' sao cho phù hợp nhu cầu
                    radio_Nam.Visible = true;
                    radio_nu.Visible = true;

                    txt_textBox2.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;


                    //Trường hợp ấn nút 'Sửa'
                    if(btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box2; // Tên khách hàng
                        if (CollectingData.txt_box3 == "Nam") // Giới tính
                            radio_Nam.Checked = true;
                        else
                            radio_nu.Checked = true;
                        txt_textBox3.Text = CollectingData.txt_box4; // Số điện thoại
                        txt_textBox4.Text = CollectingData.txt_box5; // Email

                    }

                    break;

                case "TaiKhoan":
                    txt_label1.Text = "Tên tài khoản";
                    txt_label2.Text = "Mã khách hàng";

                    //Ẩn và hiện các nút theo bảng 'TaiKHoan' sao cho phù hợp nhu cầu
                    txt_textBox3.Visible = false;
                    txt_textBox4.Visible = false;
                    txt_textBox5.Visible = false;
                    txt_label3.Visible = false;
                    txt_label4.Visible = false;
                    txt_label5.Visible = false;

                    //Tắt enable cho textBox tên tài khoản
                    txt_textBox1.Enabled = false;

                    if(btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box1; // Tên tài khoản
                        txt_textBox2.Text = CollectingData.txt_box2; // Mã khách hàng
                    }

                    break;
                
                case "Ban":
                    txt_label1.Text = "Số ghế";
                    txt_label2.Text = "Trạng thái"; // cbo_trangThai

                    //combo box về trạng thái
                    insert_cbo = new string[] { "Có sẵn", "Trống" };
                    cbo_trangThai.Items.AddRange(insert_cbo);
                    cbo_trangThai.Location = new Point(58, 154);

                    //Ẩn và hiện các nút theo bảng 'Ban' sao cho phù hợp nhu cầu
                    cbo_trangThai.Visible = true;
                    txt_textBox2.Visible = false;

                    txt_label3.Visible = false;
                    txt_textBox3.Visible = false;

                    txt_label4.Visible = false;
                    txt_textBox4.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;

                    //Trường hợp ấn nút 'Sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box2; // Số ghế
                        cbo_trangThai.Text = CollectingData.txt_box3; // Trạng thái
                    }

                    break;

                case "DatBan":
                    txt_label1.Text = "Thời gian"; // dateTimePicker
                    txt_label2.Text = "Số khách";
                    txt_label3.Text = "Mã khách hàng";
                    txt_label4.Text = "Mã bàn"; // combo box mã bàn

                    //Ẩn và hiện các nút theo bảng 'DatBan' sao cho phù hợp nhu cầu
                    dateTimePicker1.Visible = true;
                    txt_textBox1.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;

                    //Trường hợp ấn nút 'Sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        dateTimePicker1.Value = ParseDate(CollectingData.txt_box4); // Thời gian
                        txt_textBox2.Text = CollectingData.txt_box5; // Số khách
                        txt_textBox3.Text = CollectingData.txt_box3; // Mã khách hàng
                        txt_textBox4.Text = CollectingData.txt_box2; // Mã bàn
                    }

                    break;

                
                case "MonAn":
                    txt_label1.Text = "Tên món ăn";
                    txt_label2.Text = "Loại món"; // cbo_loaiMon
                    txt_label3.Text = "Đơn giá";
                    txt_label4.Text = "Trạng thái món"; // cbo_trangThai

                    //Combo box về loại món
                    insert_cbo = new string[] { "Món chính", "Món phụ", "Món tráng miệng", "Món ăn kèm", "Món lẩu" };
                    cbo_loaiMon.Items.AddRange(insert_cbo);
                    cbo_loaiMon.Location = new Point(58, 154);

                    //Combo box về trạng thái món
                    insert_cbo = new string[] { "Có sẵn", "Hết" };
                    cbo_trangThai.Items.AddRange(insert_cbo);
                    cbo_trangThai.Location = new Point(58, 272);

                    //Ẩn và hiện các nút theo bảng 'MonAn' sao cho phù hợp nhu cầu
                    cbo_loaiMon.Visible = true;
                    txt_textBox2.Visible = false;
                    
                    cbo_trangThai.Visible = true;
                    txt_textBox4.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;

                    //Trường hợp ấn nút 'Sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box2; // Tên món ăn
                        cbo_loaiMon.Text = CollectingData.txt_box3; // Loại món
                        txt_textBox3.Text = CollectingData.txt_box4; // Đơn gián
                        cbo_trangThai.Text = CollectingData.txt_box5; // trạng thái món
                    }

                    break;

                case "DatMon":
                    txt_label1.Text = "Ngày lập";
                    txt_label2.Text = "Mã khách hàng";

                    //Ẩn và hiện các nút theo bảng 'DatMon' sao cho phù hợp nhu cầu
                    dateTimePicker1.Visible = true;
                    txt_textBox1.Visible = false;

                    txt_label3.Visible = false;
                    txt_textBox3.Visible = false;

                    txt_label4.Visible = false;
                    txt_textBox4.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;                    
                    
                    //Trường hợp ấn nút 'Sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        dateTimePicker1.Value = ParseDate(CollectingData.txt_box3); // Ngày lập
                        txt_textBox2.Text = CollectingData.txt_box2; // Mã khách hàng
                        txt_textBox2.Enabled = false; // Mã khách hàng không thể sửa được nên chặn người dùng nhập dữ liệu mã khách hàng
                    }

                    break;

                case "HoaDon":
                    txt_label1.Text = "Ngày lập"; // dateTimePicker
                    txt_label2.Text = "Mã đặt món";
                    txt_label3.Text = "Phương thức thanh toán"; // cbo_trangThai

                    //combo box về phương thức thanh toán (Sử dụng cbo_trangThai)
                    insert_cbo = new string[] { "Tiền mặt", "Thẻ", "Chuyển khoản" };
                    cbo_trangThai.Items.AddRange(insert_cbo);
                    cbo_trangThai.Location = new Point(58, 211); // điều chỉnh vị trí

                    //Ẩn và hiện các nút theo bảng 'HoaDon' sao cho phù hợp nhu cầu
                    dateTimePicker1.Visible = true;
                    txt_textBox1.Visible = false;

                    cbo_trangThai.Visible = true;
                    txt_textBox3.Visible = false;

                    txt_label4.Visible = false;
                    txt_textBox4.Visible = false;

                    txt_label5.Visible = false;
                    txt_textBox5.Visible = false;

                    //Trường hợp ấn nút 'Sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        dateTimePicker1.Value = ParseDate(CollectingData.txt_box3); //Ngày lập
                        txt_textBox2.Text = CollectingData.txt_box2; //Mã đặt món
                        cbo_trangThai.Text = CollectingData.txt_box5; //Phương thức thanh toán
                    }

                    break;

                //Nhiều khoá chính
                case "ChiTietDatMon":
                    txt_label1.Text = "Mã đặt món"; // Cần kiểm tra sự tồn tại khi thêm hoặc cập nhật
                    txt_label2.Text = "Mã món"; // Cần kiểm tra sự tồn tại khi thêm hoặc cập nhật
                    txt_label3.Text = "Tên món"; // Truy xuất thông qua mã món
                    txt_label4.Text = "Số lượng";
                    txt_label5.Text = "Ghi chú";

                    //Trường hợp ấn nút 'sửa'
                    if(btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box1;
                        txt_textBox2.Text = CollectingData.txt_box2;
                        txt_textBox3.Text = CollectingData.txt_box3;
                        txt_textBox4.Text = CollectingData.txt_box4;
                        txt_textBox5.Text = CollectingData.txt_box6;
                    }

                    break;

                case "NhanVien":
                    txt_label1.Text = "Tên nhân viên";
                    txt_label2.Text = "Giới tính"; //radio check về giới tính
                    txt_label3.Text = "Số điện thoại";
                    txt_label4.Text = "Email";
                    txt_label5.Text = "Mã người quản lý"; // Kiểm tra sự tồn tại của mã nhân viên khi thêm hoặc xoá; trường hợp ko có thì null hoặc để trống

                    //Ẩn và hiện các nút theo bảng 'NhanVien' sao cho phù hợp nhu cầu
                    radio_Nam.Visible = true;
                    radio_nu.Visible = true;
                    txt_textBox2.Visible = false;

                    
                    

                    //Trường hợp ấn nút 'sửa'
                    if (btn_capNhat.Visible == true)
                    {
                        txt_textBox1.Text = CollectingData.txt_box2; // Tên nhân viên
                        if (CollectingData.txt_box3 == "Nam") // Giới tính
                            radio_Nam.Checked = true;
                        else
                            radio_nu.Checked = true;
                        txt_textBox3.Text = CollectingData.txt_box4; // Số điện thoại
                        txt_textBox4.Text = CollectingData.txt_box5; // Email
                        txt_textBox5.Text = CollectingData.txt_box6; // Mã người quản lý
                    }

                    break;

            }

        }

        private void AddData_FormClosing(object sender, FormClosingEventArgs e)
        {
            CollectingData.txt_box1 = "";
            this.DestroyHandle();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                switch (txt_tableName.Text)
                {
                    case "KhachHang":
                        {
                            string tenKH = txt_textBox1.Text.Trim();
                            string gioiTinh;
                            if (radio_Nam.Checked)
                                gioiTinh = radio_Nam.Text;
                            else
                                gioiTinh = radio_nu.Text;
                            string sdt = txt_textBox3.Text.Trim();
                            string email = txt_textBox4.Text.Trim();
                            if (tenKH != "" && email != "")
                            {
                                if (IsValidEmail(email))
                                {
                                    string query = $"EXEC SP_Insert_KhachHang N'{tenKH}', N'{gioiTinh}', '{sdt}', '{email}'";
                                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }
                                    
                                    this.DestroyHandle();
                                }
                                else
                                    MessageBox.Show("Email Không hợp lệ", "Error");
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }
                            break;
                        }
                        
                    case "Ban":
                        {
                            string soghe = txt_textBox1.Text.Trim();
                            string trangThai = cbo_trangThai.Text;
                            if (soghe != "" && trangThai != "")
                            {
                               
                                string query = $"EXEC SP_Insert_Ban {soghe}, N'{trangThai}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                //MessageBox.Show("Đã thêm thành công!");
                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "DatBan":
                        {
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"); // Thời gian phải lớn hơn hiện tại
                            string soKhach = txt_textBox2.Text;
                            string maKh = txt_textBox3.Text.Trim();
                            string maBan = txt_textBox4.Text.Trim();
                            if (soKhach != "" && maKh != "" && maBan != "")
                            {

                                string query = $"exec SP_Insert_DatBan '{maBan}', '{maKh}', '{thoiGian}', {soKhach}";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                this.DestroyHandle();

                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }
                        
                    case "MonAn":
                        {
                            string tenMon = txt_textBox1.Text.Trim();
                            string loaiMon = cbo_loaiMon.Text;
                            double donGia;
                            if (string.IsNullOrEmpty(txt_textBox3.Text.Trim()))
                                donGia = 0;
                            else
                                donGia = double.Parse(txt_textBox3.Text.Trim());
                            string trangThaiMon = cbo_trangThai.Text;
                            if (tenMon != "" && loaiMon != "" && trangThaiMon != "")
                            {

                                string query = $"exec SP_Insert_MonAn N'{tenMon}', N'{loaiMon}', {donGia}, N'{trangThaiMon}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }
                    
                    case "DatMon":
                        {
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            string maKH = txt_textBox2.Text.Trim();
                            
                            if (maKH != "")
                            {

                                string query = $"exec SP_Insert_DatMon '{maKH}', '{thoiGian}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "HoaDon":
                        {
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            string MaDM = txt_textBox2.Text.Trim();
                            string PhuongThucThanhToan = cbo_trangThai.Text.Trim();
                            if (MaDM != "")
                            {

                                string query = $"exec SP_Insert_HoaDon '{MaDM}', '{thoiGian}', N'{PhuongThucThanhToan}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank", "Error");
                            }
                            break;
                        }

                    case "ChiTietDatMon":
                        {
                            string maDM = txt_textBox1.Text;
                            string maMon = txt_textBox2.Text;
                            string tenMon = txt_textBox3.Text;
                            string soLuong = txt_textBox4.Text;
                            string ghiChu = txt_textBox5.Text;
                            if (maDM != "" && maMon != "" && tenMon != "" && soLuong != "")
                            {
                                //Trường hợp ghi chú rỗng thì cho là null để chạy thủ tục thêm hợp lý
                                if (ghiChu == "")
                                    ghiChu = "null";
                                //Thực hiện thao tác thêm dữ liệu
                                string query = $"exec SP_Insert_ChiTietDatMon '{maDM}', '{maMon}', N'{tenMon}', {soLuong}, N'{ghiChu}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }
                        

                    case "NhanVien":
                        {
                            string tenNV = txt_textBox1.Text.Trim();
                            string gioiTinh;
                            if (radio_Nam.Checked)
                                gioiTinh = radio_Nam.Text;
                            else
                                gioiTinh = radio_nu.Text;
                            string sdt = txt_textBox3.Text.Trim();
                            string email = txt_textBox4.Text.Trim();
                            string maNQL = txt_textBox5.Text.Trim();
                            if (tenNV != "" && sdt != "" && email != "")
                            {
                                if(IsValidEmail(email))
                                {

                                    string query;
                                    if (maNQL == "")
                                        query = $"EXEC SP_Insert_NhanVien N'{tenNV}', N'{gioiTinh}', '{sdt}', '{email}'";
                                    else
                                        query = $"EXEC SP_Insert_NhanVien N'{tenNV}', N'{gioiTinh}', '{sdt}', '{email}', '{maNQL}'";

                                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                    this.DestroyHandle();
                                }
                                else
                                {
                                    MessageBox.Show("Email không hợp lệ", "Error");
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }
                }
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("Error-----> " + ex.Message);
            }
        }

        
        //Bug: Trường hợp mở 2 form cập nhật thì form mở cuối cùng sẽ thay đổi dữ liệu
        //Lúc đó, dữ liệu sẽ bị chèn và bắt lỗi cho form đầu tiên
        private void btn_capNhat_Click(object sender, EventArgs e)
        {
            try
            {
                switch (txt_tableName.Text)
                {
                    case "KhachHang":
                        {
                            string maNV = CollectingData.txt_box1; // Lấy mã nhân viên cần sửa
                            string tenKH = txt_textBox1.Text.Trim();
                            string gioiTinh;
                            if (radio_Nam.Checked)
                                gioiTinh = radio_Nam.Text;
                            else
                                gioiTinh = radio_nu.Text;
                            string sdt = txt_textBox3.Text.Trim();
                            string email = txt_textBox4.Text.Trim();
                            if (tenKH != "" && email != "")
                            {
                                if (IsValidEmail(email))
                                {
                                    string query = $"EXEC SP_Update_KhachHang '{maNV}', N'{tenKH}', N'{gioiTinh}', '{sdt}', '{email}'";
                                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                    this.DestroyHandle();
                                }
                                else
                                    MessageBox.Show("Email Không hợp lệ", "Error");
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }
                            break;
                        }

                    case "TaiKhoan":
                        {
                            string tenTaiKhoan = CollectingData.txt_box1; // Lấy tên tài khoản
                            string maKhachHang = txt_textBox2.Text.Trim(); // Lấy mã khách hàng
                            
                            if (tenTaiKhoan != "")
                            {
                                string query;
                                if (maKhachHang != "")
                                    query = $"exec SP_Update_TaiKhoan '{tenTaiKhoan}', '{maKhachHang}'";
                                else
                                    query = $"exec SP_Update_TaiKhoan '{tenTaiKhoan}', null";
                                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                                cmd.ExecuteNonQuery();
                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }
                            break;
                        }

                    case "Ban":
                        {
                            string maBan = CollectingData.txt_box1; // Lấy mã bàn cần sửa
                            string soghe = txt_textBox1.Text.Trim();
                            string trangThai = cbo_trangThai.Text;
                            if (soghe != "" && trangThai != "")
                            {

                                string query = $"EXEC SP_Update_Ban '{maBan}', {soghe}, N'{trangThai}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "DatBan":
                        {
                            string maDB = CollectingData.txt_box1; // Lấy mã đặt bàn cần sửa
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"); // Thời gian phải lớn hơn hiện tại
                            string soKhach = txt_textBox2.Text;
                            string maKh = txt_textBox3.Text.Trim();
                            string maBan = txt_textBox4.Text.Trim();
                            if (soKhach != "" && maKh != "" && maBan != "")
                            {

                                string query = $"exec SP_Update_DatBan '{maDB}','{maBan}', '{maKh}', '{thoiGian}', {soKhach}";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                this.DestroyHandle();

                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "MonAn":
                        {
                            string maMon = CollectingData.txt_box1; // Lấy mã món cần sửa
                            string tenMon = txt_textBox1.Text.Trim();
                            string loaiMon = cbo_loaiMon.Text;
                            double donGia;
                            if (string.IsNullOrEmpty(txt_textBox3.Text.Trim()))
                                donGia = 0;
                            else
                                donGia = double.Parse(txt_textBox3.Text.Trim());
                            string trangThaiMon = cbo_trangThai.Text;
                            if (tenMon != "" && loaiMon != "" && trangThaiMon != "")
                            {

                                string query = $"exec SP_Update_MonAn '{maMon}',N'{tenMon}', N'{loaiMon}', {donGia}, N'{trangThaiMon}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "DatMon":
                        {
                            string maDM = CollectingData.txt_box1; // Lấy mã đặt món cần sửa
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            string maKH = txt_textBox2.Text.Trim();
                            
                            if (maKH != "")
                            {

                                string query = $"exec SP_Update_DatMon '{maDM}','{maKH}', '{thoiGian}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }

                    case "HoaDon":
                        {
                            string maHD = CollectingData.txt_box1; // Lấy mã hoá đơn cần sửa
                            string thoiGian = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
                            string MaDM = txt_textBox2.Text.Trim();
                            string PhuongThucThanhToan = cbo_trangThai.Text.Trim();
                            if (MaDM != "")
                            {

                                string query = $"exec SP_Update_HoaDon '{maHD}', '{MaDM}', '{thoiGian}', N'{PhuongThucThanhToan}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank", "Error");
                            }
                            break;
                        }

                    // Chưa sửa xong
                    case "ChiTietDatMon":
                        {

                            string maDM = CollectingData.txt_box1; // Lấy mã đặt món cần sửa (khoá chính 1)
                            string maMon = CollectingData.txt_box2; // Lấy mã món cần sửa (Khoá chính 2)
                            string tenMon = txt_textBox3.Text;
                            string soLuong = txt_textBox4.Text;
                            string ghiChu = txt_textBox5.Text;
                            if (maDM != "" && maMon != "" && tenMon != "" && soLuong != "")
                            {
                                //Trường hợp ghi chú rỗng thì cho là null để chạy thủ tục thêm hợp lý
                                if (ghiChu == "")
                                    ghiChu = "null";
                                //Thực hiện thao tác cập nhật dữ liệu
                                string query = $"exec SP_Update_ChiTietDatMon '{maDM}', '{maMon}', N'{tenMon}', {soLuong}, N'{ghiChu}'";
                                using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                {
                                    cmd.ExecuteNonQuery();
                                }

                                this.DestroyHandle();
                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }


                    case "NhanVien":
                        {
                            string maNV = CollectingData.txt_box1; // Lấy mã nhân viên cần sửa
                            string tenNV = txt_textBox1.Text.Trim();
                            string gioiTinh;
                            if (radio_Nam.Checked)
                                gioiTinh = radio_Nam.Text;
                            else
                                gioiTinh = radio_nu.Text;
                            string sdt = txt_textBox3.Text.Trim();
                            string email = txt_textBox4.Text.Trim();
                            string maNQL = txt_textBox5.Text.Trim();
                            if (tenNV != "" && sdt != "" && email != "")
                            {
                                if (IsValidEmail(email))
                                {

                                    string query;
                                    if (maNQL == "")
                                        query = $"EXEC SP_Update_NhanVien '{maNV}', N'{tenNV}', N'{gioiTinh}', '{sdt}', '{email}'";
                                    else
                                        query = $"EXEC SP_Update_NhanVien '{maNV}', N'{tenNV}', N'{gioiTinh}', '{sdt}', '{email}', '{maNQL}'";

                                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                                    {
                                        cmd.ExecuteNonQuery();
                                    }

                                    this.DestroyHandle();
                                }
                                else
                                {
                                    MessageBox.Show("Email không hợp lệ", "Error");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Fill in blank");
                            }

                            break;
                        }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error-----> " + ex.Message);
            }
        }

        private void txt_textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (TableName.tableName == "KHACH" || TableName.tableName == "MONAN" || TableName.tableName == "DATMON" || TableName.tableName == "NHANVIEN")
            //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            //    {
            //        // If the character is not a digit, block it by setting Handled to true
            //        e.Handled = true;
            //    }
        }

        private void txt_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (TableName.tableName == "BAN")
            //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            //    {
            //        // If the character is not a digit, block it by setting Handled to true
            //        e.Handled = true;
            //    }
        }

        private void txt_textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (TableName.tableName == "DATBAN")
            //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            //    {
            //        // If the character is not a digit, block it by setting Handled to true
            //        e.Handled = true;
            //    }
        }

        private void txt_textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (TableName.tableName == "DATMON")
            //    if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
        }

       
        private void txt_textBox1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (TableName.tableName == "DATMON")
            //    {
            //        string maMon = txt_textBox1.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        if (maMon.Length == 5 && maMon.StartsWith("MA", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"EXEC FindMONAN_MAMON '{maMon}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                txt_textBox2.Text = reader.GetValue(0).ToString();
            //                txt_textBox4.Text = reader.GetValue(1).ToString();
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã món '{maMon}' này");
            //            }
            //            reader.Close();
            //        }
            //    }
            //    if (TableName.tableName == "CHITIETHD")
            //    {
            //        string maHD = txt_textBox1.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        label_maKH.Visible = false;
            //        if (maHD.Length == 5 && maHD.StartsWith("HD", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"EXEC FindHOADON_MAHD '{maHD}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                label_maKH.Text = "Mã khách hàng: " + reader.GetValue(1).ToString();
            //                label_maKH.Visible = true;
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã hoá đơn '{maHD}' này");
            //            }
            //            reader.Close();
            //        }
            //    }
            //    if (TableName.tableName == "PHUCVUBAN")
            //    {
            //        string maBAN = txt_textBox1.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        if (maBAN.Length == 5 && maBAN.StartsWith("BA", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"exec FindBAN_MABAN '{maBAN}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã bàn '{maBAN}' này");
            //            }
            //            reader.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error---> " + ex.Message);
            //}
        }

        private void txt_textBox3_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (TableName.tableName == "DATMON")
            //    {
            //        if(txt_textBox3.Text != "")
            //        {
            //            int soluong = int.Parse(txt_textBox3.Text);
            //            decimal dongia = decimal.Parse(txt_textBox4.Text);
            //            decimal tongTien = soluong * dongia;
            //            label_tongTien.Text = "Tổng số tiền: " + tongTien.ToString();
            //            label_tongTien.Visible = true;
            //        }
            //        else
            //        {
            //            label_tongTien.Visible = false;
            //        }    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error---> " + ex.Message);
            //}
            
        }

        private void txt_textBox2_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
                
            //    if (TableName.tableName == "CHITIETHD")
            //    {
            //        string maDM = txt_textBox2.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        label_tongTien.Visible = false;
            //        if (maDM.Length == 5 && maDM.StartsWith("DM", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"EXEC FindDATMON_MADM '{maDM}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                label_tongTien.Text = "Tổng số tiền: " + reader.GetValue(0).ToString();
            //                label_tongTien.Visible = true;
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã đặt món '{maDM}' này");
            //            }
            //            reader.Close();
            //        }
            //    }

            //    else if (TableName.tableName == "PHUCVUBAN")
            //    {
            //        string maNV = txt_textBox2.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        if (maNV.Length == 5 && maNV.StartsWith("NV", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"exec FindNHANVIEN_MANV '{maNV}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã nhân viên '{maNV}' này");
            //            }
            //            reader.Close();
            //        }
            //    }

            //    else if(TableName.tableName == "HOADON")
            //    {
            //        string maKH = txt_textBox2.Text;
            //        btn_them.Enabled = false;
            //        btn_capNhat.Enabled = false;
            //        if (maKH.Length == 5 && maKH.StartsWith("KH", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"exec FindKHACH_MAKH '{maKH}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã khách hàng '{maKH}' này");
            //            }
            //            reader.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error---> " + ex.Message);
            //}
        }

        private void txt_textBox6_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (TableName.tableName == "NHANVIEN")
            //    {
            //        string maNV = txt_textBox5.Text;
            //        if(maNV == "")
            //        {
            //            btn_them.Enabled = true;
            //            btn_capNhat.Enabled = true;
            //        }
            //        else if(maNV.Length == 5 && maNV.StartsWith("NV", StringComparison.OrdinalIgnoreCase))
            //        {
            //            string query = $"exec FindNHANVIEN_MANV '{maNV}'";
            //            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
            //            SqlDataReader reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                btn_them.Enabled = true;
            //                btn_capNhat.Enabled = true;
            //            }
            //            else
            //            {
            //                MessageBox.Show($"Không có mã nhân viên '{maNV}' này");
            //            }
            //            reader.Close();
            //        }
            //        else
            //        {
            //            btn_them.Enabled = false;
            //            btn_capNhat.Enabled = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error---> " + ex.Message);
            //}
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (TableName.tableName == "DATBAN")
            //    {
            //        if(dateTimePicker1.Value <= DateTime.Now)
            //        {
            //            errorProvider1.SetError(dateTimePicker1, "Thời gian đặt bàn phải trước ngày hiện tại");
            //            btn_capNhat.Enabled = false;
            //            btn_them.Enabled = false;
            //        }
            //        else
            //        {
            //            errorProvider1.SetError(dateTimePicker1, "");
            //            btn_capNhat.Enabled = true;
            //            btn_them.Enabled = true;
            //        }
            //    }
            //    else if(TableName.tableName == "HOADON")
            //    {
            //        if(dateTimePicker1.Value > DateTime.Now)
            //        {
            //            errorProvider1.SetError(dateTimePicker1, "Thời gian hoá đơn làm gì có tương lai");
            //            btn_capNhat.Enabled = false;
            //            btn_them.Enabled = false;
            //        }
            //        else
            //        {
            //            errorProvider1.SetError(dateTimePicker1, "");
            //            btn_capNhat.Enabled = true;
            //            btn_them.Enabled = true;
            //        }

            //    }
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Error---> " + ex.Message);
            //}
            
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
