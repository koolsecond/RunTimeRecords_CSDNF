using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace RunTimeRecords_CSDNF
{
    internal class ProcessSummaryDao
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
        public ProcessSummaryDao() { }

        /// <summary>
        /// 集計処理を実施し、画面に表示
        /// </summary>
        public List<ProcessSummaryDto> Summary(List<ProcessDto> data)
        {
            // 集計処理の実施
            return data
                .GroupBy(x => x.WindowTitle)
                .Select(x => new
                ProcessSummaryDto
                {
                    WindowTitle = x.Key
                    ,
                    TotalRunTime = new TimeSpan(x.Sum(y => y.RunTime.Ticks))
                    ,
                    LastDate = x.Max(y => y.ProcessStartTime)
                })
                .OrderByDescending(x => x.LastDate)
                .ToList();
        }
        /// <summary>
        /// 集計データの（CSV）ファイルの書き込み
        /// </summary>
        /// <param name="summaryList"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool SaveSummaryList(List<ProcessSummaryDto> summaryList, string filePath)
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
                    foreach (ProcessSummaryDto processDto in summaryList)
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
    }
}
