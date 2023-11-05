using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace RunTimeRecords_CSDNF
{
    public class ListFileDao
    {
        readonly LoggerManager loggerManager = new LoggerManager();
        readonly IFileSystem fileSystem;

        /// <summary>
        /// 指定したファイルシステムで処理するためのコンストラクタ
        /// </summary>
        /// <param name="fileSystem"></param>
        public ListFileDao(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }
        /// <summary>
        /// 実際のファイルシステムで処理するためのコンストラクタ
        /// </summary>
        public ListFileDao() : this(new FileSystem())
        {
            // 実際のファイルシステムのインスタンスを生成して別のコンストラクタに渡す
        }

        /// <summary>
        /// ファイルからテキストを読み込む、Dto生成
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ListFileDto LoadListFile(string filePath)
        {
            List<string> dataList = new List<string>();
            // 前回保存ファイルがあれば読み込みを実施
            if (fileSystem.File.Exists(filePath))
            {
                // リストファイル読み込み
                try
                {
                    dataList.AddRange(fileSystem.File.ReadAllLines(filePath));
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
                    loggerManager.LogError($"リストファイル読み込みエラー,{filePath}", ex);
                }
            }
            // Dto生成
            ListFileDto listFileDto = new ListFileDto
            {
                FilePath = filePath,
                DataList = dataList
            };
            return listFileDto;
        }

        /// <summary>
        /// テキストをファイルに書き込む
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        public bool SaveListFile(ListFileDto fileDto)
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
                fileSystem.File.WriteAllLines(fileDto.FilePath, fileDto.DataList);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                loggerManager.LogError($"リストファイル書き込みエラー,{fileDto.FilePath}", ex);
                return false;
            }
        }
    }
}
