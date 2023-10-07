using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RunTimeRecords_CSDNF
{
    public class WhiteListDao
    {
        private static readonly string SaveFolderPath = @".\master";
        private static readonly string SaveFileName = @"whiteList.txt";
        private static readonly string SaveFilePath = SaveFolderPath + @"\" + SaveFileName;
        private static readonly Encoding SaveFileEncoding = Encoding.GetEncoding("shift_jis");

        public WhiteListDao()
        {
            // コンストラクタは不要
        }

        public static List<string> LoadWhiteList()
        {
            // 前回保存ファイルがあれば読み込みを実施
            if (File.Exists(SaveFilePath))
            {
                //行ごとのstring配列として、テキストファイルの内容を全て読込、List<string>に変換
                try
                {
                    return File.ReadAllLines(SaveFilePath, SaveFileEncoding).ToList<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return new List<string>();
        }

        public static bool SaveWhiteList(List<string> whiteList)
        {
            // 保存先フォルダが無ければ作成
            Utilities.CreateDirectory(SaveFolderPath);
            // 一括書き込み
            string[] stringList = whiteList.ToArray<string>();
            try
            {
                File.WriteAllLines(SaveFilePath,stringList, SaveFileEncoding);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

    }
}