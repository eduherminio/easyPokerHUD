using System;
using System.Drawing;
using System.Threading;

namespace easyPokerHUD
{
    public partial class PokerStarsOverlay : PokerRoomOverlay
    {
        public PokerStarsOverlay(PokerStarsHand hand)
        {
            InitializeComponent();

            //Prepare the overlay
            PrepareOverlay(hand.tableName, hand.tableSize, hand.playerName);

            //Add an event to the timers
            statsFetcherTimer.Tick += new EventHandler(UpdatePlayerStatsIfNewHandExists);
            controlSizeUpdateTimer.Tick += new EventHandler(UpdateControlSizeOrCloseOverlay);

            //Set the size for all controls
            SetPositionOfStatsWindowsWindowsAccordingToTableSize();
        }

        /// <summary>
        /// Updates all of the controls size on the form or closes the overlay if the table is closed
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eve"></param>
        public void UpdateControlSizeOrCloseOverlay(object obj, EventArgs eve)
        {
            if (GetTableWindowName() == "")
            {
                PokerStarsMain.overlays.TryRemove(tableName, out tableName);
                Close();
                Thread.CurrentThread.Abort();
            }
            else if (Height != oldHeight || Width != oldWidth)
            {
                oldHeight = Height;
                oldWidth = Width;
                SetPositionOfStatsWindowsWindowsAccordingToTableSize();
                SetStatsWindowsFontSize();
            }
        }

        /// <summary>
        /// Updates the player stats
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eve"></param>
        protected void UpdatePlayerStatsIfNewHandExists(object obj, EventArgs eve)
        {
            PokerStarsHand hand;
            if (PokerStarsMain.newHandsToBeFetched.TryRemove(tableName, out hand))
            {
                hand.players = PreparePlayerListForCorrectPositioning(hand.players);
                PopulateStatsWindows(hand.players);
            }
        }

        /// <summary>
        /// Updates the size of the controls for the according table size
        /// </summary>
        protected void SetPositionOfStatsWindowsWindowsAccordingToTableSize()
        {
            if (tableSize == 2)
            {
                PositionControlsHeadsUp();
            }
            else if (tableSize == 3)
            {
                PositionControls3Max();
            }
            else if (tableSize == 6)
            {
                PositionControls6Max();
            }
            else if (tableSize == 9)
            {
                PositionControls9Max();
            }
        }

        /// <summary>
        /// Positions the controls relative to the window size for HeadsUp
        /// </summary>
        private void PositionControlsHeadsUp()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 2.4), Convert.ToInt32(Convert.ToDouble(Height / 1.25)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.65), Convert.ToInt32(Convert.ToDouble(Height / 6.5)));
        }

        /// <summary>
        /// Positions the controls relative to the window size for 3max
        /// </summary>
        private void PositionControls3Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.25), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 2.4), Convert.ToInt32(Convert.ToDouble(Height / 1.25)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 25.0), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
        }

        /// <summary>
        /// Positions the controls relative to the window size for 6Max
        /// </summary>
        private void PositionControls6Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.25), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.25), Convert.ToInt32(Convert.ToDouble(Height / 1.58)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 2.4), Convert.ToInt32(Convert.ToDouble(Height / 1.25)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 25.0), Convert.ToInt32(Convert.ToDouble(Height / 1.58)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 25.0), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.65), Convert.ToInt32(Convert.ToDouble(Height / 6.5)));
        }

        /// <summary>
        /// Positions the controls relative to the window size for 9Max
        /// </summary>
        private void PositionControls9Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.34), Convert.ToInt32(Convert.ToDouble(Height / 8)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.26), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.2), Convert.ToInt32(Convert.ToDouble(Height / 1.9)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 1.4), Convert.ToInt32(Convert.ToDouble(Height / 1.37)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 2.4), Convert.ToInt32(Convert.ToDouble(Height / 1.25)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 7.8), Convert.ToInt32(Convert.ToDouble(Height / 1.37)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 55.0), Convert.ToInt32(Convert.ToDouble(Height / 1.9)));
            statsWindow8.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 14.0), Convert.ToInt32(Convert.ToDouble(Height / 3.01)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(Width) / 8), Convert.ToInt32(Convert.ToDouble(Height / 8)));
        }

        /// <summary>
        /// Needed by the designer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PokerStarsOverlay_Load(object sender, EventArgs e)
        {

        }
    }
}
