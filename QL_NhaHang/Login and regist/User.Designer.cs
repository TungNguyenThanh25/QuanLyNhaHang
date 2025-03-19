namespace QL_NhaHang.Login_and_regist
{
    partial class User
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
            this.timer_logout = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txt_NguoiDung = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_dangXuat = new System.Windows.Forms.Button();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer_logout
            // 
            this.timer_logout.Interval = 5000;
            this.timer_logout.Tick += new System.EventHandler(this.timer_logout_Tick);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(180, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "User";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_NguoiDung
            // 
            this.txt_NguoiDung.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txt_NguoiDung.ForeColor = System.Drawing.Color.Green;
            this.txt_NguoiDung.Location = new System.Drawing.Point(77, 46);
            this.txt_NguoiDung.Name = "txt_NguoiDung";
            this.txt_NguoiDung.Size = new System.Drawing.Size(400, 23);
            this.txt_NguoiDung.TabIndex = 12;
            this.txt_NguoiDung.Text = "Tên của người dùng";
            this.txt_NguoiDung.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelActions
            // 
            this.panelActions.BackColor = System.Drawing.Color.Bisque;
            this.panelActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelActions.Controls.Add(this.button1);
            this.panelActions.Location = new System.Drawing.Point(77, 79);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(400, 86);
            this.panelActions.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.button1.Location = new System.Drawing.Point(59, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(275, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Đặt món ăn (Menu)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btn_datMonAn_Click);
            // 
            // btn_dangXuat
            // 
            this.btn_dangXuat.BackColor = System.Drawing.Color.Red;
            this.btn_dangXuat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dangXuat.ForeColor = System.Drawing.Color.White;
            this.btn_dangXuat.Location = new System.Drawing.Point(187, 171);
            this.btn_dangXuat.Name = "btn_dangXuat";
            this.btn_dangXuat.Size = new System.Drawing.Size(170, 34);
            this.btn_dangXuat.TabIndex = 14;
            this.btn_dangXuat.Text = "Đăng xuất";
            this.btn_dangXuat.UseVisualStyleBackColor = false;
            this.btn_dangXuat.Click += new System.EventHandler(this.btn_dangXuat_Click);
            // 
            // User
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 290);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_dangXuat);
            this.Controls.Add(this.txt_NguoiDung);
            this.Controls.Add(this.panelActions);
            this.Name = "User";
            this.Text = "User";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.User_FormClosing);
            this.Load += new System.EventHandler(this.User_Load);
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer_logout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txt_NguoiDung;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_dangXuat;
    }
}