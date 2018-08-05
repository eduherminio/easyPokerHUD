using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace easyPokerHUD
{
    class MainMethods
    {
        private static List<string> positiveMessages = new List<string>();
        private static List<string> negativeMessages = new List<string>();

        //Checks which pokerRooms are installed and activates their filewatchers
        public static void activateFileWatchers()
        {
            if (checkIfPokerRoomIsInstalled("PokerStars"))
            {
                PokerStarsMain.activateFileWatcher();
            }
            if (checkIfPokerRoomIsInstalled("888poker"))
            {
                EightPokerMain.activateFileWatcher();
            }
        }

        //Accesses the uninstall list of windows and checks if it contains the pokerroom-name 
        private static bool checkIfPokerRoomIsInstalled(string pokerRoom)
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        try
                        {
                            string name = (string)subkey.GetValue("DisplayName");
                            if (name.Contains(pokerRoom))
                            {
                                return true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return false;
        }

        //Opens the QuickStartGuide in a new browser tab
        public static void openQuickStartGuide(Object obj, EventArgs e)
        {
            Process.Start("https://easypokerhud.com/quickstart-guides/");
        }

        //Adds all status messages to two lists
        public static void addMessagesToLists()
        { 
            positiveMessages = new List<string>();
            negativeMessages = new List<string>();

            if (PokerStarsMain.handHistoryWatcher != null)
            {
                positiveMessages.Add(PokerStarsMain.handHistoryWatcher.pMessage);
                negativeMessages.Add(PokerStarsMain.handHistoryWatcher.nMessage);
            }

            if (EightPokerMain.handHistoryWatcher != null)
            {
                positiveMessages.Add(EightPokerMain.handHistoryWatcher.pMessage);
                negativeMessages.Add(EightPokerMain.handHistoryWatcher.nMessage);
            }
        }

        //Builds a success and an error string to display in the main window
        public static string[] getHUDStatusStrings()
        {
            //Get all status messages from every FileWatcher class
            addMessagesToLists();

            //Start with two empty strings and build them into properly formatted positive and negative messages
            string positiveMessage = "";
            string negativeMessage = "";
            
            foreach(string line in positiveMessages)
            {
                if (line != "")
                {
                    if (positiveMessage != "")
                    {
                        positiveMessage = positiveMessage + ", ";
                    }
                    positiveMessage = positiveMessage + line;
                }
            }

            foreach (string line in negativeMessages)
            {
                if (line != "")
                {
                    if (negativeMessage != "")
                    {
                        negativeMessage = negativeMessage + ", ";
                    }
                    negativeMessage = negativeMessage + line;
                }
            }

            //Put both status strings into an array and return it
            string[] statusStrings = new string[2];
            statusStrings[0] = positiveMessage;
            statusStrings[1] = negativeMessage;

            return statusStrings;
        }
    }
}
