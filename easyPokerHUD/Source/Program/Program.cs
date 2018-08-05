using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace easyPokerHUD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Only put mandatory stuff in this class
        /// </summary>
        static Mutex mutex = new Mutex(true, "{8F6F0AC4-B9A1-45fd-B1Y4T-72F04E6BDE8F}");
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                //Necessary
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ApplicationUpdater.checkForUpdatesAndInstallIfNecessary();
                Application.Run(new MainWindow());
                mutex.ReleaseMutex();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
