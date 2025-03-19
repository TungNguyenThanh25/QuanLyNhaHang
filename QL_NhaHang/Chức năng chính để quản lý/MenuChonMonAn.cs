using Microsoft.IdentityModel.Tokens;
using QL_NhaHang.Login_and_regist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.Chức_năng_chính_để_quản_lý
{
    public partial class MenuChonMonAn : Form
    {
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        SqlDataReader reader;
        
        public MenuChonMonAn()
        {
            InitializeComponent();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AllowUserToAddRows = false;
        }
        //--------Load dữ liệu-----------
        //Load thực đơn món ăn
        void load_gridView_menu()
        {
            //Load dữ liệu vào bảng dataGridView
            cmd = new SqlCommand("exec SP_Select_MonAn", DatabaseConnection.con);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;

            //Xuất các thông tin đầu tiên vào textBox
            txt_maMon.Text = dt.Rows[0][0].ToString();
            txt_tenMon.Text = dt.Rows[0][1].ToString();
            cbo_loaiMon.Text = dt.Rows[0][2].ToString();
            txt_donGia.Text = dt.Rows[0][3].ToString();
            cbo_trangThaiMon.Text = dt.Rows[0][4].ToString();
        }

        //Load danh sách món ăn mà khách hàng 'Chưa đặt' hoặc 'Đang đợi duyệt' hoặc 'Đã đặt'
        void load_gridView_DSChuaDat()
        {
            try
            {
                //Tìm và thực hiện theo mã khách hàng
                cmd = new SqlCommand($"EXEC SP_Select_DuyetThucDon_XuatThongTinCanThiet '{txt_maKH.Text}'", DatabaseConnection.con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

                //Kiểm tra xem khách hàng có chọn món nào không?, nếu ko thì quăng lỗi vào
                if (dt.Rows.Count == 0)
                {
                    //Ẩn các nút xoá, sửa, đặt món
                    btn_xoa.Enabled = false;
                    btn_sua.Enabled = false;
                    btn_datMon.Enabled = false;
                    throw new Exception("Hiện tại chưa có món nào trong danh sách hết");
                }
                else
                {
                    //Hiện các nút xoá, sửa, đặt món để thực hiện chức năng
                    btn_xoa.Enabled = true;
                    btn_sua.Enabled = true;
                    btn_datMon.Enabled = true;
                }

                txt_maMon.Text = dt.Rows[0][0].ToString();
                txt_tenMon.Text = dt.Rows[0][1].ToString();
                cbo_loaiMon.Text = dt.Rows[0][2].ToString();
                txt_donGia.Text = dt.Rows[0][3].ToString();
                txt_trangThaiDuyetMon.Text = dt.Rows[0][4].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Load combo box trạng thái món ăn
        void load_cbo_monan_trangthai()
        {
            string query = "exec SP_Select_MonAn_TrangThai";
            sda = new SqlDataAdapter(query, DatabaseConnection.con);
            dt = new DataTable();
            sda.Fill(dt);

            cbo_trangThaiMon.DataSource = dt;
            cbo_trangThaiMon.DisplayMember = "trangthai";
        }

        //Load combo box về loại món của MonAn
        void load_cbo_monan_loaimon()
        {
            string query = "exec SP_Select_MonAn_LoaiMon";
            sda = new SqlDataAdapter(query, DatabaseConnection.con);
            dt = new DataTable();
            sda.Fill(dt);

            cbo_loaiMon.DataSource = dt;
            cbo_loaiMon.DisplayMember = "LoaiMon";
        }

        void load_textBox_LayMaKhachHang()
        {
            try
            {
                string query = $"SP_Select_TaiKhoan_LayMaKhachHang '{DangNhap.dangNhap}'";
                cmd = new SqlCommand(query, DatabaseConnection.con);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                    if (reader.GetString(0).IsNullOrEmpty())
                        throw new Exception();
                    else
                        txt_maKH.Text = reader.GetString(0);
            }
            catch
            {
                MessageBox.Show("Bạn chưa có mã khách hàng được cấp từ admin");
                btn_thoat.PerformClick();
                this.Dispose();
            }
            finally
            {
                reader.Close();
            }
        }

        //-----------From chức năng-----------
        //Load dữ liệu cần thiết
        private void MenuChonMonAn_Load(object sender, EventArgs e)
        {
            load_cbo_monan_loaimon();
            load_cbo_monan_trangthai();
            load_gridView_menu();

            //Các textBox của mã món ăn và đơn giá ko được chỉnh sửa
            txt_maMon.Enabled = false;
            txt_donGia.Enabled = false;

            //Ẩn nút xoá - sửa khi chưa cần thiết sử dụng (Chỉ cần thiết khi khách hàng ấn 'Xem danh sách đặt món')
            btn_xoa.Visible = false;
            btn_sua.Visible = false;

            //Ẩn các nút và textBox về tên món, trạng thái món, loại món... ban đầu (Chỉ mở lên khi tìm thấy mã khách hàng tồn tại)
            if(DangNhap.dangNhap == "sa")
            {
                txt_tenMon.Enabled = false;
                cbo_loaiMon.Enabled = false;
                cbo_trangThaiMon.Enabled = false;
                txt_soLuong.Enabled = false;
                btn_chonMon.Enabled = false;
                btn_xemDSMonDat.Enabled = false;
                btn_datMon.Enabled = false;
                dataGridView1.Enabled = false;
            }
            else
            {
                load_textBox_LayMaKhachHang();
                btn_datMon.Enabled = false;
                txt_maKH.Enabled = false;
                btn_KiemTraMaKH.Visible = false;
            }
        }

        //Chức năng load dữ liệu tên món, mã món, loại món... khi click vào danh sách datagrid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    //Xuất thông tin món ăn vào textBox khi click chuột
                    int index = e.RowIndex;
                    txt_maMon.Text = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    txt_tenMon.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    cbo_loaiMon.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    txt_donGia.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    cbo_trangThaiMon.Text = dataGridView1.Rows[index].Cells[4].Value.ToString(); // Load món ăn 'Có sẵn' hoặc 'Đã hết'
                    txt_trangThaiDuyetMon.Text = dataGridView1.Rows[index].Cells[4].Value.ToString(); // Load trạng thái duyệt món

                    //Trường hợp ấn nút 'Xem danh sách món đặt' thì thêm số lượng mà khách hàng muốn đặt
                    if (string.Compare(btn_xemDSMonDat.Text, "Xem danh sách món đặt", true) != 0)
                        txt_soLuong.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không có món nào đang đợi đặt");
            }
        }

        private void btn_xemDSMonDat_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.Compare(btn_xemDSMonDat.Text, "Xem danh sách món đặt", true) == 0)
                {
                    //Load dữ liệu những danh sách món ăn Chưa đặt hoặc đang đợi duyệt
                    load_gridView_DSChuaDat();
                    btn_xemDSMonDat.Text = "Trở lại MENU";

                    //Hiện các nút xoá sửa
                    btn_xoa.Visible = true;
                    btn_sua.Visible = true;

                    //Ẩn nút 'Chọn món', TextBox mã khách hàng
                    btn_chonMon.Enabled = false;
                    txt_maKH.Enabled = false;

                    //Hiện textBox cho Trạng thái món kiểu Duyệt món ăn
                    txt_trangThaiDuyetMon.Visible = true;
                }
                else
                {
                    load_gridView_menu();
                    btn_xemDSMonDat.Text = "Xem danh sách món đặt";

                    //Ẩn các nút xoá sửa
                    btn_xoa.Visible = false;
                    btn_sua.Visible = false;

                    //Ẩn nút Đặt món
                    btn_datMon.Enabled = false;

                    //Hiện nút 'Chọn món', TextBox mã khách hàng
                    btn_chonMon.Enabled = true;

                    //Ẩn textBox cho Trạng thái món kiểu Duyệt món ăn
                    txt_trangThaiDuyetMon.Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn_thoat.PerformClick();
            }
        }

        private void btn_chonMon_Click(object sender, EventArgs e)
        {
            try
            {
                string maMon = txt_maMon.Text.Trim();
                string maKh = txt_maKH.Text; // Xử lý tạm thời vì chưa cấp quyền tài khoản user có chứa ID mã khách hàng
                string soLuong = txt_soLuong.Text.Trim();
                string query = "";
                
                //Kiểm tra xem người dùng có nhập số lượng món ăn cần đặt không
                if(soLuong == "")
                {
                    query = $"exec SP_Insert_DuyetThucDon '{maKh}','{maMon}'";
                }
                else
                {
                    query = $"exec SP_Insert_DuyetThucDon '{maKh}','{maMon}', '{soLuong}'";
                }

                //Thực hiện chạy command
                cmd = new SqlCommand(query, DatabaseConnection.con);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn_thoat.PerformClick();
            }
        }

        //Xoá món ăn mà khách hàng không muốn đặt
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                //Trường hợp các món ăn trong trạng thái "Chưa đặt" thì mới được thực hiện
                if (string.Compare(txt_trangThaiDuyetMon.Text, "Đang đợi duyệt", true) != 0)
                {
                    string maKH = txt_maKH.Text;
                    string maMon = txt_maMon.Text;
                    string tenMon = txt_tenMon.Text;

                    //Hiện thông báo xoá, nếu 'No' thì bỏ qua
                    DialogResult r = MessageBox.Show($"Món ăn '{tenMon}' sẽ bị xoá, bạn có chắc không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (r == DialogResult.Yes)
                    {
                        //Thực hiện xoá
                        cmd = new SqlCommand($"exec SP_Delete_DuyetThucDon '{txt_maKH.Text}', '{txt_maMon.Text}'", DatabaseConnection.con);
                        cmd.ExecuteNonQuery();
                        load_gridView_DSChuaDat();
                    }
                }
                else
                {
                    throw new Exception("Không thể xoá món ăn đang trong trạng thái 'Đang đợi duyệt'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                // Nếu phân quyền bị huỷ mà người dùng vẫn còn tương tác, thoát ngay lập tức
                try
                {
                    string query = "exec SP_Select_MonAn_LoaiMon";
                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    btn_thoat.PerformClick();
                }
            }

        }

        //Cập nhật số lượng món ăn theo ý muốn
        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                //Xét trường hợp Trạng thái duyệt thực đơn, nếu trạng thái 'Chưa đặt' thì thực hiện, ko thì quăn cái lỗi lên
                if (string.Compare(txt_trangThaiDuyetMon.Text, "Chưa đặt", true) == 0)
                {
                    cmd = new SqlCommand($"exec SP_Update_DuyetThucDon_SoLuong '{txt_maKH.Text}', '{txt_maMon.Text}', {txt_soLuong.Text}", DatabaseConnection.con);
                    cmd.ExecuteNonQuery();
                    load_gridView_DSChuaDat();
                }
                else
                {
                    //Trường hợp trạng thái là 'Đang đợi duyệt' thì quăng thông báo này, còn ko thì quăng thông báo 'Đã duyệt'
                    if (string.Compare(txt_trangThaiDuyetMon.Text, "Đang đợi duyệt", true) == 0)
                        throw new Exception("Không thể cập nhật số lượng món ăn đang trong trạng thái 'Đang đợi duyệt'");
                    else
                        throw new Exception("Không thể cập nhật số lượng món ăn đang trong trạng thái 'Đã duyệt'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                // Nếu phân quyền bị huỷ mà người dùng vẫn còn tương tác, thoát ngay lập tức
                try
                {
                    string query = "exec SP_Select_MonAn_LoaiMon";
                    using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch
                {
                    btn_thoat.PerformClick();
                }
            }
        }

        private void btn_datMon_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand($"exec SP_Update_DuyetThucDon_DangDoiDuyet '{txt_maKH.Text}'", DatabaseConnection.con);
                cmd.ExecuteNonQuery();
                load_gridView_DSChuaDat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btn_thoat.PerformClick();
            }
        }

        private void MenuChonMonAn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(DangNhap.dangNhap == "sa")
            {
                Admin admin = new Admin();
                admin.Show();
            }
            else
            {
                User user = new User();
                user.Show();
            }
            this.DestroyHandle();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_KiemTraMaKH_Click(object sender, EventArgs e)
        {
            try
            {

                //Kiểm tra tên nút có phải là 'Đổi' không?, nếu có thì return để ko thực hiện bên dưới code
                if(string.Compare(btn_KiemTraMaKH.Text,"Đổi",true) == 0)
                {
                    //Đổi tên nút thành 'Kiểm tra'
                    btn_KiemTraMaKH.Text = "Kiểm tra";

                    //Ẩn các nút và textBox về tên món, trạng thái món, loại món... khi ấn nút 'Đổi'
                    txt_tenMon.Enabled = false;
                    cbo_loaiMon.Enabled = false;
                    cbo_trangThaiMon.Enabled = false;
                    txt_soLuong.Enabled = false;
                    btn_chonMon.Enabled = false;
                    dataGridView1.Enabled = false;
                    btn_xemDSMonDat.Enabled = false;
                    txt_maKH.Enabled = true; //Cho nhập mã khách hàng
                    return;
                }

                string maKH = txt_maKH.Text.Trim();

                //Kiểm tra dữ liệu nhập có trùng khớp với mã KH ko (VD: KH***)
                if (maKH.Length >= 5 && maKH.StartsWith("KH", StringComparison.OrdinalIgnoreCase))
                {
                    //Thực hiện tìm mã khách hàng trong thủ tục KhachHang
                    string query = $"EXEC SP_Select_KhachHang_MaKH '{maKH}'";
                    SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    //Kiểm tra xem dữ liệu cần tìm tồn tại không
                    if (reader.Read())
                    {
                        //Hiện các textBox hoặc nút chức năng để thực hiện đồng thời đổi tên nút thành 'Kiểm tra'
                        btn_KiemTraMaKH.Text = "Đổi";
                        txt_tenMon.Enabled = true;
                        cbo_loaiMon.Enabled = true;
                        cbo_trangThaiMon.Enabled = true;
                        txt_soLuong.Enabled = true;
                        btn_chonMon.Enabled = true;
                        btn_xemDSMonDat.Enabled = true;
                        dataGridView1.Enabled = true;
                        txt_maKH.Enabled = false; //Ko cho nhập mã khách hàng khi thực hiện chức năng
                    }
                    else
                    {
                        MessageBox.Show($"Không có mã khách hàng '{maKH}' này");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
