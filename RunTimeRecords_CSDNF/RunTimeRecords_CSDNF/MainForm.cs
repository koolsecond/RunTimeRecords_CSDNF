using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Management; //あらかじめ「System.Management」を参照に追加しておくこと
using System.Windows.Forms;
using System.Xml.Linq;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private readonly ManagementClass _managementClass;
        private DataTable processDataTable;

        public MainForm()
        {
            InitializeComponent();
            // https://learn.microsoft.com/ja-jp/windows/win32/cimwin32prov/win32-process
            _managementClass = new ManagementClass("Win32_Process");
            // データテーブルの初期化
            InitializeProcessDataTable();            
            // TODO : 未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;
            // test
            testLabel.Text = DateTime.Now.ToString();
            // プロセスリストの初期化
            GetProcessList();
            // プロセスリストの初期化
            SetProcessList();

        }

        private void InitializeProcessDataTable()
        {
            processDataTable = new DataTable();
            DataColumn column;
            // key 項目の設定
            column = new DataColumn
            {
                ColumnName = "Key",
                DataType = typeof(string),
                Unique = true
            };
            processDataTable.Columns.Add(column);
            var keyList = new DataColumn[] { column };
            processDataTable.PrimaryKey = keyList;
            // name 項目の設定
            column = new DataColumn
            {
                ColumnName = "Name",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // id 項目の設定
            column = new DataColumn
            {
                ColumnName = "Id",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // ExecutablePath 項目の設定
            column = new DataColumn
            {
                ColumnName = "ExecutablePath",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // Description 項目の設定
            column = new DataColumn
            {
                ColumnName = "Description",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // UserModeTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "UserModeTime",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // KernelModeTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "KernelModeTime",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // CpuTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "CpuTime",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
            // StartTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "StartTime",
                DataType = typeof(DateTime)
            };
            processDataTable.Columns.Add(column);
            // RunTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "RunTime",
                DataType = typeof(string)
            };
            processDataTable.Columns.Add(column);
        }

        /// <summary>
        /// 実行中のプロセスリストを取得
        /// </summary>
        private void GetProcessList()
        {
            var nowTime = DateTime.Now;
            // プロセス一覧を取得して設定
            var list = _managementClass.GetInstances();
            foreach (var process in list)
            {
                try
                {
                    var name = process["Name"].ToString();
                    var id = process["ProcessId"].ToString();
                    string key = id + name;
                    string path = "";
                    if (process["ExecutablePath"] != null)
                    {
                        path = process["ExecutablePath"].ToString();
                    }
                    // ★とりま「D:」始まりでないものはスキップ
                    if (!path.StartsWith("D:"))
                    {
                        continue;
                    }
                    // description
                    string description = "";
                    if (process["Description"] != null)
                    {
                        description = process["Description"].ToString();
                    }
                    // UserModeTime
                    // ユーザー モードの時間 (100 ナノ秒単位)。
                    // この情報が使用できない場合は、0 (ゼロ) の値を使用します。
                    string userModeTime = "";
                    if (process["UserModeTime"] != null)
                    {
                        userModeTime = process["UserModeTime"].ToString();                        
                    }
                    // KernelModeTime
                    // カーネル モードの時間 (100 ナノ秒単位)。
                    // この情報が使用できない場合は、0 (ゼロ) の値を使用します。
                    string kernelModeTime = "";
                    if (process["KernelModeTime"] != null)
                    {
                        kernelModeTime = process["KernelModeTime"].ToString();
                    }
                    // CPU Time = KernelModeTime + UserModeTime
                    string cpuTime = "";
                    if (UInt64.TryParse(userModeTime, out ulong uTime) && UInt64.TryParse(kernelModeTime, out ulong kTime))
                    {
                        cpuTime = (uTime + kTime).ToString();
                    }
                    // 時刻表示切替
                    userModeTime = TimeFormatString(StringToTimeSpan(userModeTime));
                    kernelModeTime = TimeFormatString(StringToTimeSpan(kernelModeTime));
                    cpuTime = TimeFormatString(StringToTimeSpan(cpuTime));
                    // データテーブルへ反映
                    var newRow = processDataTable.NewRow();
                    newRow["Key"] = key;
                    newRow["Name"] = name;
                    newRow["Id"] = id;
                    newRow["ExecutablePath"] = path;
                    newRow["Description"] = description;
                    newRow["UserModeTime"] = userModeTime;
                    newRow["kernelModeTime"] = kernelModeTime;
                    newRow["cpuTime"] = cpuTime;
                    var existRow = processDataTable.Rows.Find(key);
                    if( existRow != null )
                    {
                        // 既存データ
                        existRow["UserModeTime"] = userModeTime;
                        existRow["kernelModeTime"] = kernelModeTime;
                        existRow["cpuTime"] = cpuTime;
                        DateTime startTime = (DateTime)existRow["StartTime"];
                        TimeSpan runTime = nowTime - startTime;
                        existRow["RunTime"] = TimeFormatString(runTime);
                    }
                    else
                    {
                        // 新規データ
                        newRow["StartTime"] = DateTime.Now;
                        newRow["RunTime"] = "00:00:00";
                        processDataTable.Rows.Add(newRow);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
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
                    (string)row["Name"],
                    (string)row["Id"],
                    (string)row["ExecutablePath"],
                    (string)row["Description"],
                    ((DateTime)row["StartTime"]).ToString(),
                    (string)row["RunTime"],
                    (string)row["UserModeTime"],
                    (string)row["kernelModeTime"],
                    (string)row["cpuTime"]
                };
                processListView.Items.Add(new ListViewItem(item));
            }
        }

        /// <summary>
        /// TimeSpanとして戻す
        /// </summary>
        /// <returns>TimeSpan</returns>
        private static TimeSpan StringToTimeSpan(string strTime)
        {
            if (UInt64.TryParse(strTime, out var time) )
            {
                // 1ナノ秒 = 10^-3マイクロ秒 = 10^-6ミリ秒 = 10^-9秒
                // 100ナノ秒 =  0.1マイクロ秒 * 1000 =  0.1ミリ秒 * 10 = 1ミリ秒
                var milliSseconds = (int)(time / (Math.Pow(10, 3))); // ミリ秒変換
                return TimeSpan.FromMilliseconds(milliSseconds);
            }
            return new TimeSpan();
        }


        private static string TimeFormatString(TimeSpan timeSpan)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        }

        /// <summary>
        /// タイマーイベントの関数の定義
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnTimerTick(object sender, EventArgs e)
        {
            GetProcessList(); // 事前に値を取得
            testLabel.Text = DateTime.Now.ToString();// test
            SetProcessList(); // 取得した値で差し替え
        }

    }
}
