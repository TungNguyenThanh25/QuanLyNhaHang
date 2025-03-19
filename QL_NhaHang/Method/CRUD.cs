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

namespace QL_NhaHang.Method
{
    public partial class CRUD : Form
    {
        public CRUD()
        {
            InitializeComponent();
            dataGridView1.AutoResizeColumns();
            dataGridView1.AllowUserToAddRows = false;
        }

        //Các phương thức hỗ trợ--------------------------------------------------------------------------
        //Lấy tên của tất cả bảng trong database
        private List<string> GetTableNames() 
        {
            List<string> tableNames = new List<string>();
            string query = "exec SP_Select_TenTatCaBang";

            using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader["TABLE_NAME"].ToString());
                    }
                }
            }
            
            return tableNames;
        }

        //Lấy dữ liệu từng bảng
        private DataTable GetDataFromTable(string table_name)
        {
            DataTable dataTable = new DataTable();
            string query = $"EXEC SP_Select_{table_name}";
            

            using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    // Fill the DataTable with the result of the query
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        //Chức năng form---------------------------------------------------------------------------------

        private void TruyVan_Load(object sender, EventArgs e)
        {
            List<string> listTable = GetTableNames();
            cbo_tableName.Items.AddRange(listTable.ToArray());
            cbo_tableName.Items.Remove("UserSession");
            cbo_tableName.Items.Remove("sysdiagrams");
        }

        //Nút test để kiểm tra lúc chạy code (Không có liên quan)
        private void btn_test_Click(object sender, EventArgs e)
        {
            //ko có gì hết
        }

        //ComboBox về chọn bảng và load dữ liệu vào DataGridView khi được chọn
        private void cbo_tableName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null; // Bug về load bảng ko theo trình tự

                DataTable tableData = GetDataFromTable(cbo_tableName.Text);

                dataGridView1.DataSource = tableData;

                //Tạm thời ẩn chức năng lọc cho một số bảng
                if (cbo_tableName.Text == "Ban" ||
                    cbo_tableName.Text == "PhucVuBan" ||
                    cbo_tableName.Text == "DatMon" ||
                    cbo_tableName.Text == "ChiTietDatMon" ||
                    cbo_tableName.Text == "DuyetThucDon")
                {
                    btn_loc.Enabled = false;
                }
                else
                {
                    btn_loc.Enabled = true;
                }

                if(cbo_tableName.Text == "TaiKhoan")
                {
                    btn_loc.Enabled = false;
                    btn_them.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error-----> " + ex.Message);
            }
        }

        //Nút 'Thêm' dữ liệu cho một bảng
        private void btn_them_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem bảng cần load có được chọn chưa?
            if (cbo_tableName.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn bảng");
                return;
            }

            //Kiểm tra xem bảng chọn có phải là PhucVuBan không? nếu có thì return
            if(cbo_tableName.Text=="PhucVuBan")
            {
                MessageBox.Show("Không thể thao tác bảng PhucVuBan");
                return;
            }

            //Kiểm tra xem bảng chọn có phải là DuyetThucDon không? nếu có thì return
            if (cbo_tableName.Text == "DuyetThucDon")
            {
                MessageBox.Show("Đang bảo trì!");
                return;
            }

            if (string.Compare(btn_them.Text, "Thêm", true) == 0)
            //Ấn nút 'Thêm' sẽ xuất hiện form 'Add_Update_Data' đồng thời đổi tên nút thành 'Cập nhật'
            {
                btn_sua.Enabled = false;
                btn_xoa.Enabled = false;
                btn_them.Text = "Cập nhật"; //Đổi tên sau khi ấn nút 'Thêm'

                //Chuyển vào form 'Add_Update_Data' để thêm dữ liệu phù hợp
                TableName.tableName = cbo_tableName.SelectedItem.ToString(); // Lấy tên bảng xác định các dữ liệu nhập thông tin trên form mới sao cho phù hợp
                Form AddData = new Them_Sua();
                AddData.ShowDialog();
                AddData.Text = "Thêm dữ liệu"; // Tên của form (Ko quan trọng)
            }
            else
            //Ấn nút 'Cập nhật' sẽ cập nhật lại bảng và đổi tên nút thành 'Thêm'
            {
                btn_sua.Enabled = true;
                btn_xoa.Enabled = true;
                btn_them.Text = "Thêm";

                //Cập nhật lại bảng sau khi thêm
                if (dataGridView1.Rows.Count > 0)
                {
                    DataTable tableData = GetDataFromTable(cbo_tableName.Text);
                    dataGridView1.DataSource = tableData;
                }
            }
        }

        private void TruyVan_FormClosing(object sender, FormClosingEventArgs e)
        {

            Admin admin = new Admin();
            admin.Show();
            this.DestroyHandle();

        }

        //Nút 'xoá' dữ liệu được chọn từ một bảng
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                //Kiểm tra xem bảng cần load có được chọn chưa?
                if (cbo_tableName.SelectedItem == null)
                {
                    MessageBox.Show("Bạn chưa chọn bảng");
                    return;
                }

                //Kiểm tra xem bảng chọn có phải là PhucVuBan không? nếu có thì return
                if (cbo_tableName.Text == "PhucVuBan")
                {
                    MessageBox.Show("Không thể thao tác bảng PhucVuBan");
                    return;
                }

                //Kiểm tra xem bảng chọn có phải là DuyetThucDon không? nếu có thì return
                if (cbo_tableName.Text == "DuyetThucDon")
                {
                    MessageBox.Show("Đang bảo trì!");
                    return;
                }

                string tenBang = cbo_tableName.SelectedItem.ToString(); // Lấy tên bảng từ combo box được chọn
                
                //Thủ tục xoá dữ liệu
                foreach (DataGridViewRow row in dataGridView1.SelectedRows) // Lấy 1 dòng dữ liệu được chọn
                {
                    if (!row.IsNewRow) // Kiểm tra dòng dữ liệu chọn có rỗng hay không.
                    {
                        string getID = row.Cells[0].Value.ToString().Trim(); // Lấy khoá chính
                        string getAnotherID = null; // Khởi tạo rỗng khi không có 2 khoá chính trở lên
                            
                        DialogResult r; //Hiện thông báo cho người dùng chọn Yes No
                        //Xét trường hợp 1 hoặc nhiều khoá chính cho một bảng được chọn
                        if (tenBang == "ChiTietDatMon")
                        {
                            getAnotherID = row.Cells[1].Value.ToString().Trim(); // Lấy khoá chính thứ 2
                            r = MessageBox.Show($"Có chắc là muốn xoá mã '{getID}'_'{getAnotherID}' của bảng '{tenBang}' không?", "Thông báo", MessageBoxButtons.YesNo);
                        }
                        else
                            r = MessageBox.Show($"Có chắc là muốn xoá mã '{getID}' của bảng '{tenBang}' không?", "Thông báo", MessageBoxButtons.YesNo);

                        //Thực hiện thủ tục xoá khi chọn 'YES'
                        if (r == DialogResult.Yes)
                        {
                            //Xét trường hợp 1 hoặc nhiều khoá chính để thực thi phù hợp
                            string query;
                            if (tenBang == "ChiTietDatMon")
                                query = $"exec SP_Delete_{tenBang} '{getID}', '{getAnotherID}'";
                            else
                                query = $"exec SP_Delete_{tenBang} '{getID}'";

                            //Thực hiện command
                            SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                            cmd.ExecuteNonQuery();

                            //Cập nhật lại bảng sau khi xoá
                            if (dataGridView1.Rows.Count > 0)
                            {
                                DataTable tableData = GetDataFromTable(cbo_tableName.Text);
                                dataGridView1.DataSource = tableData;
                            }
                        }
                    }
                    else
                        MessageBox.Show("Đùa không vui, làm gì có dữ liệu ở đó", "Bruh");
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error-----> " + ex.Message);
            }
        }

        //Để khắc phục chuyện này, FormManagement sẽ được sinh ra cho việc tối ưu hoá và xử lý code gọn
        private void btn_sua_Click(object sender, EventArgs e)
        {
            if(string.Compare(btn_sua.Text, "Sửa",true) == 0)
            {

                //Kiểm tra xem bảng cần load có được chọn chưa?
                if (cbo_tableName.SelectedItem == null)
                {
                    MessageBox.Show("Bạn chưa chọn bảng");
                    return;
                }

                //Kiểm tra xem bảng chọn có phải là PhucVuBan không? nếu có thì return
                if (cbo_tableName.Text == "PhucVuBan")
                {
                    MessageBox.Show("Không thể thao tác bảng PhucVuBan");
                    return;
                }

                //Kiểm tra xem bảng chọn có phải là DuyetThucDon không? nếu có thì return
                if (cbo_tableName.Text == "DuyetThucDon")
                {
                    MessageBox.Show("Đang bảo trì!");
                    return;
                }

                //Kiểm tra xem dữ liệu được chọn có rỗng không
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.IsNewRow)
                    {
                        MessageBox.Show("Đùa không vui, làm gì có dữ liệu ở đó", "Bruh");
                        return; // Thoát chức năng 'xoá' liền khi dữ liệu rỗng
                    }
                    //Ẩn các nút 'Thêm', 'Xoá' lại khi thực hiện thao tác
                    btn_them.Enabled = false;
                    btn_xoa.Enabled = false;
                    cbo_tableName.Enabled = false; // Không cho thao tác trên bảng
                    
                    //Đổi tên nút 'Sửa' thành 'Cập nhật'
                    btn_sua.Text = "Cập nhật";

                    //Lấy các thông tin cần cập nhật
                    int i = 0;
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        if (i == 0)
                            CollectingData.txt_box1 = cell.Value.ToString().Trim(); //thường chứa dữ liệu khoá chính duy nhất
                        else if (i == 1)
                            CollectingData.txt_box2 = cell.Value.ToString().Trim();
                        else if (i == 2)
                            CollectingData.txt_box3 = cell.Value.ToString().Trim();
                        else if (i == 3)
                            CollectingData.txt_box4 = cell.Value.ToString().Trim();
                        else if (i == 4)
                            CollectingData.txt_box5 = cell.Value.ToString().Trim();
                        else
                            CollectingData.txt_box6 = cell.Value.ToString().Trim();
                        i++;
                    }

                    //Mở form mới để cập nhật thông tin cần thiết
                    TableName.tableName = cbo_tableName.SelectedItem.ToString();
                    Form update_data = new Them_Sua();
                    update_data.ShowDialog();
                    update_data.Text = "Cập nhật dữ liệu";
                }
            }
            else
            {
                if(TableName.tableName != "TaiKhoan")
                    btn_them.Enabled = true;
                btn_xoa.Enabled = true;
                cbo_tableName.Enabled = true;
                btn_sua.Text = "Sửa";

                //Cập nhật lại bảng sau khi cập nhật
                if (dataGridView1.Rows.Count > 0)
                {
                    DataTable tableData = GetDataFromTable(cbo_tableName.Text);
                    dataGridView1.DataSource = tableData;
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                DataTable tableData = GetDataFromTable(cbo_tableName.Text);
                dataGridView1.DataSource = tableData;
            }
        }

        private void btn_truyVan_Click(object sender, EventArgs e)
        {
            //Kiểm tra xem bảng cần load có được chọn chưa?
            if (cbo_tableName.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn bảng");
                return;
            }
            //Tạm thời không lọc các dữ liệu từ bảng này

            TableName.tableName = cbo_tableName.SelectedItem.ToString();
            Xem truyvan = new Xem();
            truyvan.Show();
        }

        private void menuStrip_CapNhatBan_Click(object sender, EventArgs e)
        {
            //DialogResult r = MessageBox.Show("Bạn có chắc chắn rằng sẽ cập nhật tất cả 'trạng thái' bàn không?", 
            //    "Cảnh báo!", 
            //    MessageBoxButtons.YesNo);
            //if(r == DialogResult.Yes)
            //{
            //    SqlCommand cmd = new SqlCommand("exec CapNhatTrangThaiBan", DatabaseConnection.con);
            //    cmd.ExecuteNonQuery();
            //    //MessageBox.Show("Đã cập nhật tất cả trạng thái bàn thành công");

            //}
        }

        private void menuStrip_XoaDBQuaThoiHan_Click(object sender, EventArgs e)
        {
            //DialogResult r = MessageBox.Show("Bạn có chắc chắn rằng sẽ xoá tất cả các 'thông tin đặt bàn' đã qua thời hạn không? " +
            //    "Điều này sẽ không còn lưu các thông tin của đặt bàn một khi đã xoá hết",
            //    "Cảnh báo!",
            //    MessageBoxButtons.YesNo);
            //if (r == DialogResult.Yes)
            //{
            //    SqlCommand cmd = new SqlCommand("exec XoaDatBanCu", DatabaseConnection.con);
            //    cmd.ExecuteNonQuery();
            //    MessageBox.Show("Đã xoá tất cả các bàn đặt đã qua thời hạn thành công");
            //}
        }

        private void btn_timKiemTenMon_Click(object sender, EventArgs e)
        {
            
            //string tenMon = txt_tenMonAn.Text;
            //using (SqlCommand cmd = new SqlCommand($"SELECT * FROM dbo.fn_TimKiemMonAn(N'{tenMon}')", DatabaseConnection.con))
            //{
            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    DataTable tabledata = new DataTable();
            //    sda.Fill(tabledata);
            //    if (tabledata.Rows.Count > 0)
            //    {
            //        dataGridView1.DataSource = tabledata;
            //    }
            //    else
            //    {
            //        MessageBox.Show($"Không tìm thấy tên món ăn liên quan '{tenMon}'");
            //    }
            //}
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
