namespace QL_NhaHang.Chức_năng_chính_để_quản_lý
{
    partial class DuyetMonAnKhachHangChon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_xemThongTinKHDatMon = new System.Windows.Forms.Button();
            this.btn_duyet = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_maKH = new System.Windows.Forms.TextBox();
            this.txt_tenKH = new System.Windows.Forms.TextBox();
            this.txt_gioiTinh = new System.Windows.Forms.TextBox();
            this.txt_sdt = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.btn_quayLai = new System.Windows.Forms.Button();
            this.btn_thoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(53, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(684, 164);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // btn_xemThongTinKHDatMon
            // 
            this.btn_xemThongTinKHDatMon.Location = new System.Drawing.Point(53, 143);
            this.btn_xemThongTinKHDatMon.Name = "btn_xemThongTinKHDatMon";
            this.btn_xemThongTinKHDatMon.Size = new System.Drawing.Size(256, 27);
            this.btn_xemThongTinKHDatMon.TabIndex = 1;
            this.btn_xemThongTinKHDatMon.Text = "Xem thông tin khách hàng đặt món";
            this.btn_xemThongTinKHDatMon.UseVisualStyleBackColor = true;
            this.btn_xemThongTinKHDatMon.Click += new System.EventHandler(this.btn_xemThongTinKHDatMon_Click);
            // 
            // btn_duyet
            // 
            this.btn_duyet.Enabled = false;
            this.btn_duyet.Location = new System.Drawing.Point(324, 143);
            this.btn_duyet.Name = "btn_duyet";
            this.btn_duyet.Size = new System.Drawing.Size(127, 27);
            this.btn_duyet.TabIndex = 2;
            this.btn_duyet.Text = "Duyệt thông tin";
            this.btn_duyet.UseVisualStyleBackColor = true;
            this.btn_duyet.Click += new System.EventHandler(this.btn_duyet_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mã Khách hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tên khách hàng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Giới tính";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Số điện thoại";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(473, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 19);
            this.label5.TabIndex = 3;
            this.label5.Text = "Email";
            // 
            // txt_maKH
            // 
            this.txt_maKH.Location = new System.Drawing.Point(168, 26);
            this.txt_maKH.Name = "txt_maKH";
            this.txt_maKH.ReadOnly = true;
            this.txt_maKH.Size = new System.Drawing.Size(93, 27);
            this.txt_maKH.TabIndex = 4;
            // 
            // txt_tenKH
            // 
            this.txt_tenKH.Location = new System.Drawing.Point(167, 62);
            this.txt_tenKH.Name = "txt_tenKH";
            this.txt_tenKH.ReadOnly = true;
            this.txt_tenKH.Size = new System.Drawing.Size(220, 27);
            this.txt_tenKH.TabIndex = 4;
            // 
            // txt_gioiTinh
            // 
            this.txt_gioiTinh.Location = new System.Drawing.Point(167, 98);
            this.txt_gioiTinh.Name = "txt_gioiTinh";
            this.txt_gioiTinh.ReadOnly = true;
            this.txt_gioiTinh.Size = new System.Drawing.Size(94, 27);
            this.txt_gioiTinh.TabIndex = 4;
            // 
            // txt_sdt
            // 
            this.txt_sdt.Location = new System.Drawing.Point(528, 65);
            this.txt_sdt.Name = "txt_sdt";
            this.txt_sdt.ReadOnly = true;
            this.txt_sdt.Size = new System.Drawing.Size(117, 27);
            this.txt_sdt.TabIndex = 4;
            // 
            // txt_email
            // 
            this.txt_email.Location = new System.Drawing.Point(528, 26);
            this.txt_email.Name = "txt_email";
            this.txt_email.ReadOnly = true;
            this.txt_email.Size = new System.Drawing.Size(209, 27);
            this.txt_email.TabIndex = 4;
            // 
            // btn_quayLai
            // 
            this.btn_quayLai.Enabled = false;
            this.btn_quayLai.Location = new System.Drawing.Point(662, 143);
            this.btn_quayLai.Name = "btn_quayLai";
            this.btn_quayLai.Size = new System.Drawing.Size(75, 27);
            this.btn_quayLai.TabIndex = 5;
            this.btn_quayLai.Text = "Quay lại";
            this.btn_quayLai.UseVisualStyleBackColor = true;
            this.btn_quayLai.Click += new System.EventHandler(this.btn_quayLai_Click);
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(818, 25);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(75, 28);
            this.btn_thoat.TabIndex = 6;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // DuyetMonAnKhachHangChon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 371);
            this.Controls.Add(this.btn_thoat);
            this.Controls.Add(this.btn_quayLai);
            this.Controls.Add(this.txt_gioiTinh);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_sdt);
            this.Controls.Add(this.txt_tenKH);
            this.Controls.Add(this.txt_maKH);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_duyet);
            this.Controls.Add(this.btn_xemThongTinKHDatMon);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DuyetMonAnKhachHangChon";
            this.Text = "DuyetMonAnKhachHangChon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DuyetMonAnKhachHangChon_FormClosing);
            this.Load += new System.EventHandler(this.DuyetMonAnKhachHangChon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_xemThongTinKHDatMon;
        private System.Windows.Forms.Button btn_duyet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_maKH;
        private System.Windows.Forms.TextBox txt_tenKH;
        private System.Windows.Forms.TextBox txt_gioiTinh;
        private System.Windows.Forms.TextBox txt_sdt;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.Button btn_quayLai;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Button btn_thoat;
    }
}