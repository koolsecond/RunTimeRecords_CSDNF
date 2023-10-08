using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private DataTable processDataTable;
        private readonly List<ProcessDto> processList;
        private readonly List<string> whiteList;
        private readonly List<string> blackList;

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;
            
            // TODO : ホワイトリストとブラックリストは MasterListを親として新規作成すれば共通化できそう

            // ホワイトリストの読込と設定
            whiteList = WhiteListDao.LoadWhiteList();
            SetWhiteListView();

            // ブラックリストの読込と設定
            blackList = BlackListDao.LoadList();
            SetBlackListView();

            // データテーブルの初期化（前回保存内容の読込）
            processDataTable = InitializeProcessDataTable();
            processList = ProcessesDao.LoadProcesses();
            foreach (var record in processList)
            {
                // todo:　List<ProcessDto> -> DataTableの型変換は無くしたい
                // 新規データ
                DataRow newRow = processDataTable.NewRow();
                newRow["WindowTitle"] = Utilities.DoubleQuateDelete(record.WindowTitle);
                newRow["ProcessStartTime"] = record.ProcessStartTime;
                newRow["RunTime"] = record.RunTime;
                newRow["ProcessId"] = record.ProcessId;
                newRow["ExecutablePath"] = Utilities.DoubleQuateDelete(record.ExecutablePath);
                processDataTable.Rows.Add(newRow);
            }
            // 実行中のプロセスのデータを追加
            processDataTable = ProcessesDao.GetProcessList(processDataTable, whiteList, blackList);
            // プロセスリストの初期化
            SetProcessListView();
        }

        private static DataTable InitializeProcessDataTable()
        {
            // TODO : ★dataTable一式は１つのクラスにしたい（DTO？）->もういらない？

            DataTable processDataTable = new DataTable();
            // WindowTitle 項目の設定(PK)
            DataColumn keyColumn1 = processDataTable.Columns.Add("WindowTitle", typeof(string));
            // ProcessStartTime 項目の設定
            DataColumn keyColumn2 = processDataTable.Columns.Add("ProcessStartTime", typeof(DateTime));
            // RunTime 項目の設定
            processDataTable.Columns.Add("RunTime", typeof(TimeSpan));
            // ProcessId 項目の設定
            processDataTable.Columns.Add("ProcessId", typeof(int));
            // ExecutablePath 項目の設定
            processDataTable.Columns.Add("ExecutablePath", typeof(string));
            // データテーブルのキー項目の設定
            var keyList = new DataColumn[] { keyColumn1 , keyColumn2 };
            processDataTable.PrimaryKey = keyList;
            return processDataTable;
        }

        /// <summary>
        /// リストビューに実行中のプロセスリストを設定
        /// </summary>
        private void SetProcessListView()
        {
            // リストビューを再設定
            processListView.Items.Clear();
            foreach (DataRow row in processDataTable.Rows)
            {
                string[] item = {
                    (string)row["WindowTitle"],
                    ((DateTime)row["ProcessStartTime"]).ToString(),
                    Utilities.TimeFormatString((TimeSpan)row["RunTime"]),
                    ((int)row["ProcessId"]).ToString(),
                    (string)row["ExecutablePath"]
                };
                processListView.Items.Add(new ListViewItem(item));
            }
        }

        /// <summary>
        /// ホワイトリスト表示処理
        /// </summary>
        private void SetWhiteListView()
        {
            whiteListGridView.Rows.Clear();
            foreach( string path in whiteList)
            {
                whiteListGridView.Rows.Add(path);
            }
        }

        /// <summary>
        /// ブラックリスト表示処理
        /// </summary>
        private void SetBlackListView()
        {
            blackListGridView.Rows.Clear();
            foreach( string path in blackList)
            {
                blackListGridView.Rows.Add(path);
            }
        }

        /// <summary>
        /// タイマーイベントの関数の定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimerTick(object sender, EventArgs e)
        {
            processDataTable = ProcessesDao.GetProcessList(processDataTable, whiteList, blackList); // 事前に値を取得
            List<ProcessDto> processList = new List<ProcessDto>();
            foreach (DataRow row in processDataTable.Rows)
            {
                var processDto = new ProcessDto
                {
                    WindowTitle = (string)row["WindowTitle"],
                    ProcessStartTime = (DateTime)row["ProcessStartTime"],
                    RunTime = (TimeSpan)row["RunTime"],
                    ProcessId = ((int)row["ProcessId"]),
                    ExecutablePath = (string)row["ExecutablePath"]
                };
                processList.Add(processDto);
            }
            SetProcessListView(); // 取得した値で差し替え
            ProcessesDao.SaveProcesses(processList); // 自動保存
        }

        /// <summary>
        /// ホワイトボックス追加ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWhiteDirectoryButton_Click(object sender, EventArgs e)
        {
            string path = addWhiteDirectory.Text;
            whiteList.Add(path);
            WhiteListDao.SaveWhiteList(whiteList); // TODO: ADDとセットで記載がよくない。 Dto化？
            SetWhiteListView();
        }

        /// <summary>
        /// ホワイトリスト内のイベント
        /// ・削除ボタンが押下された場合、該当データを削除する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhiteListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 削除ボタンが押下された場合
            if (whiteListGridView.Columns[e.ColumnIndex].Name == "deleteWhiteListButton")
            {
                // 選択行の解析
                DataGridViewRow row = whiteListGridView.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[0];
                string path = cell.Value.ToString();
                // ダイアログの表示
                string deleteCaption = "ホワイトリスト削除確認";
                string deleteMessage = $"フォルダ「{path}」をホワイトリストから削除して宜しいですか？";
                DialogResult result = MessageBox.Show(deleteMessage, deleteCaption, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // リストの削除・保存・表示
                    whiteList.Remove(path);
                    WhiteListDao.SaveWhiteList(whiteList); // TODO: Removeとセットで記載がよくない。 Dto化？
                    SetWhiteListView();
                }
            }
        }

        /// <summary>
        /// タブ切り替えイベント
        /// ・監視タブ以外ではタイマーを無効にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 監視タブ以外ではタイマーを無効にする
            if(tabControl1.SelectedTab.Name == "tabPage1")
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        /// <summary>
        /// ブラックボックス追加ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBlackDirectoryButton_Click(object sender, EventArgs e)
        {
            string path = addBlackDirectory.Text;
            blackList.Add(path);
            BlackListDao.SaveList(blackList); // TODO: ADDとセットで記載がよくない。 Dto化？
            SetBlackListView();
        }

        private void BlackListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 削除ボタンが押下された場合
            if (blackListGridView.Columns[e.ColumnIndex].Name == "deleteBlackListButton")
            {
                // 選択行の解析
                DataGridViewRow row = blackListGridView.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[0];
                string path = cell.Value.ToString();
                // ダイアログの表示
                string deleteCaption = "ブラックリスト削除確認";
                string deleteMessage = $"フォルダ「{path}」をブラックリストから削除して宜しいですか？";
                DialogResult result = MessageBox.Show(deleteMessage, deleteCaption, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // リストの削除・保存・表示
                    blackList.Remove(path);
                    BlackListDao.SaveList(blackList); // TODO: Removeとセットで記載がよくない。 Dto化？
                    SetBlackListView();
                }
            }
        }
    }
}
