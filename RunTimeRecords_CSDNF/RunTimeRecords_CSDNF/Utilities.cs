using System;
using System.IO.Abstractions;

namespace RunTimeRecords_CSDNF
{
    public static class Utilities
    {
        /// <summary>
        /// 指定したパスのフォルダが存在していなければ作成する
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileSystem">実際のファイルシステムなら指定不要。テスト時はモック指定</param>
        /// <returns></returns>
        public static void CreateDirectory(string path, IFileSystem fileSystem = null)
        {
            if (fileSystem == null)
            {
                fileSystem = new FileSystem(); // 実際のファイルシステム
            }

            if (fileSystem.Directory.Exists(path))
            {
                return;
            }
            fileSystem.Directory.CreateDirectory(path);
        }

        /// <summary>
        /// TimeSpanデータを固定のフォーマットの文字列に変更する
        /// </summary>
        /// <param name="timeSpan">元データ</param>
        /// <returns>変換後文字列</returns>
        public static string TimeFormatString(TimeSpan timeSpan)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.Hours, (int)timeSpan.Minutes, (int)timeSpan.Seconds);
        }
    }
}