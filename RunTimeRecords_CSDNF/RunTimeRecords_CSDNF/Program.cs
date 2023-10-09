using System;
using System.Threading;
using System.Windows.Forms;

namespace RunTimeRecords_CSDNF
{
    internal static class Program
    {
        // アプリケーション固有名
        private static readonly string mutexName = "koolsecond.RuntimeRecords.cs.netframwork4.8";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Mustexオブジェクトを作成する。
            Mutex mutexObject = new Mutex(true, mutexName, out bool createdNew);
            // 初期所有権が付与されていなければ起動済みと判断して終了
            if (!createdNew)
            {
                MessageBox.Show("既に起動しています。", "多重起動禁止");
                mutexObject.Close();
                return;
            }
            // アプリケーションの本体を起動
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            finally
            {
                // Mutexを解放する。
                mutexObject.ReleaseMutex();
                mutexObject.Close();
            }

        }
    }
}
