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
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.historyPanel = new System.Windows.Forms.Panel();
            this.HistoryListView = new System.Windows.Forms.ListView();
            this.historyWindowName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.historyProcessStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.historyRunTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HistoryProcessId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HistoryExecutablePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.historyLabel = new System.Windows.Forms.Label();
            this.processPanel = new System.Windows.Forms.Panel();
            this.processListView = new System.Windows.Forms.ListView();
            this.windowName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processStartDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.runTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.executablePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processListLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.summaryListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.whiteListPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.addWhiteDirectory = new System.Windows.Forms.TextBox();
            this.addWhiteDirectoryButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.whiteListGridView = new System.Windows.Forms.DataGridView();
            this.directoryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteWhiteListButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.blackListPanel = new System.Windows.Forms.Panel();
            this.blackListGridView = new System.Windows.Forms.DataGridView();
            this.blackDirectoryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteBlackListButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.addBlackDirectoryButton = new System.Windows.Forms.Button();
            this.addBlackDirectory = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ToolStripMenuItemOpenDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.historyPanel.SuspendLayout();
            this.processPanel.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.whiteListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whiteListGridView)).BeginInit();
            this.blackListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blackListGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItemFile
            // 
            this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSave,
            this.ToolStripMenuItemOpenDirectory});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            this.ToolStripMenuItemFile.Size = new System.Drawing.Size(67, 20);
            this.ToolStripMenuItemFile.Text = "ファイル(&F)";
            // 
            // ToolStripMenuItemSave
            // 
            this.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
            this.ToolStripMenuItemSave.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemSave.Text = "保存(&S)";
            this.ToolStripMenuItemSave.Click += new System.EventHandler(this.ToolStripMenuItemSave_Click);
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
            this.tabPage1.Controls.Add(this.historyPanel);
            this.tabPage1.Controls.Add(this.processPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "監視";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // historyPanel
            // 
            this.historyPanel.BackColor = System.Drawing.Color.Lime;
            this.historyPanel.Controls.Add(this.HistoryListView);
            this.historyPanel.Controls.Add(this.historyLabel);
            this.historyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyPanel.Location = new System.Drawing.Point(3, 118);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(786, 257);
            this.historyPanel.TabIndex = 3;
            // 
            // HistoryListView
            // 
            this.HistoryListView.BackColor = System.Drawing.Color.PaleGreen;
            this.HistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.historyWindowName,
            this.historyProcessStartDate,
            this.historyRunTime,
            this.HistoryProcessId,
            this.HistoryExecutablePath});
            this.HistoryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HistoryListView.FullRowSelect = true;
            this.HistoryListView.GridLines = true;
            this.HistoryListView.HideSelection = false;
            this.HistoryListView.Location = new System.Drawing.Point(0, 12);
            this.HistoryListView.Name = "HistoryListView";
            this.HistoryListView.Size = new System.Drawing.Size(786, 245);
            this.HistoryListView.TabIndex = 1;
            this.HistoryListView.UseCompatibleStateImageBehavior = false;
            this.HistoryListView.View = System.Windows.Forms.View.Details;
            // 
            // historyWindowName
            // 
            this.historyWindowName.Text = "ウィンドウ名";
            this.historyWindowName.Width = 200;
            // 
            // historyProcessStartDate
            // 
            this.historyProcessStartDate.Text = "プロセス開始時刻";
            this.historyProcessStartDate.Width = 150;
            // 
            // historyRunTime
            // 
            this.historyRunTime.Text = "実行時間";
            this.historyRunTime.Width = 80;
            // 
            // HistoryProcessId
            // 
            this.HistoryProcessId.Text = "プロセスID";
            // 
            // HistoryExecutablePath
            // 
            this.HistoryExecutablePath.Text = "実行ファイルパス";
            this.HistoryExecutablePath.Width = 180;
            // 
            // historyLabel
            // 
            this.historyLabel.AutoSize = true;
            this.historyLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.historyLabel.Location = new System.Drawing.Point(0, 0);
            this.historyLabel.Name = "historyLabel";
            this.historyLabel.Size = new System.Drawing.Size(53, 12);
            this.historyLabel.TabIndex = 0;
            this.historyLabel.Text = "履歴一覧";
            // 
            // processPanel
            // 
            this.processPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.processPanel.Controls.Add(this.processListView);
            this.processPanel.Controls.Add(this.processListLabel);
            this.processPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.processPanel.Location = new System.Drawing.Point(3, 3);
            this.processPanel.Name = "processPanel";
            this.processPanel.Size = new System.Drawing.Size(786, 115);
            this.processPanel.TabIndex = 2;
            // 
            // processListView
            // 
            this.processListView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.processListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.windowName,
            this.processStartDate,
            this.runTime,
            this.processId,
            this.executablePath});
            this.processListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processListView.FullRowSelect = true;
            this.processListView.GridLines = true;
            this.processListView.HideSelection = false;
            this.processListView.Location = new System.Drawing.Point(0, 12);
            this.processListView.Name = "processListView";
            this.processListView.Size = new System.Drawing.Size(786, 103);
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
            // processListLabel
            // 
            this.processListLabel.AutoSize = true;
            this.processListLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.processListLabel.Location = new System.Drawing.Point(0, 0);
            this.processListLabel.Name = "processListLabel";
            this.processListLabel.Size = new System.Drawing.Size(66, 12);
            this.processListLabel.TabIndex = 1;
            this.processListLabel.Text = "プロセス一覧";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "集計";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.summaryListView);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 326);
            this.panel2.TabIndex = 1;
            // 
            // summaryListView
            // 
            this.summaryListView.BackColor = System.Drawing.Color.LightSeaGreen;
            this.summaryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3,
            this.columnHeader2});
            this.summaryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryListView.FullRowSelect = true;
            this.summaryListView.GridLines = true;
            this.summaryListView.HideSelection = false;
            this.summaryListView.Location = new System.Drawing.Point(0, 0);
            this.summaryListView.Name = "summaryListView";
            this.summaryListView.Size = new System.Drawing.Size(786, 326);
            this.summaryListView.TabIndex = 1;
            this.summaryListView.UseCompatibleStateImageBehavior = false;
            this.summaryListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ウィンドウ名";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "実行時間";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "最終監視日";
            this.columnHeader2.Width = 100;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Aquamarine;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 46);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(3, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "実行時間集計";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.whiteListPanel);
            this.tabPage3.Controls.Add(this.blackListPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(792, 378);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "対象";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // whiteListPanel
            // 
            this.whiteListPanel.Controls.Add(this.label2);
            this.whiteListPanel.Controls.Add(this.addWhiteDirectory);
            this.whiteListPanel.Controls.Add(this.addWhiteDirectoryButton);
            this.whiteListPanel.Controls.Add(this.label1);
            this.whiteListPanel.Controls.Add(this.whiteListGridView);
            this.whiteListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whiteListPanel.Location = new System.Drawing.Point(0, 0);
            this.whiteListPanel.Name = "whiteListPanel";
            this.whiteListPanel.Size = new System.Drawing.Size(401, 378);
            this.whiteListPanel.TabIndex = 6;
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
            // addWhiteDirectory
            // 
            this.addWhiteDirectory.Location = new System.Drawing.Point(10, 25);
            this.addWhiteDirectory.Name = "addWhiteDirectory";
            this.addWhiteDirectory.Size = new System.Drawing.Size(202, 19);
            this.addWhiteDirectory.TabIndex = 1;
            // 
            // addWhiteDirectoryButton
            // 
            this.addWhiteDirectoryButton.Location = new System.Drawing.Point(218, 23);
            this.addWhiteDirectoryButton.Name = "addWhiteDirectoryButton";
            this.addWhiteDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.addWhiteDirectoryButton.TabIndex = 2;
            this.addWhiteDirectoryButton.Text = "追加";
            this.addWhiteDirectoryButton.UseVisualStyleBackColor = true;
            this.addWhiteDirectoryButton.Click += new System.EventHandler(this.AddWhiteDirectoryButton_Click);
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
            // whiteListGridView
            // 
            this.whiteListGridView.AllowUserToAddRows = false;
            this.whiteListGridView.AllowUserToDeleteRows = false;
            this.whiteListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.whiteListGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.whiteListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.whiteListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.directoryPath,
            this.deleteWhiteListButton});
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
            // deleteWhiteListButton
            // 
            this.deleteWhiteListButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.deleteWhiteListButton.Frozen = true;
            this.deleteWhiteListButton.HeaderText = "削除";
            this.deleteWhiteListButton.Name = "deleteWhiteListButton";
            this.deleteWhiteListButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.deleteWhiteListButton.Text = "削除";
            this.deleteWhiteListButton.UseColumnTextForButtonValue = true;
            this.deleteWhiteListButton.Width = 50;
            // 
            // blackListPanel
            // 
            this.blackListPanel.Controls.Add(this.blackListGridView);
            this.blackListPanel.Controls.Add(this.label4);
            this.blackListPanel.Controls.Add(this.addBlackDirectoryButton);
            this.blackListPanel.Controls.Add(this.addBlackDirectory);
            this.blackListPanel.Controls.Add(this.label3);
            this.blackListPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.blackListPanel.Location = new System.Drawing.Point(401, 0);
            this.blackListPanel.Name = "blackListPanel";
            this.blackListPanel.Size = new System.Drawing.Size(391, 378);
            this.blackListPanel.TabIndex = 5;
            // 
            // blackListGridView
            // 
            this.blackListGridView.AllowUserToAddRows = false;
            this.blackListGridView.AllowUserToDeleteRows = false;
            this.blackListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.blackListGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.blackListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blackListGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.blackDirectoryPath,
            this.deleteBlackListButton});
            this.blackListGridView.Location = new System.Drawing.Point(11, 62);
            this.blackListGridView.Name = "blackListGridView";
            this.blackListGridView.RowTemplate.Height = 21;
            this.blackListGridView.Size = new System.Drawing.Size(283, 313);
            this.blackListGridView.TabIndex = 5;
            this.blackListGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BlackListGridView_CellContentClick);
            // 
            // blackDirectoryPath
            // 
            this.blackDirectoryPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.blackDirectoryPath.Frozen = true;
            this.blackDirectoryPath.HeaderText = "フォルダパス";
            this.blackDirectoryPath.Name = "blackDirectoryPath";
            this.blackDirectoryPath.ReadOnly = true;
            this.blackDirectoryPath.Width = 84;
            // 
            // deleteBlackListButton
            // 
            this.deleteBlackListButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.deleteBlackListButton.Frozen = true;
            this.deleteBlackListButton.HeaderText = "削除";
            this.deleteBlackListButton.Name = "deleteBlackListButton";
            this.deleteBlackListButton.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.deleteBlackListButton.Text = "削除";
            this.deleteBlackListButton.UseColumnTextForButtonValue = true;
            this.deleteBlackListButton.Width = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "ブラックリスト";
            // 
            // addBlackDirectoryButton
            // 
            this.addBlackDirectoryButton.Location = new System.Drawing.Point(217, 26);
            this.addBlackDirectoryButton.Name = "addBlackDirectoryButton";
            this.addBlackDirectoryButton.Size = new System.Drawing.Size(75, 23);
            this.addBlackDirectoryButton.TabIndex = 4;
            this.addBlackDirectoryButton.Text = "追加";
            this.addBlackDirectoryButton.UseVisualStyleBackColor = true;
            this.addBlackDirectoryButton.Click += new System.EventHandler(this.AddBlackDirectoryButton_Click);
            // 
            // addBlackDirectory
            // 
            this.addBlackDirectory.Location = new System.Drawing.Point(9, 26);
            this.addBlackDirectory.Name = "addBlackDirectory";
            this.addBlackDirectory.Size = new System.Drawing.Size(202, 19);
            this.addBlackDirectory.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "ブラックリスト追加";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.OnTimerTick);
            // 
            // ToolStripMenuItemOpenDirectory
            // 
            this.ToolStripMenuItemOpenDirectory.Name = "ToolStripMenuItemOpenDirectory";
            this.ToolStripMenuItemOpenDirectory.Size = new System.Drawing.Size(180, 22);
            this.ToolStripMenuItemOpenDirectory.Text = "保存フォルダを開く(&O)";
            this.ToolStripMenuItemOpenDirectory.Click += new System.EventHandler(this.ToolStripMenuItemOpenDirectory_Click);
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
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.historyPanel.ResumeLayout(false);
            this.historyPanel.PerformLayout();
            this.processPanel.ResumeLayout(false);
            this.processPanel.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.whiteListPanel.ResumeLayout(false);
            this.whiteListPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.whiteListGridView)).EndInit();
            this.blackListPanel.ResumeLayout(false);
            this.blackListPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blackListGridView)).EndInit();
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
        private System.Windows.Forms.DataGridViewButtonColumn deleteWhiteListButton;
        private System.Windows.Forms.Panel whiteListPanel;
        private System.Windows.Forms.Panel blackListPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView blackListGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn blackDirectoryPath;
        private System.Windows.Forms.DataGridViewButtonColumn deleteBlackListButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button addBlackDirectoryButton;
        private System.Windows.Forms.TextBox addBlackDirectory;
        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.Label historyLabel;
        private System.Windows.Forms.Panel processPanel;
        private System.Windows.Forms.ListView HistoryListView;
        private System.Windows.Forms.ColumnHeader historyWindowName;
        private System.Windows.Forms.ColumnHeader historyProcessStartDate;
        private System.Windows.Forms.ColumnHeader historyRunTime;
        private System.Windows.Forms.ColumnHeader HistoryProcessId;
        private System.Windows.Forms.ColumnHeader HistoryExecutablePath;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView summaryListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenDirectory;
    }
}

