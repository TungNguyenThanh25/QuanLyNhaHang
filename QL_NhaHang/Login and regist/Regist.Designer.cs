namespace QL_NhaHang.Login_and_regist
{
    partial class Regist
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_dangNhap = new System.Windows.Forms.TextBox();
            this.txt_matKhau = new System.Windows.Forms.TextBox();
            this.txt_xacNhanMK = new System.Windows.Forms.TextBox();
            this.btn_dangKy = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.link_dangNhap = new System.Windows.Forms.LinkLabel();
            this.check_showPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(89, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tên đăng nhập";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(89, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mật khẩu";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 19);
            this.label6.TabIndex = 4;
            this.label6.Text = "Xác nhận mật khẩu";
            // 
            // txt_dangNhap
            // 
            this.txt_dangNhap.Location = new System.Drawing.Point(93, 130);
            this.txt_dangNhap.Name = "txt_dangNhap";
            this.txt_dangNhap.Size = new System.Drawing.Size(264, 27);
            this.txt_dangNhap.TabIndex = 1;
            this.txt_dangNhap.TextChanged += new System.EventHandler(this.txt_dangNhap_TextChanged);
            // 
            // txt_matKhau
            // 
            this.txt_matKhau.Location = new System.Drawing.Point(93, 192);
            this.txt_matKhau.Name = "txt_matKhau";
            this.txt_matKhau.Size = new System.Drawing.Size(264, 27);
            this.txt_matKhau.TabIndex = 2;
            // 
            // txt_xacNhanMK
            // 
            this.txt_xacNhanMK.Location = new System.Drawing.Point(93, 253);
            this.txt_xacNhanMK.Name = "txt_xacNhanMK";
            this.txt_xacNhanMK.Size = new System.Drawing.Size(264, 27);
            this.txt_xacNhanMK.TabIndex = 3;
            // 
            // btn_dangKy
            // 
            this.btn_dangKy.Location = new System.Drawing.Point(93, 320);
            this.btn_dangKy.Name = "btn_dangKy";
            this.btn_dangKy.Size = new System.Drawing.Size(101, 35);
            this.btn_dangKy.TabIndex = 5;
            this.btn_dangKy.Text = "Đăng ký";
            this.btn_dangKy.UseVisualStyleBackColor = true;
            this.btn_dangKy.Click += new System.EventHandler(this.btn_dangKy_ClickAsync);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label7.Location = new System.Drawing.Point(97, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 17);
            this.label7.TabIndex = 8;
            this.label7.Text = "Đã có tài khoản?";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label8.Font = new System.Drawing.Font("Viner Hand ITC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(151, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 52);
            this.label8.TabIndex = 9;
            this.label8.Text = "Đăng ký";
            // 
            // link_dangNhap
            // 
            this.link_dangNhap.AutoSize = true;
            this.link_dangNhap.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.link_dangNhap.LinkColor = System.Drawing.Color.DarkSlateGray;
            this.link_dangNhap.Location = new System.Drawing.Point(221, 380);
            this.link_dangNhap.Name = "link_dangNhap";
            this.link_dangNhap.Size = new System.Drawing.Size(125, 25);
            this.link_dangNhap.TabIndex = 6;
            this.link_dangNhap.TabStop = true;
            this.link_dangNhap.Text = "Đăng nhập";
            this.link_dangNhap.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_dangNhap_LinkClicked);
            // 
            // check_showPassword
            // 
            this.check_showPassword.AutoSize = true;
            this.check_showPassword.Location = new System.Drawing.Point(237, 288);
            this.check_showPassword.Name = "check_showPassword";
            this.check_showPassword.Size = new System.Drawing.Size(125, 23);
            this.check_showPassword.TabIndex = 4;
            this.check_showPassword.Text = "Hiện mật khẩu";
            this.check_showPassword.UseVisualStyleBackColor = true;
            this.check_showPassword.CheckedChanged += new System.EventHandler(this.check_showPassword_CheckedChanged);
            // 
            // Regist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(457, 459);
            this.Controls.Add(this.check_showPassword);
            this.Controls.Add(this.link_dangNhap);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_dangKy);
            this.Controls.Add(this.txt_xacNhanMK);
            this.Controls.Add(this.txt_matKhau);
            this.Controls.Add(this.txt_dangNhap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Times New Roman", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Regist";
            this.Text = "Regist page";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Regist_FormClosing);
            this.Load += new System.EventHandler(this.Regist_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_dangNhap;
        private System.Windows.Forms.TextBox txt_matKhau;
        private System.Windows.Forms.TextBox txt_xacNhanMK;
        private System.Windows.Forms.Button btn_dangKy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel link_dangNhap;
        private System.Windows.Forms.CheckBox check_showPassword;
    }
}