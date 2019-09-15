using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace easyPokerHUD
{
    public class EightPokerMain
    {
        public static HandHistoryWatcher handHistoryWatcher;
        public static List<Player> playerCache = new List<Player>();
        public static ConcurrentDictionary<string, string> overlays = new ConcurrentDictionary<string, string>();
        public static ConcurrentDictionary<string, EightPokerHand> newHandsToBeFetched = new ConcurrentDictionary<string, EightPokerHand>();

        /// <summary>
        /// Activates the Filewatcher
        /// </summary>
        public static void activateFileWatcher()
        {
            handHistoryWatcher = new HandHistoryWatcher(System.Environment.SpecialFolder.MyDocuments, "888poker", "HandHistory");
            handHistoryWatcher.Changed += getInformationAndPassItToHUD;
        }

        /// <summary>
        /// Creates a hand, fills it with the information about players and finally passes it on to the hud
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public static void getInformationAndPassItToHUD(object source, FileSystemEventArgs e)
        {
            //888 Poker stores summary txts, that should be ignored
            if (e.FullPath.Contains("Summary"))
            {
                return;
            }

            //Create a new hand and check if it is valid to be displayed
            EightPokerHand hand = new EightPokerHand(e.FullPath);
            if (checkIfHandIsValidForHUD(hand.tableSize, hand.tableInformation, hand.players, hand.playerName))
            {
                combineDataSets(hand.players);
                createNewOverlayOrStoreInformation(hand);
            }
        }

        /// <summary>
        /// Creates a new overlay or stores the information in a list to be fetched by the overlay timer
        /// </summary>
        /// <param name="hand"></param>
        private static void createNewOverlayOrStoreInformation(EightPokerHand hand)
        {
            if (overlays.ContainsKey(hand.tableName))
            {
                newHandsToBeFetched.TryAdd(hand.tableName, hand);
            }
            else
            {
                newHandsToBeFetched.TryAdd(hand.tableName, hand);
                overlays.TryAdd(hand.tableName, hand.tableName);
                new Thread(() => Application.Run(new EightPokerOverlay(hand))).Start();
            }
        }

        /// <summary>
        /// Gets the stats stored in the database
        /// </summary>
        /// <param name="players"></param>
        protected static void combineDataSets(List<Player> players)
        {
            foreach (Player player in players)
            {
                try
                {
                    Player playerStoredInCache = playerCache.Single(p => p.name.Equals(player.name));
                    player.handsPlayed += playerStoredInCache.handsPlayed;
                    player.preflopCalls += playerStoredInCache.preflopCalls;
                    player.preflopBetsAndRaises += playerStoredInCache.preflopBetsAndRaises;
                    player.postflopBetsAndRaises += playerStoredInCache.postflopBetsAndRaises;
                    player.postflopCallsChecksAndFolds += playerStoredInCache.postflopCallsChecksAndFolds;
                    playerCache[playerCache.IndexOf(playerStoredInCache)] = player;
                }
                catch
                {
                    player.CombinethisPlayerWithStoredStats();
                    playerCache.Add(player);
                }
            }
        }

        /// <summary>
        /// Updates the players in the database
        /// </summary>
        public static void UpdatePlayersInDatabaseFromCache()
        {
            foreach (Player player in playerCache)
            {
                player.UpdateOrCreatePlayerInDatabase();
            }
        }

        /// <summary>
        /// Checks whether this hand is eligible to be hudded 
        /// </summary>
        /// <param name="tableSize"></param>
        /// <param name="handInformation"></param>
        /// <param name="players"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        protected static bool checkIfHandIsValidForHUD(int tableSize, string handInformation, List<Player> players, string playerName)
        {
            //If the player is sitting out, the playerName will return empty
            if (playerName.Equals(""))
            {
                return false;
            }
            
            //If 888 Poker bugs out 
            foreach (Player player in players)
            {   
                if (player.name.Equals("")){
                    return false;
                }
            }

            //All these table sizes are supported
            if (tableSize == 2)
            {
                return true;
            }
            else if (tableSize == 4)
            {
                return true;
            }
            else if (tableSize == 6)
            {
                return true;
            }
            else if (tableSize == 9)
            {
                return true;
            }
            else if (tableSize == 10)
            {
                return true;
            }
            return false;
        }
    }
}
