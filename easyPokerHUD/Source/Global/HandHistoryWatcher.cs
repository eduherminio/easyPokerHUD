using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;

namespace easyPokerHUD
{
    public class HandHistoryWatcher : FileSystemWatcher
    {
        Timer directorySearcher;
        public string pMessage = "";
        public string nMessage = "";

        private Environment.SpecialFolder windowsEnvironmentFolder;
        private string pokerRoom;
        private string handHistoryFolder;

        //Enables the filewatcher with specified path
        public HandHistoryWatcher(Environment.SpecialFolder windowsEnvironmentFolder, string pokerRoom, string handHistoryFolder)
        {
            //Set the global variables
            this.windowsEnvironmentFolder = windowsEnvironmentFolder;
            this.pokerRoom = pokerRoom;
            this.handHistoryFolder = handHistoryFolder;

            //Set the filters for the filewatcher
            NotifyFilter = NotifyFilters.LastWrite;
            Filter = "*.txt";
            IncludeSubdirectories = true;

            //Enable Filewatcher
            enableFileWatcher();
        }

        //Calls the enableFilewatcher method
        private void tryEnablingFileWatcher(Object obj, EventArgs eve)
        {
            enableFileWatcher();
        }

        //Checks, whether a valid path has been found and enables the filewatching
        private void enableFileWatcher()
        {
            Path = getHandHistoryDirectory();

            //Begin watching.
            if (!Path.Equals(""))
            {
                startFileWatcher();
            }
            else
            {
                showErrorMessageAndStartdirectorySearcher();
            }
        }

        //Start the file watcher and dispose the directory searcher if necessary
        private void startFileWatcher()
        {
            if (directorySearcher != null)
            {
                directorySearcher.Dispose();
            }
            EnableRaisingEvents = true;
            pMessage = pokerRoom;
            nMessage = "";
        }

        //Accesses specified folders and looks for the handhistory
        private string getHandHistoryDirectory()
        {
            try
            {
                //Start in the user folder, where the poker room stores the hand history and move on from there
                var startingDirectory = new DirectoryInfo(@Environment.GetFolderPath(windowsEnvironmentFolder));
                var possibleDirectories = startingDirectory.GetDirectories().Where(s => s.ToString().Contains(pokerRoom)).OrderByDescending(f => f.LastWriteTime);

                //Take the list of possible directories and return the most recent one, that contains the hand history folder
                foreach (DirectoryInfo possibleDirectory in possibleDirectories)
                {
                    try
                    {
                        var probableDirectory = possibleDirectory.GetDirectories().Where(s => s.ToString().Contains(handHistoryFolder)).Single();
                        return probableDirectory.FullName.ToString();
                    }
                    catch { /*Do nothing when no such directory is found */}
                }
                return "";
            } catch
            {
                return "";
            }
        }

        //Starts a directory searcher that keeps looking for the directory
        private void showErrorMessageAndStartdirectorySearcher()
        {
            if (directorySearcher == null)
            {
                directorySearcher = new Timer();
                showErrorMessageAndStartdirectorySearcher();
                directorySearcher.Interval += 3000;
                directorySearcher.Elapsed += tryEnablingFileWatcher;
                directorySearcher.Start();
                nMessage = pokerRoom;
            }
        }
    }
}
