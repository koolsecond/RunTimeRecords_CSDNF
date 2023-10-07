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
            this.processListLabel = new System.Windows.Forms.Label();
            this.processListView = new System.Windows.Forms.ListView();
            this.windowName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.runTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.executablePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.whiteListGridView = new System.Windows.Forms.DataGridView();
            this.directoryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addWhiteDirectoryButton = new System.Windows.Forms.Button();
            this.addWhiteDirectory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whiteListGridView)).BeginInit();
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
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 404);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.processListLabel);
            this.tabPage1.Controls.Add(this.processListView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監視";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.windowName,
            this.processStartDate,
            this.runTime,
            this.processId,
            this.executablePath});
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
            // windowName
            // 
            this.windowName.Text = "ウィンドウ名";
            this.windowName.Width = 200;
            // 
            // processStartDate
            // 
            this.processStartDate.Text = "プロセス開始時刻";
            this.processStartDate.Width = 150;
            // 
            // runTime
            // 
            this.runTime.Text = "実行時間";
            this.runTime.Width = 80;
            // 
            // processId
            // 
            this.processId.Text = "プロセスID";
            // 
            // executablePath
            // 
            this.executablePath.Text = "実行ファイルパス";
            this.executablePath.Width = 180;
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.whiteListGridView);
            this.tabPage3.Controls.Add(this.addWhiteDirectoryButton);
            this.tabPage3.Controls.Add(this.addWhiteDirectory);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "対象";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // whiteListGridView
            // 
            this.whiteListGridView.AllowUserToAddRows = false;
            this.whiteListGridView.AllowUserToDeleteRows = false;
            this.whiteListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.whiteListGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.whiteListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.whiteListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.directoryPath,
            this.deleteButton});
            this.whiteListGridView.Location = new System.Drawing.Point(10, 62);
            this.whiteListGridView.Name = "whiteListGridView";
            this.whiteListGridView.RowTemplate.Height = 21;
            this.whiteListGridView.Size = new System.Drawing.Size(283, 313);
            this.whiteListGridView.TabIndex = 4;
            this.whiteListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.WhiteListGridView_CellContentClick);
            // 
            // directoryPath
            // 
            this.directoryPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.directoryPath.Frozen = true;
            this.directoryPath.HeaderText = "フォルダパス";
            this.directoryPath.Name = "directoryPath";
            this.directoryPath.ReadOnly = true;
            this.directoryPath.Width = 84;
            // 
            // deleteButton
            // 
            this.deleteButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.deleteButton.Frozen = true;
            this.deleteButton.HeaderText = "削除";
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.deleteButton.Text = "削除";
            this.deleteButton.UseColumnTextForButtonValue = true;
            this.deleteButton.Width = 50;
            // 
            // addWhiteDirectoryButton
            // 
            this.addWhiteDirectoryButton.Location = new System.Drawing.Point(218, 23);
            this.addWhiteDirectoryButton.Name = "addWhiteDirectoryButton";
            this.addWhiteDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.addWhiteDirectoryButton.TabIndex = 2;
            this.addWhiteDirectoryButton.Text = "追加";
            this.addWhiteDirectoryButton.UseVisualStyleBackColor = true;
            this.addWhiteDirectoryButton.Click += new System.EventHandler(this.WhiteDirectoryButton_Click);
            // 
            // addWhiteDirectory
            // 
            this.addWhiteDirectory.Location = new System.Drawing.Point(10, 25);
            this.addWhiteDirectory.Name = "addWhiteDirectory";
            this.addWhiteDirectory.Size = new System.Drawing.Size(202, 19);
            this.addWhiteDirectory.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "ホワイトリスト追加";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ホワイトリスト";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
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
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whiteListGridView)).EndInit();
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
        private System.Windows.Forms.ColumnHeader windowName;
        private System.Windows.Forms.ColumnHeader processId;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColumnHeader runTime;
        private System.Windows.Forms.ColumnHeader processStartDate;
        private System.Windows.Forms.ColumnHeader executablePath;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addWhiteDirectoryButton;
        private System.Windows.Forms.TextBox addWhiteDirectory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView whiteListGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn directoryPath;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
    }
}

