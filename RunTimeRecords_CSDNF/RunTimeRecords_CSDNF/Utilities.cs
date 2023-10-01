using System;
using System.IO;

namespace RunTimeRecords_CSDNF
{
    public static class Utilities
    {
        /// <summary>
        /// 値の両端に「"」があれば削除して戻す
        /// </summary>
        /// <param name="value">削除対象文字列</param>
        /// <returns>削除後文字列</returns>
        public static string DoubleQuateDelete(string value)
        {
            if (value[0].Equals("\"") && value[value.Length - 1].Equals("\""))
            {
                return value.Substring(1, value.Length - 2);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 指定したパスのフォルダが存在していなければ作成する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryInfo CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
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