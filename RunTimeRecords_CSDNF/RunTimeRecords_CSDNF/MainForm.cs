using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private DataTable processDataTable;
        List<ProcessDto> processList;

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;
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
            processDataTable = ProcessesDao.GetProcessList(processDataTable);
            // プロセスリストの初期化
            SetProcessList();
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
        private void SetProcessList()
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
        /// タイマーイベントの関数の定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimerTick(object sender, EventArgs e)
        {
            processDataTable = ProcessesDao.GetProcessList(processDataTable); // 事前に値を取得
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
            SetProcessList(); // 取得した値で差し替え
            ProcessesDao.SaveProcesses(processList); // 自動保存
        }
    }
}
