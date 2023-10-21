using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private readonly List<ProcessDto> processList;
        private readonly List<ProcessDto> processHistory;
        private readonly ProcessesDao processesDao;
        private readonly ListFileDto whiteList;
        private readonly ListFileDto blackList;
        private readonly ListFileDao listFileDao = new ListFileDao();

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            menuStrip.Visible = false;
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;

            // ホワイトリストの読込と設定
            whiteList = listFileDao.LoadListFile(Settings.Instance.WhiteListFilePath);
            SetWhiteListView();

            // ブラックリストの読込と設定
            blackList = listFileDao.LoadListFile(Settings.Instance.BlackListFilePath);
            SetBlackListView();

            // プロセスリストDaoのインスタンス作成
            processesDao = new ProcessesDao();
            // 履歴ファイルの読込
            processHistory = processesDao.LoadProcesses(Settings.Instance.HistoryFilePath);
            // プロセスリストの初期化（前回保存内容の読込）
            processList = processesDao.LoadProcesses(Settings.Instance.SaveFilePath);
            // プロセスリスト内で当日以外のデータがあればヒストリーへ移動
            ProcessesDao.MoveProcessDataToHistory(processList, processHistory);
            // 履歴データを保存
            processesDao.SaveProcesses(processHistory, Settings.Instance.HistoryFilePath);
            // 実行中のプロセスのデータを追加
            ProcessesDao.GetProcessList(processList, whiteList.DataList, blackList.DataList);
            // リストの初期化
            SetProcessListView();
            SetHistoryListView();
        }

        /// <summary>
        /// プロセスリストに実行中のプロセスリストを設定
        /// </summary>
        private void SetProcessListView()
        {
            // リストビューを再設定
            processListView.Items.Clear();
            foreach (ProcessDto process in processList)
            {
                string[] item =
                {
                    process.WindowTitle,
                    process.ProcessStartTime.ToString(),
                    Utilities.TimeFormatString(process.RunTime),
                    process.ProcessId.ToString(),
                    process.ExecutablePath,
                };
                processListView.Items.Add(new ListViewItem(item));
            }
        }

        /// <summary>
        /// 履歴リストを設定
        /// </summary>
        private void SetHistoryListView()
        {
            // リストビューを再設定
            HistoryListView.Items.Clear();
            foreach (ProcessDto process in processHistory)
            {
                string[] item =
                {
                    process.WindowTitle,
                    process.ProcessStartTime.ToString(),
                    Utilities.TimeFormatString(process.RunTime),
                    process.ProcessId.ToString(),
                    process.ExecutablePath,
                };
                HistoryListView.Items.Add(new ListViewItem(item));
            }
        }

        /// <summary>
        /// ホワイトリスト表示処理
        /// </summary>
        private void SetWhiteListView()
        {
            whiteListGridView.Rows.Clear();
            foreach (string path in whiteList.DataList)
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
            foreach (string path in blackList.DataList)
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
            // 実行中プロセスを取得
            ProcessesDao.GetProcessList(processList, whiteList.DataList, blackList.DataList);
            // 取得した値で差し替え
            SetProcessListView();
            // 自動保存
            processesDao.SaveProcesses(processList, Settings.Instance.SaveFilePath);
        }

        /// <summary>
        /// ホワイトボックス追加ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWhiteDirectoryButton_Click(object sender, EventArgs e)
        {
            string path = addWhiteDirectory.Text;
            whiteList.Add(path, listFileDao);
            // TODO : リストの追加・保存の失敗時の動作
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
                    whiteList.Remove(path, listFileDao);
                    // TODO : リストの削除・保存の失敗時の動作
                    SetWhiteListView();
                }
            }
        }

        /// <summary>
        /// タブ切り替えイベント
        /// ・監視タブ以外ではタイマーを無効にする
        /// ・集計タブの場合、集計処理を実施
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 監視タブ以外ではタイマーを無効にする
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
            // 集計タブ処理
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                Summary();
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
            blackList.Add(path, listFileDao);
            // TODO : リストの追加・保存の失敗時の動作
            SetBlackListView();
        }

        /// <summary>
        /// ブラックリスト内のイベント
        /// ・削除ボタンが押下された場合、該当データを削除する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    blackList.Remove(path, listFileDao);
                    // TODO : リストの削除・保存の失敗時の動作
                    SetBlackListView();
                }
            }
        }

        /// <summary>
        /// 集計処理を実施し、画面に表示
        /// </summary>
        private void Summary()
        {
            // 直近のデータと履歴データを合体
            List<ProcessDto> data = new List<ProcessDto>();
            data.AddRange(processList);
            data.AddRange(processHistory);

            // 集計処理の実施
            var query = data
                .GroupBy(x => x.WindowTitle)
                .Select(x => new
                {
                    WindowTitle = x.Key
                    ,
                    TotalRunTime = new TimeSpan(x.Sum(y => y.RunTime.Ticks))
                    ,
                    LastDate = x.Max(y => y.ProcessStartTime)
                })
                .OrderByDescending(x => x.LastDate);

            // 画面設定
            summaryListView.Items.Clear();
            foreach (var group in query)
            {
                string[] item =
                {
                    group.WindowTitle,
                    Utilities.TimeFormatString(group.TotalRunTime),
                    group.LastDate.Date.ToShortDateString(),
                };
                summaryListView.Items.Add(new ListViewItem(item));
            }
        }
    }
}
