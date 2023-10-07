using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System;
using System.Management;//あらかじめ「System.Management」を参照に追加しておくこと
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using System.Linq;

namespace RunTimeRecords_CSDNF
{
    public class ProcessesDao
    {
        private static readonly string SaveFolderPath = @".\save";
        private static readonly string SaveFileName = @"processes.csv";
        private static readonly string SaveFilePath = SaveFolderPath + @"\" + SaveFileName;
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
        public static DataTable GetProcessList(DataTable processDataTable , List<string> whiteList)
        {
            var nowTime = DateTime.Now;

            // プロセス一覧を取得してid,実行パスの辞書を作成
            Dictionary<int, string> processPathDictionary = new Dictionary<int, string>();
            using (ManagementObjectCollection list = _managementClass.GetInstances()) // このインスタンスは都度再生成が必要
            {
                foreach (ManagementBaseObject process in list)
                {
                    // 実行パスが無いものはスキップ
                    if (process["ExecutablePath"] == null)
                    {
                        continue;
                    }
                    string executablePath = process["ExecutablePath"].ToString();
                    // ホワイトリストに無ければスキップ
                    //if (!whiteList.Contains(executablePath))
                    if (!whiteList.Exists(x => executablePath.StartsWith(x)))
                    {
                        continue;
                    }
                    int processId = int.Parse(process["ProcessId"].ToString());
                    // 辞書に追加
                    processPathDictionary.Add(processId, executablePath);
                }
            }

            // プロセス一覧を取得してデータテーブルを更新
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
                    DataRow existRow = null;
                    foreach (DataRow row in processDataTable.Rows)
                    {
                        if (row["WindowTitle"].Equals(windowTitle) && ((DateTime)row["ProcessStartTime"]).ToString().Equals(startTime.ToString()))
                        {
                            existRow = row;
                            break;
                        }
                    }
                    // 行の有無で分岐
                    if (existRow != null)
                    {
                        // 既存データは実行時間のみ更新
                        existRow["RunTime"] = runTime;
                    }
                    else
                    {
                        // 新規データ
                        DataRow newRow = processDataTable.NewRow();
                        newRow["WindowTitle"] = windowTitle;
                        newRow["ProcessStartTime"] = startTime;
                        newRow["RunTime"] = runTime;
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

        public static List<ProcessDto> LoadProcesses()
        {
            try
            {
                // 前回保存ファイルがあれば読み込みを実施
                if (File.Exists(SaveFilePath))
                {
                    var records = new List<ProcessDto>();
                    using (var streamReader = new StreamReader(SaveFilePath))
                    using (var csvReader = new CsvReader(streamReader, CsvConfig))
                    {
                        records = csvReader.GetRecords<ProcessDto>().ToList();
                    }
                    return records;
                }
            }
            catch( Exception ex) 
            {
                Console.WriteLine(ex);
            }
            return new List<ProcessDto>();
        }

        public static bool SaveProcesses(List<ProcessDto> processes)
        {
            try
            {
                // 保存先フォルダが無ければ作成
                Utilities.CreateDirectory(SaveFolderPath);

                // csv書き込み（1行ずつ）
                using (var streamWriter = new StreamWriter(SaveFilePath))
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
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

    }
}