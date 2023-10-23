using System.Collections.Generic;

namespace RunTimeRecords_CSDNF
{
    public class ListFileDto
    {
        /// <summary>
        /// 保存先ファイルのフルパス
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// データ内容
        /// </summary>
        public List<string> DataList { get; set; }

        /// <summary>
        /// データリストに追加すると同時にファイルに保存する。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listFileDao"></param>
        /// <returns></returns>
        public bool Add(string data, ListFileDao listFileDao)
        {
            DataList.Add(data);
            return listFileDao.SaveListFile(this);
        }

        /// <summary>
        /// データリストに追加すると同時にファイルに保存する。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="listFileDao"></param>
        /// <returns></returns>
        public bool Remove(string data, ListFileDao listFileDao)
        {
            DataList.Remove(data);
            return listFileDao.SaveListFile(this);
        }
    }
}
