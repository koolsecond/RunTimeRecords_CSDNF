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
        List<ProcessDto> processList;
        List<string> whiteList;

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;
            
            // ホワイトリストの読込と設定
            whiteList = WhiteListDao.LoadWhiteList();
            SetWhiteListView();

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
            processDataTable = ProcessesDao.GetProcessList(processDataTable, whiteList);
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

        private void SetWhiteListView()
        {
            whiteListGridView.Rows.Clear();
            foreach( string path in whiteList)
            {
                whiteListGridView.Rows.Add(path);
            }
        }

        /// <summary>
        /// タイマーイベントの関数の定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimerTick(object sender, EventArgs e)
        {
            processDataTable = ProcessesDao.GetProcessList(processDataTable, whiteList); // 事前に値を取得
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

        private void WhiteDirectoryButton_Click(object sender, EventArgs e)
        {
            string path = addWhiteDirectory.Text;
            whiteList.Add(path);
            WhiteListDao.SaveWhiteList(whiteList);
            SetWhiteListView();
        }

        private void WhiteListGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 削除ボタンが押下された場合
            if (whiteListGridView.Columns[e.ColumnIndex].Name == "deleteButton")
            {
                // 選択行の解析
                DataGridViewRow row = whiteListGridView.Rows[e.RowIndex];
                DataGridViewCell cell = row.Cells[0];
                string path = cell.Value.ToString();
                // ダイアログの表示
                string deleteCaption = "対象リスト削除確認";
                string deleteMessage = $"フォルダ「{path}」を対象から削除して宜しいですか？";
                DialogResult result = MessageBox.Show(deleteMessage, deleteCaption, MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // リストの削除・保存・表示
                    whiteList.Remove(path);
                    WhiteListDao.SaveWhiteList(whiteList);
                    SetWhiteListView();
                }
            }
        }

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
    }
}
