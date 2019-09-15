using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace easyPokerHUD
{
    public class PokerRoomHand
    {
        /// <summary>
        /// Properties of this hand
        /// </summary>
        public string path;
        public string pokerRoom;
        public string tableName;
        public string playerName;
        public int tableSize;
        public bool tournament;

        /// <summary>
        /// Parts of this hand
        /// </summary>
        public string[] hand;
        public string handInformation;
        public string tableInformation;
        public string[] playerOverview;
        public string[] preflop;
        public string[] postflop;

        /// <summary>
        /// The players in this hand
        /// </summary>
        public List<Player> players = new List<Player>();

        /// <summary>
        /// Reads out specified handhistory from the back and returns only the last played hand
        /// </summary>
        /// <param name="path"></param>
        /// <param name="skipKeyword"></param>
        /// <param name="takeKeyword"></param>
        /// <returns></returns>
        protected string[] GetHand(string path, string skipKeyword, string takeKeyword)
        {
            FileInfo file = new FileInfo(path);
            while (IsFileLocked(file))
            {
            }
            var hand = File.ReadLines(path).Reverse().SkipWhile(s => !s.Contains(skipKeyword)).TakeWhile(s => s != takeKeyword).Reverse();
            return hand.ToArray();
        }

        /// <summary>
        /// Resets the hadActionInPot variable in the whole player list
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        protected static List<Player> ResetHadActionInPot(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.hadActionInPot = false;
            }
            return players;
        }

        /// <summary>
        /// Inserts the preflop stats into the player list
        /// </summary>
        /// <param name="preFlop"></param>
        /// <param name="players"></param>
        /// <param name="wordForCall"></param>
        /// <param name="wordForRaise"></param>
        /// <param name="wordForBet"></param>
        /// <returns></returns>
        protected static List<Player> InsertPreFlopStats(string[] preFlop, List<Player> players,
            string wordForCall, string wordForRaise, string wordForBet)
        {
            foreach (string line in preFlop)
            {
                foreach (Player player in players)
                {
                    if (line.Contains(player.name) && !player.hadActionInPot)
                    {
                        if (line.Contains(wordForCall))
                        {
                            player.preflopCalls++;
                            player.hadActionInPot = true;
                        }
                        else if (line.Contains(wordForBet) || line.Contains(wordForRaise))
                        {
                            player.preflopBetsAndRaises++;
                            player.hadActionInPot = true;
                        }
                    }
                }
            }
            return ResetHadActionInPot(players);
        }

        /// <summary>
        /// Inserts the postflop stats into the player list
        /// </summary>
        /// <param name="postflop"></param>
        /// <param name="players"></param>
        /// <param name="wordForCall"></param>
        /// <param name="wordForRaise"></param>
        /// <param name="wordForBet"></param>
        /// <param name="wordForCheck"></param>
        /// <param name="wordForFold"></param>
        /// <returns></returns>
        protected static List<Player> InsertPostFlopStats(string[] postflop, List<Player> players,
            string wordForCall, string wordForRaise, string wordForBet, string wordForCheck, string wordForFold)
        {
            foreach (string line in postflop)
            {
                foreach (Player player in players)
                {
                    if (line.Contains(player.name) && !player.hadActionInPot)
                    {
                        if (line.Contains(wordForCall) || line.Contains(wordForCheck) || line.Contains(wordForFold))
                        {
                            player.postflopCallsChecksAndFolds++;
                        }
                        else if (line.Contains(wordForBet) || line.Contains(wordForRaise))
                        {
                            player.postflopBetsAndRaises++;
                        }
                    }
                }
            }
            return players;
        }

        /// <summary>
        /// Checks if a file is still used by another process. Needs to be used with a while loop
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
        }
    }
}
