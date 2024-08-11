using NLog.LayoutRenderers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RunTimeRecords_CSDNF
{
    internal class DictFileDao
    {
        readonly LoggerManager loggerManager = new LoggerManager();
        readonly IFileSystem fileSystem;
        readonly char SEPARATOR = '@';

        /// <summary>
        /// 指定したファイルシステムで処理するためのコンストラクタ
        /// </summary>
        /// <param name="fileSystem"></param>
        public DictFileDao(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// 実際のファイルシステムで処理するためのコンストラクタ
        /// </summary>
        public DictFileDao() : this(new FileSystem())
        {
            // 実際のファイルシステムのインスタンスを生成して別のコンストラクタに渡す
        }

        /// <summary>
        /// ファイルからテキストを読み込む、Dto生成
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DictFileDto LoadDictFile(string filePath)
        {
            var dataList = new Dictionary<string, string>();
            // 前回保存ファイルがあれば読み込みを実施
            if (fileSystem.File.Exists(filePath))
            {
                // リストファイル読み込み
                try
                {
                    var lineList = fileSystem.File.ReadAllLines(filePath);
                    foreach (var line in lineList)
                    {
                        var keyValue = line.Split(SEPARATOR);
                        dataList.Add(keyValue[0], keyValue[1]);
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                    loggerManager.LogError($"辞書ファイル読み込みエラー,{filePath}", ex);
                }
            }
            // Dto生成
            DictFileDto dictFileDto = new DictFileDto
            {
                FilePath = filePath,
                DataList = dataList
            };
            return dictFileDto;
        }

        /// <summary>
        /// テキストをファイルに書き込む
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        public bool SaveDictFile(DictFileDto fileDto)
        {
            // パスにフォルダ名があれば有無チェックと作成を実施
            string directoryName = fileSystem.Path.GetDirectoryName(fileDto.FilePath);
            if (directoryName != null)
            {
                // 保存先フォルダが無ければ作成
                Utilities.CreateDirectory(directoryName, fileSystem);
            }
            // 書き込み処理
            try
            {
                List<string> dictList = new List<string>();
                foreach (var dict in fileDto.DataList)
                {
                    dictList.Add(dict.Key + SEPARATOR + dict.Value);
                }
                fileSystem.File.WriteAllLines(fileDto.FilePath, dictList);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"辞書ファイル書き込みエラー,{fileDto.FilePath}", ex);
                return false;
            }
        }
    }
}
