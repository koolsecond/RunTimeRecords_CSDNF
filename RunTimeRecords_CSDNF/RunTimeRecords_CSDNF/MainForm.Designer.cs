namespace RunTimeRecords_CSDNF
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.testLabel = new System.Windows.Forms.Label();
            this.processListLabel = new System.Windows.Forms.Label();
            this.processListView = new System.Windows.Forms.ListView();
            this.processName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.userModeTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.kernelTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cpuTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 404);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.testLabel);
            this.tabPage1.Controls.Add(this.processListLabel);
            this.tabPage1.Controls.Add(this.processListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監視状態";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // testLabel
            // 
            this.testLabel.AutoSize = true;
            this.testLabel.Location = new System.Drawing.Point(233, 21);
            this.testLabel.Name = "testLabel";
            this.testLabel.Size = new System.Drawing.Size(35, 12);
            this.testLabel.TabIndex = 2;
            this.testLabel.Text = "label1";
            // 
            // processListLabel
            // 
            this.processListLabel.AutoSize = true;
            this.processListLabel.Location = new System.Drawing.Point(8, 31);
            this.processListLabel.Name = "processListLabel";
            this.processListLabel.Size = new System.Drawing.Size(66, 12);
            this.processListLabel.TabIndex = 1;
            this.processListLabel.Text = "プロセス一覧";
            // 
            // processListView
            // 
            this.processListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.processName,
            this.ID,
            this.processPath,
            this.Description,
            this.startTime,
            this.RunTime,
            this.userModeTime,
            this.kernelTime,
            this.cpuTime});
            this.processListView.FullRowSelect = true;
            this.processListView.GridLines = true;
            this.processListView.HideSelection = false;
            this.processListView.Location = new System.Drawing.Point(6, 46);
            this.processListView.Name = "processListView";
            this.processListView.Size = new System.Drawing.Size(764, 259);
            this.processListView.TabIndex = 0;
            this.processListView.UseCompatibleStateImageBehavior = false;
            this.processListView.View = System.Windows.Forms.View.Details;
            // 
            // processName
            // 
            this.processName.Text = "プロセス名";
            this.processName.Width = 100;
            // 
            // ID
            // 
            this.ID.Text = "id";
            // 
            // processPath
            // 
            this.processPath.Text = "ファイル名";
            // 
            // Description
            // 
            this.Description.Text = "説明";
            this.Description.Width = 100;
            // 
            // userModeTime
            // 
            this.userModeTime.Text = "ユーザ時間";
            this.userModeTime.Width = 100;
            // 
            // kernelTime
            // 
            this.kernelTime.Text = "カーネル時間";
            this.kernelTime.Width = 100;
            // 
            // cpuTime
            // 
            this.cpuTime.Text = "CPU時間";
            this.cpuTime.Width = 100;
            // 
            // RunTime
            // 
            this.RunTime.Text = "計測時間";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "集計";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // startTime
            // 
            this.startTime.Text = "計測開始時刻";
            this.startTime.Width = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "RunTimeRecords";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListView processListView;
        private System.Windows.Forms.Label processListLabel;
        private System.Windows.Forms.ColumnHeader processName;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader processPath;
        private System.Windows.Forms.ColumnHeader userModeTime;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label testLabel;
        private System.Windows.Forms.ColumnHeader kernelTime;
        private System.Windows.Forms.ColumnHeader cpuTime;
        private System.Windows.Forms.ColumnHeader RunTime;
        private System.Windows.Forms.ColumnHeader startTime;
    }
}

