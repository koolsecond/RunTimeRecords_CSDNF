using System;
using System.Collections.Generic;
using System.IO;

namespace RunTimeRecords_CSDNF
{
    internal class ListFileDao
    {
        /// <summary>
        /// ファイルからテキストを読み込む、Dto生成
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ListFileDto LoadListFile(string filePath)
        {
            List<string> dataList = new List<string>();
            // 前回保存ファイルがあれば読み込みを実施
            if (File.Exists(filePath))
            {
                // リストファイル読み込み
                try
                {
                    dataList.AddRange(File.ReadAllLines(filePath));
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex);
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
            string directoryName = Path.GetDirectoryName(fileDto.FilePath);
            if (directoryName != null)
            {
                // 保存先フォルダが無ければ作成
                Utilities.CreateDirectory(directoryName);
            }
            // 書き込み処理
            try
            {
                File.WriteAllLines(fileDto.FilePath, fileDto.DataList);
                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}
