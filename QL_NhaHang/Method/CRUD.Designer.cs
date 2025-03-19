namespace QL_NhaHang.Method
{
    partial class CRUD
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
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_CapNhatBan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip_XoaDBQuaThoiHan = new System.Windows.Forms.ToolStripMenuItem();
            this.cbo_tableName = new System.Windows.Forms.ComboBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_loc = new System.Windows.Forms.Button();
            this.btn_timKiemTenMon = new System.Windows.Forms.Button();
            this.txt_tenMonAn = new System.Windows.Forms.TextBox();
            this.btn_thoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(102, 216);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(763, 215);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuStrip_CapNhatBan,
            this.menuStrip_XoaDBQuaThoiHan});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menuStrip_CapNhatBan
            // 
            this.menuStrip_CapNhatBan.Name = "menuStrip_CapNhatBan";
            this.menuStrip_CapNhatBan.Size = new System.Drawing.Size(256, 22);
            this.menuStrip_CapNhatBan.Text = "Cập nhật toàn bộ trạng thái \'Bàn\'";
            this.menuStrip_CapNhatBan.Click += new System.EventHandler(this.menuStrip_CapNhatBan_Click);
            // 
            // menuStrip_XoaDBQuaThoiHan
            // 
            this.menuStrip_XoaDBQuaThoiHan.Name = "menuStrip_XoaDBQuaThoiHan";
            this.menuStrip_XoaDBQuaThoiHan.Size = new System.Drawing.Size(256, 22);
            this.menuStrip_XoaDBQuaThoiHan.Text = "Xoá các đơn \'Đặt bàn\' hết thời hạn";
            this.menuStrip_XoaDBQuaThoiHan.Click += new System.EventHandler(this.menuStrip_XoaDBQuaThoiHan_Click);
            // 
            // cbo_tableName
            // 
            this.cbo_tableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_tableName.FormattingEnabled = true;
            this.cbo_tableName.Location = new System.Drawing.Point(102, 143);
            this.cbo_tableName.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_tableName.Name = "cbo_tableName";
            this.cbo_tableName.Size = new System.Drawing.Size(180, 27);
            this.cbo_tableName.TabIndex = 1;
            this.cbo_tableName.SelectedIndexChanged += new System.EventHandler(this.cbo_tableName_SelectedIndexChanged);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(12, 12);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(80, 31);
            this.btn_test.TabIndex = 2;
            this.btn_test.Text = "Test";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Visible = false;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // btn_them
            // 
            this.btn_them.Location = new System.Drawing.Point(455, 135);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(86, 36);
            this.btn_them.TabIndex = 3;
            this.btn_them.Text = "Thêm";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.Location = new System.Drawing.Point(574, 136);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(80, 33);
            this.btn_xoa.TabIndex = 4;
            this.btn_xoa.Text = "Xoá";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.Location = new System.Drawing.Point(681, 135);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(85, 35);
            this.btn_sua.TabIndex = 5;
            this.btn_sua.Text = "Sửa";
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_loc
            // 
            this.btn_loc.Location = new System.Drawing.Point(785, 135);
            this.btn_loc.Name = "btn_loc";
            this.btn_loc.Size = new System.Drawing.Size(80, 36);
            this.btn_loc.TabIndex = 6;
            this.btn_loc.Text = "Lọc";
            this.btn_loc.UseVisualStyleBackColor = true;
            this.btn_loc.Click += new System.EventHandler(this.btn_truyVan_Click);
            // 
            // btn_timKiemTenMon
            // 
            this.btn_timKiemTenMon.Location = new System.Drawing.Point(288, 78);
            this.btn_timKiemTenMon.Name = "btn_timKiemTenMon";
            this.btn_timKiemTenMon.Size = new System.Drawing.Size(144, 31);
            this.btn_timKiemTenMon.TabIndex = 7;
            this.btn_timKiemTenMon.Text = "Tìm kiếm món ăn";
            this.btn_timKiemTenMon.UseVisualStyleBackColor = true;
            this.btn_timKiemTenMon.Click += new System.EventHandler(this.btn_timKiemTenMon_Click);
            // 
            // txt_tenMonAn
            // 
            this.txt_tenMonAn.Location = new System.Drawing.Point(102, 81);
            this.txt_tenMonAn.Name = "txt_tenMonAn";
            this.txt_tenMonAn.Size = new System.Drawing.Size(180, 26);
            this.txt_tenMonAn.TabIndex = 8;
            // 
            // btn_thoat
            // 
            this.btn_thoat.Location = new System.Drawing.Point(776, 20);
            this.btn_thoat.Name = "btn_thoat";
            this.btn_thoat.Size = new System.Drawing.Size(80, 33);
            this.btn_thoat.TabIndex = 9;
            this.btn_thoat.Text = "Thoát";
            this.btn_thoat.UseVisualStyleBackColor = true;
            this.btn_thoat.Click += new System.EventHandler(this.btn_thoat_Click);
            // 
            // CRUD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 468);
            this.Controls.Add(this.btn_thoat);
            this.Controls.Add(this.txt_tenMonAn);
            this.Controls.Add(this.btn_timKiemTenMon);
            this.Controls.Add(this.btn_loc);
            this.Controls.Add(this.btn_sua);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.cbo_tableName);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CRUD";
            this.Text = "Chức năng CRUD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TruyVan_FormClosing);
            this.Load += new System.EventHandler(this.TruyVan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbo_tableName;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.Button btn_loc;
        private System.Windows.Forms.Button btn_timKiemTenMon;
        private System.Windows.Forms.TextBox txt_tenMonAn;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_CapNhatBan;
        private System.Windows.Forms.ToolStripMenuItem menuStrip_XoaDBQuaThoiHan;
        private System.Windows.Forms.Button btn_thoat;
    }
}