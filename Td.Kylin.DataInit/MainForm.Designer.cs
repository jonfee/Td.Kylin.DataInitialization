namespace Td.Kylin.DataInit
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.groupDB = new System.Windows.Forms.GroupBox();
            this.txtInitDbPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInitDbAccount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInitDbServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.combInitType = new System.Windows.Forms.ComboBox();
            this.probarInit = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDownDbServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDownDbAccount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDownDbPwd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInitDbName = new System.Windows.Forms.TextBox();
            this.txtDownDbName = new System.Windows.Forms.TextBox();
            this.groupDB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.BackColor = System.Drawing.SystemColors.HighlightText;
            this.rtxtMsg.Location = new System.Drawing.Point(12, 245);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(874, 387);
            this.rtxtMsg.TabIndex = 0;
            this.rtxtMsg.Text = "";
            // 
            // groupDB
            // 
            this.groupDB.Controls.Add(this.txtDownDbName);
            this.groupDB.Controls.Add(this.txtInitDbName);
            this.groupDB.Controls.Add(this.label8);
            this.groupDB.Controls.Add(this.label7);
            this.groupDB.Controls.Add(this.txtDownDbPwd);
            this.groupDB.Controls.Add(this.txtInitDbPwd);
            this.groupDB.Controls.Add(this.label6);
            this.groupDB.Controls.Add(this.label3);
            this.groupDB.Controls.Add(this.txtDownDbAccount);
            this.groupDB.Controls.Add(this.label5);
            this.groupDB.Controls.Add(this.txtInitDbAccount);
            this.groupDB.Controls.Add(this.txtDownDbServer);
            this.groupDB.Controls.Add(this.label2);
            this.groupDB.Controls.Add(this.label4);
            this.groupDB.Controls.Add(this.txtInitDbServer);
            this.groupDB.Controls.Add(this.label1);
            this.groupDB.Location = new System.Drawing.Point(12, 13);
            this.groupDB.Name = "groupDB";
            this.groupDB.Size = new System.Drawing.Size(874, 114);
            this.groupDB.TabIndex = 1;
            this.groupDB.TabStop = false;
            this.groupDB.Text = "数据库";
            // 
            // txtInitDbPwd
            // 
            this.txtInitDbPwd.Location = new System.Drawing.Point(755, 32);
            this.txtInitDbPwd.Name = "txtInitDbPwd";
            this.txtInitDbPwd.Size = new System.Drawing.Size(100, 21);
            this.txtInitDbPwd.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(718, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码：";
            // 
            // txtInitDbAccount
            // 
            this.txtInitDbAccount.Location = new System.Drawing.Point(594, 32);
            this.txtInitDbAccount.Name = "txtInitDbAccount";
            this.txtInitDbAccount.Size = new System.Drawing.Size(100, 21);
            this.txtInitDbAccount.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "登录账号：";
            // 
            // txtInitDbServer
            // 
            this.txtInitDbServer.Location = new System.Drawing.Point(144, 33);
            this.txtInitDbServer.Name = "txtInitDbServer";
            this.txtInitDbServer.Size = new System.Drawing.Size(161, 21);
            this.txtInitDbServer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "初始化目标数据库地址：";
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(780, 30);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 9;
            this.btnDownload.Text = "下载最新";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(675, 30);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 8;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // combInitType
            // 
            this.combInitType.AllowDrop = true;
            this.combInitType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.combInitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combInitType.FormattingEnabled = true;
            this.combInitType.IntegralHeight = false;
            this.combInitType.ItemHeight = 20;
            this.combInitType.Location = new System.Drawing.Point(29, 27);
            this.combInitType.Name = "combInitType";
            this.combInitType.Size = new System.Drawing.Size(534, 26);
            this.combInitType.TabIndex = 7;
            this.combInitType.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.combInitType_DrawItem);
            // 
            // probarInit
            // 
            this.probarInit.Location = new System.Drawing.Point(12, 219);
            this.probarInit.Name = "probarInit";
            this.probarInit.Size = new System.Drawing.Size(874, 16);
            this.probarInit.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "下载更新源数据库地址：";
            // 
            // txtDownDbServer
            // 
            this.txtDownDbServer.Location = new System.Drawing.Point(144, 74);
            this.txtDownDbServer.Name = "txtDownDbServer";
            this.txtDownDbServer.Size = new System.Drawing.Size(161, 21);
            this.txtDownDbServer.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(533, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "登录账号：";
            // 
            // txtDownDbAccount
            // 
            this.txtDownDbAccount.Location = new System.Drawing.Point(594, 74);
            this.txtDownDbAccount.Name = "txtDownDbAccount";
            this.txtDownDbAccount.Size = new System.Drawing.Size(100, 21);
            this.txtDownDbAccount.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(718, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "密码：";
            // 
            // txtDownDbPwd
            // 
            this.txtDownDbPwd.Location = new System.Drawing.Point(755, 74);
            this.txtDownDbPwd.Name = "txtDownDbPwd";
            this.txtDownDbPwd.Size = new System.Drawing.Size(100, 21);
            this.txtDownDbPwd.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.combInitType);
            this.groupBox1.Controls.Add(this.btnInit);
            this.groupBox1.Location = new System.Drawing.Point(12, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(874, 72);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "业务操作";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(334, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 7;
            this.label7.Text = "数据库：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "数据库：";
            // 
            // txtInitDbName
            // 
            this.txtInitDbName.Location = new System.Drawing.Point(386, 33);
            this.txtInitDbName.Name = "txtInitDbName";
            this.txtInitDbName.Size = new System.Drawing.Size(113, 21);
            this.txtInitDbName.TabIndex = 8;
            // 
            // txtDownDbName
            // 
            this.txtDownDbName.Location = new System.Drawing.Point(386, 74);
            this.txtDownDbName.Name = "txtDownDbName";
            this.txtDownDbName.Size = new System.Drawing.Size(113, 21);
            this.txtDownDbName.TabIndex = 8;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 644);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.probarInit);
            this.Controls.Add(this.groupDB);
            this.Controls.Add(this.rtxtMsg);
            this.Name = "MainForm";
            this.Text = "Kylin数据初始化";
            this.groupDB.ResumeLayout(false);
            this.groupDB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.GroupBox groupDB;
        private System.Windows.Forms.ComboBox combInitType;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.ProgressBar probarInit;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtInitDbPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInitDbAccount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInitDbServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDownDbPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDownDbAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDownDbServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDownDbName;
        private System.Windows.Forms.TextBox txtInitDbName;
    }
}

