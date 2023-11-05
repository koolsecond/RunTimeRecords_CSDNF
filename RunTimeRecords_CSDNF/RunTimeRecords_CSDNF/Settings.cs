
using System;
using System.Configuration;

namespace RunTimeRecords_CSDNF
{
    [Serializable()]
    internal class Settings
    {
        // App.configのKey固定文字
        private static readonly string KeyMasterFolderPath = "MasterFolderPath";
        private static readonly string KeyWhiteListFileName = "WhiteListFileName";
        private static readonly string KeyBlackListFileName = "BlackListFileName";
        private static readonly string KeySaveFolderPath = "SaveFolderPath";
        private static readonly string KeySaveFileName = "SaveFileName";
        private static readonly string KeyHistoryFileName = "HistoryFileName";
        //private static readonly Encoding SaveFileEncoding = Encoding.GetEncoding("shift_jis");

        private string _masterFolderPath;
        public String MasterFolderPath
        {
            get { return _masterFolderPath; }
        }
        private string _whiteListFileName;
        public String WhiteListFileName
        {
            get { return _whiteListFileName; }
        }

        public String WhiteListFilePath
        {
            get { return _masterFolderPath + @"\" + _whiteListFileName; ; }
        }
        private string _blackListFileName;
        public String BlackListFileName
        {
            get { return _blackListFileName; }
        }

        public String BlackListFilePath
        {
            get { return _masterFolderPath + @"\" + _blackListFileName; ; }
        }

        private string _saveFolderPath;
        public String SaveFolderPath
        {
            get { return _saveFolderPath; }
        }
        private string _saveFileName;
        public String SaveFileName
        {
            get { return _saveFileName; }
        }

        public String SaveFilePath
        {
            get { return _saveFolderPath + @"\" + _saveFileName; ; }
        }
        private string _historyFileName;
        public String HistoryFileName
        {
            get { return _historyFileName; }
        }

        public String HistoryFilePath
        {
            get { return _saveFolderPath + @"\" + _historyFileName; ; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Settings()
        {
            // 全て初期化
            _masterFolderPath = "";
            _whiteListFileName = "";
            _blackListFileName = "";
            _saveFolderPath = "";
            _saveFileName = "";
            _historyFileName = "";
        }

        /// <summary>
        /// Settingクラスのただ１つのインスタンス
        /// </summary>
        [NonSerialized()]
        private static Settings _instance;
        [System.Xml.Serialization.XmlIgnore]
        public static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Settings();
                }
                return _instance;
            }
        }

        /// <summary>
        /// App.configから各種設定を読み込む
        /// </summary>
        public void ReadAppConfig()
        {
            _instance._masterFolderPath = ConfigurationManager.AppSettings[KeyMasterFolderPath];
            _instance._whiteListFileName = ConfigurationManager.AppSettings[KeyWhiteListFileName];
            _instance._blackListFileName = ConfigurationManager.AppSettings[KeyBlackListFileName];
            _instance._saveFolderPath = ConfigurationManager.AppSettings[KeySaveFolderPath];
            _instance._saveFileName = ConfigurationManager.AppSettings[KeySaveFileName];
            _instance._historyFileName = ConfigurationManager.AppSettings[KeyHistoryFileName];
        }
    }
}
