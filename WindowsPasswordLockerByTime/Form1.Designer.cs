namespace WindowsPasswordLockerByTime
{
    partial class Form1
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
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.radoAllUsers = new System.Windows.Forms.RadioButton();
            this.radoSelectrUser = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLockTimePass = new System.Windows.Forms.TextBox();
            this.txtUnlockPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lock Start Time";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lock End Time";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(16, 29);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(123, 20);
            this.dtpStartTime.TabIndex = 2;
            this.dtpStartTime.Value = new System.DateTime(2024, 8, 31, 22, 0, 0, 0);
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(164, 29);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(96, 20);
            this.dtpEndTime.TabIndex = 3;
            this.dtpEndTime.Value = new System.DateTime(2024, 8, 31, 8, 0, 0, 0);
            // 
            // radoAllUsers
            // 
            this.radoAllUsers.AutoSize = true;
            this.radoAllUsers.Checked = true;
            this.radoAllUsers.Location = new System.Drawing.Point(6, 19);
            this.radoAllUsers.Name = "radoAllUsers";
            this.radoAllUsers.Size = new System.Drawing.Size(66, 17);
            this.radoAllUsers.TabIndex = 4;
            this.radoAllUsers.TabStop = true;
            this.radoAllUsers.Text = "All Users";
            this.radoAllUsers.UseVisualStyleBackColor = true;
            this.radoAllUsers.CheckedChanged += new System.EventHandler(this.radoAllUsers_CheckedChanged);
            // 
            // radoSelectrUser
            // 
            this.radoSelectrUser.AutoSize = true;
            this.radoSelectrUser.Location = new System.Drawing.Point(78, 19);
            this.radoSelectrUser.Name = "radoSelectrUser";
            this.radoSelectrUser.Size = new System.Drawing.Size(79, 17);
            this.radoSelectrUser.TabIndex = 5;
            this.radoSelectrUser.Text = "Select User";
            this.radoSelectrUser.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.radoAllUsers);
            this.groupBox1.Controls.Add(this.radoSelectrUser);
            this.groupBox1.Location = new System.Drawing.Point(16, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 50);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "UserName:";
            // 
            // txtUserName
            // 
            this.txtUserName.Enabled = false;
            this.txtUserName.Location = new System.Drawing.Point(252, 16);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(188, 20);
            this.txtUserName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Lock Time Password";
            // 
            // txtLockTimePass
            // 
            this.txtLockTimePass.Location = new System.Drawing.Point(22, 128);
            this.txtLockTimePass.Name = "txtLockTimePass";
            this.txtLockTimePass.Size = new System.Drawing.Size(213, 20);
            this.txtLockTimePass.TabIndex = 8;
            // 
            // txtUnlockPass
            // 
            this.txtUnlockPass.Location = new System.Drawing.Point(268, 128);
            this.txtUnlockPass.Name = "txtUnlockPass";
            this.txtUnlockPass.Size = new System.Drawing.Size(188, 20);
            this.txtUnlockPass.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "UnLock Time Password(can be empty)";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(16, 173);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(450, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 208);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtUnlockPass);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtLockTimePass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.RadioButton radoAllUsers;
        private System.Windows.Forms.RadioButton radoSelectrUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLockTimePass;
        private System.Windows.Forms.TextBox txtUnlockPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnApply;
    }
}

