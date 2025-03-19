using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.Chức_năng_chính_để_quản_lý
{
    public partial class InputPhuongThucThanhToan : Form
    {
        // Thuộc tính để lưu dữ liệu mà người dùng nhập
        public string UserInput { get; set; }

        public InputPhuongThucThanhToan()
        {
            InitializeComponent();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            UserInput = textBox1.Text;
            this.DialogResult = DialogResult.OK; // Đóng form và trả kết quả OK
            this.Close();
        }
    }
}
