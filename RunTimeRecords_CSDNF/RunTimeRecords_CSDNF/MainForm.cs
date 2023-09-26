using System;
using System.Data;
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
            InitializeProcessDataTable();            
            // プロセスデータテーブルの初期化
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
            // CreationDate 項目の設定
            column = new DataColumn
            {
                ColumnName = "CreationDate",
                DataType = typeof(DateTime)
            };
            processDataTable.Columns.Add(column);
            // RunTime 項目の設定
            column = new DataColumn
            {
                ColumnName = "RunTime",
                DataType = typeof(TimeSpan)
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
            ManagementObjectCollection list = _managementClass.GetInstances(); // このインスタンスは都度再生成が必要
            foreach (ManagementBaseObject process in list)
            {
                try
                {
                    string name = process["Name"].ToString();
                    string id = process["ProcessId"].ToString();
                    string key = id + name;
                    string path = "";
                    if (process["ExecutablePath"] != null)
                    {
                        path = process["ExecutablePath"].ToString();
                    }
                    // TODO : ★とりま「ExecutablePath」が「D:」始まりでないものはスキップ
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
                    // CreationDate
                    string creationDateString = process["CreationDate"].ToString();
                    int year = int.Parse(creationDateString.Substring(0, 4));
                    int month = int.Parse(creationDateString.Substring(4, 2));
                    int day = int.Parse(creationDateString.Substring(6, 2));
                    int hour = int.Parse(creationDateString.Substring(8, 2));
                    int minute = int.Parse(creationDateString.Substring(10, 2));
                    int second = int.Parse(creationDateString.Substring(12, 2));
                    var creationDate = new DateTime(year, month, day, hour, minute, second);
                    // RunTime
                    TimeSpan runTime = nowTime - creationDate;

                    // データテーブルへ反映
                    DataRow existRow = processDataTable.Rows.Find(key);
                    if( existRow != null )
                    {
                        // 既存データは実行時間のみ更新                        
                        existRow["RunTime"] = runTime;
                    }
                    else
                    {
                        // 新規データ
                        DataRow newRow = processDataTable.NewRow();
                        newRow["Key"] = key;
                        newRow["Name"] = name;
                        newRow["Id"] = id;
                        newRow["ExecutablePath"] = path;
                        newRow["Description"] = description;
                        newRow["CreationDate"] = creationDate;
                        newRow["RunTime"] = runTime;
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
                    ((DateTime)row["CreationDate"]).ToString(),
                    TimeFormatString((TimeSpan)row["RunTime"])
                };
                processListView.Items.Add(new ListViewItem(item));
            }
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
            SetProcessList(); // 取得した値で差し替え
        }

    }
}
