using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    public partial class MainForm : Form
    {
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

            // プロセスリストの初期化（前回保存内容の読込）
            processList = ProcessesDao.LoadProcesses();
            // 実行中のプロセスのデータを追加
            ProcessesDao.GetProcessList(processList, whiteList, blackList);
            // プロセスリストの初期化
            SetProcessListView();
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
            // 実行中プロセスを取得
            ProcessesDao.GetProcessList(processList, whiteList, blackList);
            // 取得した値で差し替え
            SetProcessListView();
            // 自動保存
            ProcessesDao.SaveProcesses(processList);
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
