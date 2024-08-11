using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTimeRecords_CSDNF
{
    internal class DictFileDto
    {
        /// <summary>
        /// 保存先ファイルのフルパス
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 辞書内容
        /// </summary>
        public Dictionary<string,string> DataList { get; set; }

        /// <summary>
        /// 辞書に追加すると同時にファイルに保存する。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dictFileDao"></param>
        /// <returns></returns>
        public bool Add(string key, string value, DictFileDao dictFileDao)
        {
            if (DataList.ContainsKey(key))
            {
                DataList[key] = value;
            }
            else
            {
                DataList.Add(key, value);
            }
            return dictFileDao.SaveDictFile(this);
        }

        /// <summary>
        /// 辞書から削除すると同時にファイルに保存する。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dictFileDao"></param>
        /// <returns></returns>
        public bool Remove(string key, DictFileDao dictFileDao)
        {
            DataList.Remove(key);
            return dictFileDao.SaveDictFile(this);
        }

        /// <summary>
        /// 辞書に存在するキーであるか判定する。
        /// </summary>
        /// <param name="key">キー（実行ファイルパス）</param>
        /// <returns></returns>
        public bool ExistsKey(string key)
        {
            return DataList.ContainsKey(key);
        }
    }
}
