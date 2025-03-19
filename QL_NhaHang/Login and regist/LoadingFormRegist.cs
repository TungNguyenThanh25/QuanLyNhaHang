using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NhaHang.Login_and_regist
{
    public partial class LoadingFormRegist : Form
    {
        public LoadingFormRegist()
        {
            InitializeComponent();
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value++;
            }
            else
            {
                timer1.Stop();
                progressBar1.Value = 0;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void LoadingFormRegist_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
