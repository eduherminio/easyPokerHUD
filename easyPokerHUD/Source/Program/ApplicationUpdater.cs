using System;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace easyPokerHUD
{
    class ApplicationUpdater
    {
        static string updateInformationURL = @"https://easypokerhud.com/updateInformation.xml";
        static string pathToSaveDownload = @Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\easyPokerHUD";

        //Checks for udpates and installs them if necessary
        static public void checkForUpdatesAndInstallIfNecessary() 
        {
            string updateInformation = downloadUpdateInformation();
            if (updateInformation != null && checkIfThisVersionIsOutdated(updateInformation))
            {
                MessageBox.Show("Update found! easyPokerHUD will now download and install the update");
                downloadAndExecuteInstaller(updateInformation);
            }
            else
            {
                return;
            }
        }

        //Downloads the xml document from easyPokerHUD.com
        private static string downloadUpdateInformation()
        {
            try
            {
                string updateInformation = new WebClient().DownloadString(updateInformationURL);
                return updateInformation;
            }
            catch
            {
                return null;
            }
        }

        //Checks if the current version is outdated
        private static Boolean checkIfThisVersionIsOutdated(string updateInformation)
        {
            var result = getCurrentProductVersion().CompareTo(getNewestVersion(updateInformation));
            if (result < 0)
            {
                return true;
            }
            return false;
        }

        //Gets the current product version
        public static string getCurrentProductVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            return version;
        }

        //Reads out the newest version stored in the xml document
        private static string getNewestVersion(string updateInformation)
        {
            string version = updateInformation.Substring(updateInformation.IndexOf("<version>") + 9);
            version = version.Substring(0, version.IndexOf("</"));
            return version;
        }

        //Downloads the installer
        private static void downloadAndExecuteInstaller(string updateInformation)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(getDownloadURL(updateInformation), pathToSaveDownload + @"\easyPokerHUD.exe");
                    Process.Start(pathToSaveDownload + @"\easyPokerHUD.exe");
                    Environment.Exit(0);
                }
            }
            catch 
            {
                MessageBox.Show("Sometimes I'm a tiny helpless program. I couldn't update myself automatically. "+
                    "Please help me by removing your current installation and installing the update manually from www.easypokerhud.com");
            }
        }

        //Reads out the download URL where the update is stored
        private static string getDownloadURL(string updateInformation)
        {
            string downloadURL = updateInformation.Substring(updateInformation.IndexOf("<url>") + 5);
            downloadURL = downloadURL.Substring(0, downloadURL.IndexOf("</"));
            return downloadURL;
        }
    }
}
