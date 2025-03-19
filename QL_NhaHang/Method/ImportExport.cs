using QL_NhaHang;
using QL_NhaHang.Login_and_regist;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Demo_BCP
{
    public partial class ImportExport : Form
    {
        private SqlCommand cmd;
        private SqlDataAdapter sda;
        private DataTable dt;
        private string serverName = DatabaseConnection.IP_Address;
        private string userName = DangNhap.dangNhap;
        private string password = DangNhap.matKhau;

        public ImportExport()
        {
            InitializeComponent();
            dataGridView.AutoResizeColumns();
            dataGridView.AllowUserToAddRows = false;
        }
        //Load du
        void load_cbo_TableNames()
        {
            string query = "exec SP_Select_TenTatCaBang";
            sda = new SqlDataAdapter(query, DatabaseConnection.con);
            dt = new DataTable();
            sda.Fill(dt);

            cboBang.DataSource = dt;
            cboBang.DisplayMember = "TABLE_NAME";
        }

        // Form chức năng
        private void ImportExport_Load(object sender, EventArgs e)
        {
            load_cbo_TableNames();
        }

        private void ExportData(string tableName, string filePath)
        {
            try
            {
                //Lấy tên các cột trong bảng
                string query = $"EXEC SP_Select_CotTable '{tableName}'";
                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                SqlDataReader reader = cmd.ExecuteReader();

                //Tạo tiêu đề cột cho file CSV
                List<string> columnNames = new List<string>();
                while (reader.Read())
                {
                    columnNames.Add(reader["COLUMN_NAME"].ToString());
                }
                reader.Close();

                // Xuất dữ liệu bằng lệnh BCP (chỉ dữ liệu, không có tiêu đề)
                string tempFilePath = System.IO.Path.GetTempFileName(); // Tạo file tạm
                string bcpCommand = $"bcp {tableName} out \"{tempFilePath}\" -w -C 65001 -t, -U {userName} -P {password} -S {serverName} -d QL_NHAHANG";

                // Bắt đầu lệnh BCP
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C {bcpCommand}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                // Nếu lệnh BCP thành công, thêm tiêu đề cột và chuyển mã hóa sang UTF-8
                if (process.ExitCode == 0)
                {
                    // Đọc nội dung từ file đã xuất
                    string[] fileContent = System.IO.File.ReadAllLines(tempFilePath, System.Text.Encoding.Default);

                    // Thêm tiêu đề cột vào đầu file
                    string header = string.Join(",", columnNames);
                    List<string> finalContent = new List<string> { header };
                    finalContent.AddRange(fileContent);

                    // Ghi lại nội dung mới vào file CSV với mã hóa UTF-8
                    System.IO.File.WriteAllLines(filePath, finalContent, System.Text.Encoding.UTF8);

                    // Xóa file tạm
                    System.IO.File.Delete(tempFilePath);

                    MessageBox.Show("Export thành công");
                }
                else
                {
                    MessageBox.Show("Xuất dữ liệu thất bại!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void ImportData(string tableName, string filePath)
        {
            try
            {
                // Đọc tất cả nội dung của file CSV với mã hóa UTF-8
                string[] fileContent = System.IO.File.ReadAllLines(filePath, System.Text.Encoding.UTF8);

                // Bỏ qua dòng tiêu đề
                List<string> dataLines = fileContent.Skip(1).Select(line => line.Replace("\"", "")).ToList();  // Bỏ qua dòng đầu tiên (tiêu đề)

                // Tạo file tạm thời để chứa dữ liệu không có tiêu đề
                string tempFilePath = System.IO.Path.GetTempFileName();
                System.IO.File.WriteAllLines(tempFilePath, dataLines, System.Text.Encoding.UTF8);

                // Sử dụng lệnh BCP để import dữ liệu
                
                string bcpCommand = $"bcp Temp_{tableName} in \"{tempFilePath}\" -c -C 65001 -t, -U {userName} -P {password} -S {serverName} -d QL_NHAHANG";

                
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/C {bcpCommand}";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                process.WaitForExit();
               
                // Xóa file tạm thời sau khi import xong
                System.IO.File.Delete(tempFilePath);

                if (process.ExitCode == 0)
                {
                    string query = $"EXEC SP_Merge '{tableName}'";
                    cmd = new SqlCommand(query, DatabaseConnection.con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    MessageBox.Show("Import thành công");
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("Import dữ liệu thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }

            //try
            //{

            //    Kiểm tra ký tự có dấu
            //    Regex vietnameseRegex = new Regex("[À-ỹ]");
            //    if (vietnameseRegex.IsMatch(filePath))
            //        throw new Exception($"Không thể nhập file.csv từ đường dẫn {filePath}\n--> vì dữ liệu file không trùng khớp hoặc tên folder chứa ký tự có dấu");

            //    Thực hiện BCP import dữ liệu
            //    string query = "EXEC SP_BCP_Import" +
            //        $"    @TableName = '{tableName}'," +
            //        $"    @FilePath = '{filePath}'," +
            //        $"    @ServerName = '{serverName}'," +
            //        $"    @Username = '{userName}'," +
            //        $"    @Password = '{password}'";
            //    cmd = new SqlCommand(query, DatabaseConnection.con);
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error!");
            //}
        }

        void ExportAllTables(string directoryPath)
        {
            try
            {
                // Lấy danh sách tất cả các bảng trong cơ sở dữ liệu
                string query = "EXEC SP_Select_TenTatCaBang";
                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                SqlDataReader reader = cmd.ExecuteReader();

                //Lấy tất cả tên bảng trong cơ sở dữ liệu
                List<string> tableNames = new List<string>();
                while (reader.Read())
                {
                    //Thêm dữ liệu vào tableNames
                    tableNames.Add(reader["TABLE_NAME"].ToString());
                }
                reader.Close();

                // Xuất dữ liệu cho từng bảng
                foreach (var tableName in tableNames)
                {
                    // Tạo đường dẫn file cho từng bảng
                    string filePath = System.IO.Path.Combine(directoryPath, $"{tableName}.csv");

                    // Gọi hàm export dữ liệu từ bảng
                    ExportData(tableName, filePath);
                }
                MessageBox.Show("Export toàn bộ bảng thành công!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private void btnExport_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại để người dùng chọn nơi lưu file
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog.Title = "Chọn nơi lưu file CSV";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string tableName = cboBang.Text.Trim();
                    string filePath = saveFileDialog.FileName;  // Lấy đường dẫn người dùng chọn
                    ExportData(tableName, filePath);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại để người dùng chọn file cần import
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";  // Chỉ hiển thị file CSV
                openFileDialog.Title = "Chọn file CSV để import";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string tableName = cboBang.Text.Trim();  // Lấy bảng từ combobox
                    if (string.IsNullOrEmpty(tableName) == true)
                    {
                        MessageBox.Show("Bắt buộc chọn bảng");
                    }
                    else
                    {
                        string filePath = openFileDialog.FileName;  // Lấy đường dẫn file người dùng chọn
                        ImportData(tableName, filePath);  // Gọi hàm import
                    }
                }
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            // Mở hộp thoại để chọn thư mục lưu file CSV
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string directoryPath = folderDialog.SelectedPath;

                    // Gọi hàm export tất cả các bảng
                    ExportAllTables(directoryPath);
                }
            }
        }

        private void cboBang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                string tablename = cboBang.Text.Trim();

                if (tablename == "System.Data.DataRowView") // dữ liệu lấy từ debug, lúc chạy hiện dòng này đầu tiên nên sẽ bỏ qua
                    return;


                string query = $"EXEC SP_Select_{tablename}";
                // Tạo SqlDataAdapter để lấy dữ liệu từ cơ sở dữ liệu
                sda = new SqlDataAdapter(query, DatabaseConnection.con);
                // Tạo DataTable để chứa dữ liệu
                dt = new DataTable();

                // Đổ dữ liệu từ SqlDataAdapter vào DataTable
                sda.Fill(dt);

                // Hiển thị dữ liệu trong DataGridView
                dataGridView.DataSource = dt;

                // Tự động điều chỉnh kích thước các cột trong DataGridView để phù hợp với nội dung
                dataGridView.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ImportExport_FormClosing(object sender, FormClosingEventArgs e)
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
