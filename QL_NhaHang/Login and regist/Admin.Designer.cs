namespace QL_NhaHang.Login_and_regist
{
    partial class Admin
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_dangXuat = new System.Windows.Forms.Button();
            this.timer_logout = new System.Windows.Forms.Timer(this.components);
            this.txt_NguoiDung = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btn_crud = new System.Windows.Forms.Button();
            this.btn_import_export = new System.Windows.Forms.Button();
            this.btn_datMonAn = new System.Windows.Forms.Button();
            this.btn_duyetMonAn = new System.Windows.Forms.Button();
            this.btn_XemDoanhThu = new System.Windows.Forms.Button();
            this.btn_phanQuyen = new System.Windows.Forms.Button();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(303, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Admin Dashboard";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_dangXuat
            // 
            this.btn_dangXuat.BackColor = System.Drawing.Color.Red;
            this.btn_dangXuat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dangXuat.ForeColor = System.Drawing.Color.White;
            this.btn_dangXuat.Location = new System.Drawing.Point(310, 486);
            this.btn_dangXuat.Name = "btn_dangXuat";
            this.btn_dangXuat.Size = new System.Drawing.Size(170, 34);
            this.btn_dangXuat.TabIndex = 3;
            this.btn_dangXuat.Text = "Đăng xuất";
            this.btn_dangXuat.UseVisualStyleBackColor = false;
            this.btn_dangXuat.Click += new System.EventHandler(this.btn_dangXuat_Click);
            // 
            // timer_logout
            // 
            this.timer_logout.Interval = 5000;
            this.timer_logout.Tag = "";
            this.timer_logout.Tick += new System.EventHandler(this.timer_logout_Tick);
            // 
            // txt_NguoiDung
            // 
            this.txt_NguoiDung.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txt_NguoiDung.ForeColor = System.Drawing.Color.Green;
            this.txt_NguoiDung.Location = new System.Drawing.Point(200, 47);
            this.txt_NguoiDung.Name = "txt_NguoiDung";
            this.txt_NguoiDung.Size = new System.Drawing.Size(400, 23);
            this.txt_NguoiDung.TabIndex = 1;
            this.txt_NguoiDung.Text = "Tên của người dùng";
            this.txt_NguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Bisque;
            this.panelActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActions.Controls.Add(this.btn_crud);
            this.panelActions.Controls.Add(this.btn_import_export);
            this.panelActions.Controls.Add(this.btn_datMonAn);
            this.panelActions.Controls.Add(this.btn_duyetMonAn);
            this.panelActions.Controls.Add(this.btn_XemDoanhThu);
            this.panelActions.Controls.Add(this.btn_phanQuyen);
            this.panelActions.Location = new System.Drawing.Point(200, 80);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(400, 400);
            this.panelActions.TabIndex = 2;
            // 
            // btn_crud
            // 
            this.btn_crud.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_crud.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_crud.Location = new System.Drawing.Point(59, 16);
            this.btn_crud.Name = "btn_crud";
            this.btn_crud.Size = new System.Drawing.Size(275, 47);
            this.btn_crud.TabIndex = 0;
            this.btn_crud.Text = "Chức năng CRUD";
            this.btn_crud.UseVisualStyleBackColor = false;
            this.btn_crud.Click += new System.EventHandler(this.btn_crud_Click);
            // 
            // btn_import_export
            // 
            this.btn_import_export.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_import_export.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_import_export.Location = new System.Drawing.Point(59, 76);
            this.btn_import_export.Name = "btn_import_export";
            this.btn_import_export.Size = new System.Drawing.Size(275, 47);
            this.btn_import_export.TabIndex = 1;
            this.btn_import_export.Text = "Import/Export";
            this.btn_import_export.UseVisualStyleBackColor = false;
            this.btn_import_export.Click += new System.EventHandler(this.btn_import_export_Click);
            // 
            // btn_datMonAn
            // 
            this.btn_datMonAn.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_datMonAn.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_datMonAn.Location = new System.Drawing.Point(59, 136);
            this.btn_datMonAn.Name = "btn_datMonAn";
            this.btn_datMonAn.Size = new System.Drawing.Size(275, 47);
            this.btn_datMonAn.TabIndex = 2;
            this.btn_datMonAn.Text = "Đặt món ăn (Menu)";
            this.btn_datMonAn.UseVisualStyleBackColor = false;
            this.btn_datMonAn.Click += new System.EventHandler(this.btn_datMonAn_Click);
            // 
            // btn_duyetMonAn
            // 
            this.btn_duyetMonAn.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_duyetMonAn.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_duyetMonAn.Location = new System.Drawing.Point(59, 196);
            this.btn_duyetMonAn.Name = "btn_duyetMonAn";
            this.btn_duyetMonAn.Size = new System.Drawing.Size(281, 47);
            this.btn_duyetMonAn.TabIndex = 3;
            this.btn_duyetMonAn.Text = "Duyệt món ăn vào hóa đơn";
            this.btn_duyetMonAn.UseVisualStyleBackColor = false;
            this.btn_duyetMonAn.Click += new System.EventHandler(this.btn_duyetMonAn_Click);
            // 
            // btn_XemDoanhThu
            // 
            this.btn_XemDoanhThu.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_XemDoanhThu.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_XemDoanhThu.Location = new System.Drawing.Point(59, 256);
            this.btn_XemDoanhThu.Name = "btn_XemDoanhThu";
            this.btn_XemDoanhThu.Size = new System.Drawing.Size(275, 47);
            this.btn_XemDoanhThu.TabIndex = 4;
            this.btn_XemDoanhThu.Text = "Xem doanh thu";
            this.btn_XemDoanhThu.UseVisualStyleBackColor = false;
            this.btn_XemDoanhThu.Click += new System.EventHandler(this.btn_XemDoanhThu_Click);
            // 
            // btn_phanQuyen
            // 
            this.btn_phanQuyen.BackColor = System.Drawing.Color.AntiqueWhite;
            this.btn_phanQuyen.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.btn_phanQuyen.Location = new System.Drawing.Point(59, 316);
            this.btn_phanQuyen.Name = "btn_phanQuyen";
            this.btn_phanQuyen.Size = new System.Drawing.Size(275, 47);
            this.btn_phanQuyen.TabIndex = 5;
            this.btn_phanQuyen.Text = "Phân quyền";
            this.btn_phanQuyen.UseVisualStyleBackColor = false;
            this.btn_phanQuyen.Click += new System.EventHandler(this.btn_phanQuyen_Click);
            // 
            // Admin
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_NguoiDung);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.btn_dangXuat);
            this.Name = "Admin";
            this.Text = "Admin Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Admin_FormClosing);
            this.Load += new System.EventHandler(this.Admin_Load);
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_dangXuat;
        private System.Windows.Forms.Timer timer_logout;
        private System.Windows.Forms.Label txt_NguoiDung;
        private System.Windows.Forms.Button btn_crud;
        private System.Windows.Forms.Button btn_import_export;
        private System.Windows.Forms.Button btn_datMonAn;
        private System.Windows.Forms.Button btn_XemDoanhThu;
        private System.Windows.Forms.Button btn_duyetMonAn;
        private System.Windows.Forms.Button btn_phanQuyen;
        private System.Windows.Forms.Panel panelActions;
    }
}