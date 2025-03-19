using QL_NhaHang.Login_and_regist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.Method
{
    public partial class PhanQuyen : Form
    {
        public PhanQuyen()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoResizeColumns();
        }
        private void PhanQuyen_Load(object sender, EventArgs e)
        {
            userLoad();
            quyenLoad();
        }

        private void userLoad()
        {
            List<string> users = new List<string>();
            string query = "exec SP_Select_UserInDatabase";

            using (SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(reader["name"].ToString());
                    }
                }
            }

            comboBox4.Items.AddRange(users.ToArray());
        }

        private void quyenLoad()
        {
            string[] s = new string[] { "Grant", "Revoke" };
            comboBox1.Items.AddRange(s);
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.DestroyHandle();
        }

        private void btn_capQuyen_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Grant")
            {
                string query = "exec SP_Grant_ChoUser '" + comboBox4.Text + "'";
                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                cmd.ExecuteNonQuery();
            }
            else
            {
                string query = "exec SP_Revoke_ChoUser '" + comboBox4.Text + "'";
                SqlCommand cmd = new SqlCommand(query, DatabaseConnection.con);
                cmd.ExecuteNonQuery();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PhanQuyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            btn_thoat.PerformClick();
        }

        

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string tenUser = comboBox4.Text.Trim();
                SqlCommand cmd = new SqlCommand($"exec SP_XemQuyenUser {tenUser}", DatabaseConnection.con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
