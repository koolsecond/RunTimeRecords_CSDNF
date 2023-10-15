using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;//あらかじめ「System.Management」を参照に追加しておくこと
using System.Text;

namespace RunTimeRecords_CSDNF
{
    public class ProcessesDao
    {
        private static readonly LoggerManager loggerManager = new LoggerManager();
        private static readonly CsvConfiguration CsvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false, // ヘッダー有無
            Delimiter = ",", // 区切り文字指定
            NewLine = Environment.NewLine, // 改行コード
            IgnoreBlankLines = true, // 空行の読み飛ばし
            Encoding = Encoding.UTF8, // 文字コード
            AllowComments = true, // コメント許可
            Comment = '#', // コメントの開始文字
            DetectColumnCountChanges = true, // CSVの列数が想定と異なっていた場合にエラーにするか
            TrimOptions = TrimOptions.None, // 各カラムをトリミングするか
        };
        // プロセスを取得する際に利用するインスタンスを取得しておく。
        // https://learn.microsoft.com/ja-jp/windows/win32/cimwin32prov/win32-process
        private static readonly ManagementClass _managementClass = new ManagementClass("Win32_Process");

        public ProcessesDao()
        {

        }

        /// <summary>
        /// 実行中のプロセスリストを取得
        /// </summary>
        public static List<ProcessDto> GetProcessList(List<ProcessDto> processList, List<string> whiteList, List<string> blackList)
        {
            var nowTime = DateTime.Now;

            // プロセス一覧を取得してid,実行パスの辞書を作成
            Dictionary<int, string> processPathDictionary = new Dictionary<int, string>();
            using (ManagementObjectCollection list = _managementClass.GetInstances()) // このインスタンスは都度再生成が必要
            {
                foreach (ManagementBaseObject process in list)
                {
                    try
                    {
                        // 実行パスが無いものはスキップ
                        if (process["ExecutablePath"] == null)
                        {
                            continue;
                        }
                        string executablePath = process["ExecutablePath"].ToString();
                        // ホワイトリストに無ければスキップ
                        if (!whiteList.Exists(x => executablePath.StartsWith(x)))
                        {
                            continue;
                        }
                        // ブラックリストにあればスキップ
                        if (blackList.Exists(x => executablePath.StartsWith(x)))
                        {
                            continue;
                        }
                        int processId = int.Parse(process["ProcessId"].ToString());
                        // 辞書に追加
                        processPathDictionary.Add(processId, executablePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        loggerManager.LogError("id,実行パスの辞書作成エラー", ex);
                    }
                }
            }

            // プロセス一覧を取得して監視リストを更新
            foreach (var process in Process.GetProcesses())
            {
                // ウィンドウタイトルが設定されていないものはスキップ
                string windowTitle = process.MainWindowTitle;
                if (String.IsNullOrEmpty(windowTitle))
                {
                    continue;
                }
                // 実行パス一覧に存在しないものはスキップ
                int processId = process.Id;
                if (!processPathDictionary.TryGetValue(processId, out string executablePath))
                {
                    continue;
                }
                try
                {
                    // 時間計算
                    DateTime startTime = process.StartTime;
                    TimeSpan runTime = nowTime - startTime;
                    // データ検索
                    ProcessDto existRow = null;
                    foreach (ProcessDto row in processList)
                    {
                        if (row.WindowTitle == windowTitle && row.ProcessStartTime.ToString().Equals(startTime.ToString()))
                        {
                            existRow = row;
                            break;
                        }
                    }
                    // 行の有無で分岐
                    if (existRow != null)
                    {
                        // 既存データは実行時間のみ更新
                        existRow.RunTime = runTime;
                    }
                    else
                    {
                        // 新規データ
                        ProcessDto newProcess = new ProcessDto
                        {
                            WindowTitle = windowTitle,
                            ProcessStartTime = startTime,
                            RunTime = runTime,
                            ProcessId = processId,
                            ExecutablePath = executablePath
                        };
                        processList.Add(newProcess);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    loggerManager.LogError("監視リスト更新エラー", ex);
                }
            }
            return processList;
        }
        /// <summary>
        /// プロセスリスト（CSV）ファイルの読込
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<ProcessDto> LoadProcesses(string filePath)
        {
            try
            {
                // 前回保存ファイルがあれば読み込みを実施
                if (File.Exists(filePath))
                {
                    var records = new List<ProcessDto>();
                    using (var streamReader = new StreamReader(filePath))
                    using (var csvReader = new CsvReader(streamReader, CsvConfig))
                    {
                        records = csvReader.GetRecords<ProcessDto>().ToList();
                    }
                    return records;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"プロセスファイル読み込みエラー,{filePath}", ex);
            }
            return new List<ProcessDto>();
        }
        /// <summary>
        /// プロセスリスト（CSV）ファイルの書き込み
        /// </summary>
        /// <param name="processes"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SaveProcesses(List<ProcessDto> processes, string filePath)
        {
            try
            {
                // パスにフォルダ名があれば有無チェックと作成を実施
                string directoryName = Path.GetDirectoryName(filePath);
                if (directoryName != null)
                {
                    // 保存先フォルダが無ければ作成
                    Utilities.CreateDirectory(directoryName);
                }

                // csv書き込み（1行ずつ）
                using (var streamWriter = new StreamWriter(filePath))
                using (var csvWriter = new CsvWriter(streamWriter, CsvConfig))
                {
                    foreach (ProcessDto processDto in processes)
                    {
                        csvWriter.WriteRecord(processDto);
                        csvWriter.NextRecord(); //改行
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"プロセスファイル書き込みエラー,{filePath}", ex);
                return false;
            }
        }

        /// <summary>
        /// プロセスリストから過去分の内容を履歴リストへ移動する。
        /// </summary>
        /// <param name="processes">プロセスリスト</param>
        /// <param name="history">履歴リスト</param>
        /// <returns>エラーが発生した場合はfalse</returns>
        internal static bool MoveProcessDataToHistory(List<ProcessDto> processes, List<ProcessDto> history)
        {
            try
            {
                DateTime today = DateTime.Now;
                // 元リストから過去分を取得
                foreach (ProcessDto processDto in processes.FindAll(x => x.ProcessStartTime < today))
                {
                    // 履歴データに存在しないことを確認
                    //ProcessDto historyProcess = history.Find(x => x.WindowTitle == processDto.WindowTitle && x.ProcessStartTime == processDto.ProcessStartTime);
                    ProcessDto historyProcess = history.Find(x => x.Equals(processDto));
                    if (historyProcess == null)
                    {
                        // 履歴データに追加
                        history.Add(processDto);
                    }
                }
                // 元リストから過去分を削除
                processes.RemoveAll(x => x.ProcessStartTime.Date < today.Date);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"プロセスリストから過去分の内容を履歴リストへ移動時にエラー発生", ex);
                return false;
            }
        }

    }
}