namespace QL_NhaHang
{
    partial class Login
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_DangNhap = new System.Windows.Forms.TextBox();
            this.txt_MatKhau = new System.Windows.Forms.TextBox();
            this.btn_DangNhap = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label3 = new System.Windows.Forms.Label();
            this.check_showPassword = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.link_dangKy = new System.Windows.Forms.LinkLabel();
            this.btn_kiemTraKetNoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(312, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tài khoản";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 197);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật khẩu";
            // 
            // txt_DangNhap
            // 
            this.txt_DangNhap.Location = new System.Drawing.Point(402, 146);
            this.txt_DangNhap.Name = "txt_DangNhap";
            this.txt_DangNhap.Size = new System.Drawing.Size(196, 27);
            this.txt_DangNhap.TabIndex = 1;
            this.txt_DangNhap.TextChanged += new System.EventHandler(this.txt_DangNhap_TextChanged);
            // 
            // txt_MatKhau
            // 
            this.txt_MatKhau.Location = new System.Drawing.Point(402, 189);
            this.txt_MatKhau.Name = "txt_MatKhau";
            this.txt_MatKhau.Size = new System.Drawing.Size(196, 27);
            this.txt_MatKhau.TabIndex = 2;
            // 
            // btn_DangNhap
            // 
            this.btn_DangNhap.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_DangNhap.Location = new System.Drawing.Point(315, 256);
            this.btn_DangNhap.Name = "btn_DangNhap";
            this.btn_DangNhap.Size = new System.Drawing.Size(98, 30);
            this.btn_DangNhap.TabIndex = 4;
            this.btn_DangNhap.Text = "Đăng nhập";
            this.btn_DangNhap.UseVisualStyleBackColor = false;
            this.btn_DangNhap.Click += new System.EventHandler(this.btn_DangNhap_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Aquamarine;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(254, 411);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Viner Hand ITC", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(313, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 39);
            this.label3.TabIndex = 0;
            this.label3.Text = "Đăng nhập";
            // 
            // check_showPassword
            // 
            this.check_showPassword.AutoSize = true;
            this.check_showPassword.Location = new System.Drawing.Point(480, 226);
            this.check_showPassword.Name = "check_showPassword";
            this.check_showPassword.Size = new System.Drawing.Size(125, 23);
            this.check_showPassword.TabIndex = 3;
            this.check_showPassword.Text = "Hiện mật khẩu";
            this.check_showPassword.UseVisualStyleBackColor = true;
            this.check_showPassword.CheckedChanged += new System.EventHandler(this.check_showPassword_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label4.Location = new System.Drawing.Point(317, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Không có tài khoản?";
            // 
            // link_dangKy
            // 
            this.link_dangKy.AutoSize = true;
            this.link_dangKy.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_dangKy.LinkColor = System.Drawing.Color.DarkSlateGray;
            this.link_dangKy.Location = new System.Drawing.Point(464, 335);
            this.link_dangKy.Name = "link_dangKy";
            this.link_dangKy.Size = new System.Drawing.Size(98, 25);
            this.link_dangKy.TabIndex = 5;
            this.link_dangKy.TabStop = true;
            this.link_dangKy.Text = "Đăng ký";
            this.link_dangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_dangKy_LinkClicked);
            // 
            // btn_kiemTraKetNoi
            // 
            this.btn_kiemTraKetNoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_kiemTraKetNoi.Location = new System.Drawing.Point(77, 59);
            this.btn_kiemTraKetNoi.Name = "btn_kiemTraKetNoi";
            this.btn_kiemTraKetNoi.Size = new System.Drawing.Size(87, 70);
            this.btn_kiemTraKetNoi.TabIndex = 6;
            this.btn_kiemTraKetNoi.Text = "Kiểm tra kết nối";
            this.btn_kiemTraKetNoi.UseVisualStyleBackColor = true;
            this.btn_kiemTraKetNoi.Visible = false;
            this.btn_kiemTraKetNoi.Click += new System.EventHandler(this.btn_kiemTraKetNoi_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(668, 411);
            this.Controls.Add(this.btn_kiemTraKetNoi);
            this.Controls.Add(this.link_dangKy);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.check_showPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.btn_DangNhap);
            this.Controls.Add(this.txt_MatKhau);
            this.Controls.Add(this.txt_DangNhap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login";
            this.Text = "Login page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DangNhap;
        private System.Windows.Forms.TextBox txt_MatKhau;
        private System.Windows.Forms.Button btn_DangNhap;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox check_showPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel link_dangKy;
        private System.Windows.Forms.Button btn_kiemTraKetNoi;
    }
}

