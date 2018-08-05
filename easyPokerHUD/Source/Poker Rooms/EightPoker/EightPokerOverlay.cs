using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace easyPokerHUD
{
    public partial class EightPokerOverlay : PokerRoomOverlay
    {
        public EightPokerOverlay(EightPokerHand hand)
        {
            InitializeComponent();

            //Prepare the overlay
            prepareOverlay(hand.tableName, hand.tableSize, hand.playerName);

            //Add an event to the timers
            statsFetcherTimer.Tick += new EventHandler(updatePlayerStatsIfNewHandExists);
            controlSizeUpdateTimer.Tick += new EventHandler(updateControlSizeOrCloseOverlay);

            //Set the size for all controls
            setPositionOfStatsWindowsWindowsAccordingToTableSize();
        }

        //Updates all of the controls size on the form or closes the overlay if the table is closed
        public void updateControlSizeOrCloseOverlay(Object obj, EventArgs eve)
        {
            string windowTableName = getTableWindowName();
            if (getTableWindowName() == null || getTableWindowName().Equals(""))
            {
                EightPokerMain.overlays.TryRemove(tableName, out tableName);
                Close();
                Thread.CurrentThread.Abort();
            }
            else if (this.Height != oldHeight || this.Width != oldWidth)
            {
                oldHeight = Height;
                oldWidth = Width;
                setPositionOfStatsWindowsWindowsAccordingToTableSize();
                setStatsWindowsFontSize();
            }
        }

        //Updates the player stats 
        protected void updatePlayerStatsIfNewHandExists(Object obj, EventArgs eve)
        {
            EightPokerHand hand;
            if (EightPokerMain.newHandsToBeFetched.TryRemove(tableName, out hand))
            {
                hand.players = preparePlayerListForCorrectPositioning(hand.players);
                populateStatsWindows(hand.players);
            }
        }

        //Prepares the player seat numbers according to the size of the table
        protected new List<Player> preparePlayerListForCorrectPositioning(List<Player> players)
        {
            if (tableSize == 2)
            {
                return preparePlayerListForHeadsUp(players);
            } else if (tableSize == 4)
            {
                return preparePlayerListFor4Max(players);
            } else if (tableSize == 6)
            {
                return preparePlayerListFor6Max(players);
            } else if (tableSize == 9)
            {
                return preparePlayerListFor9Max(players);
            } else if (tableSize == 10)
            {
                return preparePlayerListFor10Max(players);
            }
            return players;
        }

        private List<Player> preparePlayerListForHeadsUp(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(4))
            {
                foreach (Player player in players)
                {
                    if (player.seat == 9)
                    {
                        player.seat = 0;
                    }
                    if (player.seat == 4)
                    {
                        player.seat += 3;
                    }
                    player.seat = player.seat + 1;
                }
            }
            return players;
        }

        private List<Player> preparePlayerListFor4Max(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(4))
            {
                foreach (Player player in players)
                {
                    if (player.seat == 1 || player.seat == 4)
                    {
                        player.seat += 2; 
                    }
                    if (player.seat == 7)
                    {
                        player.seat += 1;
                    }
                    if (player.seat == 9)
                    {
                        player.seat = 0;
                    }
                    player.seat = player.seat + 1;
                }
            }
            return players;
        }

        private List<Player> preparePlayerListFor6Max(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(4))
            {
                foreach (Player player in players)
                {
                    if (player.seat == 2 || player.seat == 4 || player.seat == 7)
                    {
                        player.seat += 1;
                    }
                    if (player.seat == 9)
                    {
                        player.seat = 0;
                    }
                    player.seat = player.seat + 1;
                }
            }
            return players;
        }

        private List<Player> preparePlayerListFor9Max(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(5))
            {
                foreach (Player player in players)
                {
                    if (player.seat == 7)
                    {
                        player.seat += 1;
                    }
                    if (player.seat == 10)
                    {
                        player.seat = 0;
                    }
                    player.seat = player.seat + 1;
                }
            }
            return players;
        }

        private List<Player> preparePlayerListFor10Max(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(5))
            {
                foreach (Player player in players)
                {
                    if (player.seat == 10)
                    {
                        player.seat = 0;
                    }
                    player.seat = player.seat + 1;
                }
            }
            return players;
        }

        //Updates the size of the controls for the according table size
        private void setPositionOfStatsWindowsWindowsAccordingToTableSize()
        {
            if (tableSize == 2)
            {
                positionControlsHeadsUp();
            }
            else if (tableSize == 4)
            {
                positionControls4Max();
            }
            else if (tableSize == 6)
            {
                positionControls6Max();
            }
            else if (tableSize == 9)
            {
                positionControls9Max();
            }
            else if (tableSize == 10)
            {
                positionControls10Max();
            }
        }

        private void positionControlsHeadsUp()
        {
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.5), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.65), Convert.ToInt32(Convert.ToDouble(this.Height / 8.5)));
        }

        private void positionControls4Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 2.05)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.5), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 2.05)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.65), Convert.ToInt32(Convert.ToDouble(this.Height / 8.5)));
        }

        private void positionControls6Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.34), Convert.ToInt32(Convert.ToDouble(this.Height / 7.5)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 2.05)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.82), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 3.9), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 2.05)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 13.0), Convert.ToInt32(Convert.ToDouble(this.Height / 7.5)));
        }

        private void positionControls9Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.37), Convert.ToInt32(Convert.ToDouble(this.Height / 7.5)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 2.7)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 1.79)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.6), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.47), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 5.5), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 1.79)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 2.7)));
            statsWindow10.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 13), Convert.ToInt32(Convert.ToDouble(this.Height / 7.5)));
        }

        private void positionControls10Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.6), Convert.ToInt32(Convert.ToDouble(this.Height / 24)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 2.7)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.24), Convert.ToInt32(Convert.ToDouble(this.Height / 1.79)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.6), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.47), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 5.5), Convert.ToInt32(Convert.ToDouble(this.Height / 1.305)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 1.79)));
            statsWindow8.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 35.0), Convert.ToInt32(Convert.ToDouble(this.Height / 2.7)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 5.5), Convert.ToInt32(Convert.ToDouble(this.Height / 24)));
            statsWindow10.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.47), Convert.ToInt32(Convert.ToDouble(this.Height / 24)));
        }
    }
}
