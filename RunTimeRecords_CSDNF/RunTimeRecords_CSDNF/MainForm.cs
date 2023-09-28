using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management; //あらかじめ「System.Management」を参照に追加しておくこと
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private readonly ManagementClass _managementClass;
        private DataTable processDataTable;

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;
            // プロセスを取得する際に利用するインスタンスを取得しておく。
            // https://learn.microsoft.com/ja-jp/windows/win32/cimwin32prov/win32-process
            _managementClass = new ManagementClass("Win32_Process");
            // データテーブルの初期化
            processDataTable = InitializeProcessDataTable();
            // プロセスデータテーブルの初期化
            processDataTable = GetProcessList(processDataTable, _managementClass);
            // プロセスリストの初期化
            SetProcessList();
        }

        private static DataTable InitializeProcessDataTable()
        {
            // TODO : ★dataTable一式は１つのクラスにしたい（DTO？）

            DataTable processDataTable = new DataTable();
            // WindowTitle 項目の設定(PK)
            DataColumn column = processDataTable.Columns.Add("WindowTitle", typeof(string));
            var keyList = new DataColumn[] { column };
            processDataTable.PrimaryKey = keyList;
            // ProcessStartTime 項目の設定
            processDataTable.Columns.Add("ProcessStartTime", typeof(DateTime));
            // RunTime 項目の設定
            processDataTable.Columns.Add("RunTime", typeof(TimeSpan));
            // TotalRunTime 項目の設定
            processDataTable.Columns.Add("TotalRunTime", typeof(TimeSpan));
            // ProcessId 項目の設定
            processDataTable.Columns.Add("ProcessId", typeof(int));
            // ExecutablePath 項目の設定
            processDataTable.Columns.Add("ExecutablePath", typeof(string));

            return processDataTable;
        }

        /// <summary>
        /// 実行中のプロセスリストを取得
        /// </summary>
        private static DataTable GetProcessList(DataTable processDataTable, ManagementClass managementClass)
        {
            // TODO : ★このクラスはDAOにすべき？

            var nowTime = DateTime.Now;

            // プロセス一覧を取得してid,実行パスの辞書を作成
            Dictionary<int, string> processPathDictionary = new Dictionary<int, string>();
            using( ManagementObjectCollection list = managementClass.GetInstances()) // このインスタンスは都度再生成が必要
            {
                foreach (ManagementBaseObject process in list)
                {
                    // 実行パスが無いものは不要
                    if (process["ExecutablePath"] == null)
                    {
                        continue;
                    }
                    string executablePath = process["ExecutablePath"].ToString();
                    // TODO : ★とりま「ExecutablePath」が「D:」始まりでないものはスキップ
                    if (!executablePath.StartsWith("D:"))
                    {
                        continue;
                    }
                    int processId = int.Parse(process["ProcessId"].ToString());
                    // 辞書に追加
                    processPathDictionary.Add(processId, executablePath);
                }
            }

            // プロセス一覧を取得してデータテーブルを更新
            foreach ( var process in Process.GetProcesses())
            {
                // ウィンドウタイトルが設定されているもののみが対象
                string windowTitle = process.MainWindowTitle;
                if (String.IsNullOrEmpty(windowTitle))
                {
                    continue;
                }
                // 実行パス一覧に存在するものが対象
                int processId = process.Id;
                if (!processPathDictionary.TryGetValue(processId, out string executablePath))
                {
                    continue; // スキップ
                }
                try
                {
                    string key = windowTitle;
                    // StartTime
                    DateTime startTime = process.StartTime;
                    // RunTime
                    TimeSpan runTime = nowTime - startTime;
                    // 行の有無で分岐
                    DataRow existRow = processDataTable.Rows.Find(key);
                    if (existRow != null)
                    {
                        // 既存データは実行時間のみ更新
                        existRow["RunTime"] = runTime;
                        existRow["TotalRunTime"] = runTime;// todo : 合計時間に変更すること
                    }
                    else
                    {
                        // 新規データ
                        DataRow newRow = processDataTable.NewRow();
                        newRow["WindowTitle"] = windowTitle;
                        newRow["ProcessStartTime"] = startTime;
                        newRow["RunTime"] = runTime;
                        newRow["TotalRunTime"] = runTime; // todo : 合計時間に変更すること
                        newRow["ProcessId"] = processId;
                        newRow["ExecutablePath"] = executablePath;
                        processDataTable.Rows.Add(newRow);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
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
                    TimeFormatString((TimeSpan)row["RunTime"]),
                    TimeFormatString((TimeSpan)row["TotalRunTime"]),// 合計実行時間
                    ((int)row["ProcessId"]).ToString(),
                    (string)row["ExecutablePath"]
                };
                processListView.Items.Add(new ListViewItem(item));
            }
        }

        private static string TimeFormatString(TimeSpan timeSpan)
        {
            // TODO : ★このメソッドはutilitiesにいれるべき？
            return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        }

        /// <summary>
        /// タイマーイベントの関数の定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimerTick(object sender, EventArgs e)
        {
            processDataTable = GetProcessList(processDataTable, _managementClass); // 事前に値を取得
            SetProcessList(); // 取得した値で差し替え
            SaveProcesses(processDataTable); // 自動保存
        }

        private static void SaveProcesses(DataTable processes)
        {
            // TODO : ★このメソッドはDAOとすべき？

            // 保存フォルダはexeがあるフォルダの「save」フォルダ固定
            string folderPath = @".\save";
            CreateDirectory(folderPath);
            // プロセスデータをcsvに保存
            string fileName = "processes.csv";
            WriteCsv(processes, folderPath + @"\" + fileName);

        }

        /// <summary>
        /// 指定したパスのフォルダが存在していなければ作成する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static DirectoryInfo CreateDirectory(string path)
        {
            // TODO : ★このメソッドはutilitiesにいれるべき？
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }

        private static void WriteCsv(DataTable processes, string filePath)
        {
            // TODO : ★このメソッドはDAOとすべき？(SaveProcessesとあわせて)

            using (var streamWriter = new StreamWriter(filePath))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                // TODO : ★専用のDTOを作成すると1行で記載可能か？
                // csv.WriteRecord(processes); 
                // 1行ずつ書き込み
                foreach (DataRow row in processes.Rows)
                {
                    // 項目毎に書き込み
                    foreach (DataColumn column in processes.Columns)
                    {
                        csvWriter.WriteField(row[column]); 
                    }
                    csvWriter.NextRecord(); //改行
                }
            }
        }
    }
}
