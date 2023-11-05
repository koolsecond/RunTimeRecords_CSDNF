using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
        private static readonly LoggerManager loggerManager = new LoggerManager();
        private readonly List<ProcessDto> processList;
        private readonly List<ProcessDto> processHistory;
        private readonly ProcessesDao processesDao = new ProcessesDao();
        private List<ProcessSummaryDto> processSummaryList;
        private readonly ProcessSummaryDao processSummaryDao = new ProcessSummaryDao();
        private readonly ListFileDto whiteList;
        private readonly ListFileDto blackList;
        private readonly ListFileDao listFileDao = new ListFileDao();

        public MainForm()
        {
            InitializeComponent();
            // TODO : ★未実装機能は非表示
            statusStrip.Visible = false;
            toolStripStatusLabel1.Text = string.Empty;

            // ホワイトリストの読込と設定
            whiteList = listFileDao.LoadListFile(Settings.Instance.WhiteListFilePath);
            SetWhiteListView();

            // ブラックリストの読込と設定
            blackList = listFileDao.LoadListFile(Settings.Instance.BlackListFilePath);
            SetBlackListView();

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
            timer1.Enabled = tabControl1.SelectedTab.Name == "tabPage1";
            // 集計タブ処理
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                Summary();
            }
            // ファイル⇒保存が可能なタブは「監視」「集計」のみ
            ToolStripMenuItemSave.Enabled = tabControl1.SelectedTab.Name == "tabPage1" || tabControl1.SelectedTab.Name == "tabPage2";
            // フォルダを開くことが可能なタブは「監視」「対象」のみ
            ToolStripMenuItemOpenDirectory.Enabled = tabControl1.SelectedTab.Name == "tabPage1" || tabControl1.SelectedTab.Name == "tabPage3";
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
            processSummaryList = processSummaryDao.Summary(data);

            // 画面設定
            summaryListView.Items.Clear();
            foreach (ProcessSummaryDto processSummary in processSummaryList)
            {
                string[] item =
                {
                    processSummary.WindowTitle,
                    Utilities.TimeFormatString(processSummary.TotalRunTime),
                    processSummary.LastDate.Date.ToShortDateString(),
                };
                summaryListView.Items.Add(new ListViewItem(item));
            }
        }
        /// <summary>
        /// ファイル⇒保存を選択した時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
        {
            // 初期ファイル名の指定
            string fileName = "";
            if (tabControl1.SelectedTab.Name == "tabPage2")
            {
                // 集計タブ
                fileName = "summary.csv";
            }
            else if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // 監視タブ
                fileName = "history.csv";
            }

            // 保存ダイアログを表示する
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = fileName, // 初期ファイル名
                Filter = "CSVファイル(*.csv;*.CSV)|*.csv;*.CSV|すべてのファイル(*.*)|*.*", // ファイルの種類
                FilterIndex = 1, // 1つ目を指定
                Title = "保存先のファイルを選択して下さい",
                RestoreDirectory = true // ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            };

            // ダイアログ表示
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool saveResultFlag = false;
                // 選択されたファイルに出力する
                if (tabControl1.SelectedTab.Name == "tabPage2")
                {
                    // 集計タブが開かれている場合は集計データを保存する
                    saveResultFlag = processSummaryDao.SaveSummaryList(processSummaryList, saveFileDialog.FileName);
                }
                else if (tabControl1.SelectedTab.Name == "tabPage1")
                {
                    // 監視タブが開かれている場合は監視データと履歴データを保存する
                    // 直近のデータと履歴データを合体
                    List<ProcessDto> data = new List<ProcessDto>();
                    data.AddRange(processList);
                    data.AddRange(processHistory);
                    // 保存処理の実行
                    saveResultFlag = processesDao.SaveProcesses(data, saveFileDialog.FileName);
                }
                // 保存処理完了後処理
                if (saveResultFlag)
                {
                    // ダイアログメッセージを表示してフォルダを開くか確認する。
                    string path = Path.GetDirectoryName(saveFileDialog.FileName);
                    string caption = "フォルダ確認";
                    string message = "ファイルを保存しました。" + Environment.NewLine
                        + $"フォルダ「{path}」を開きますか？";
                    DialogResult openDialogResultFlag = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
                    if (openDialogResultFlag == DialogResult.Yes)
                    {
                        // 関連付けでフォルダを開く
                        try
                        {
                            System.Diagnostics.Process.Start(path);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            loggerManager.LogError($"フォルダを開く動作のエラー,{path}", ex);
                        }
                    }
                }
                else
                {
                    // 保存に失敗した場合
                    // ダイアログメッセージを表示
                    string path = Path.GetDirectoryName(saveFileDialog.FileName);
                    loggerManager.LogError($"ファイル保存失敗,{path}");
                    string caption = "ファイル保存失敗";
                    string message = "ファイルの保存に失敗しました。";
                    _ = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 「保存フォルダを開く」を選択した時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemOpenDirectory_Click(object sender, EventArgs e)
        {
            // 対象フォルダ指定
            string dirctoryPath = "";
            if (tabControl1.SelectedTab.Name == "tabPage1")
            {
                // 監視タブの場合
                dirctoryPath = Settings.Instance.SaveFolderPath;
            }
            else if (tabControl1.SelectedTab.Name == "tabPage3")
            {
                // 対象タブの場合
                dirctoryPath = Settings.Instance.MasterFolderPath;
            }
            // 関連付けでフォルダを開く
            try
            {
                System.Diagnostics.Process.Start(dirctoryPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"フォルダを開く動作のエラー,{dirctoryPath}", ex);
            }
        }
    }
}
