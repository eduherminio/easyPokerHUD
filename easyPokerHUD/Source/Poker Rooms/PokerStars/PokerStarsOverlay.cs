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
            if (getTableWindowName() == "")
            {
                PokerStarsMain.overlays.TryRemove(tableName, out tableName);
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
            PokerStarsHand hand;
            if (PokerStarsMain.newHandsToBeFetched.TryRemove(tableName, out hand))
            {
                hand.players = preparePlayerListForCorrectPositioning(hand.players);
                populateStatsWindows(hand.players);
            }
        }

        //Updates the size of the controls for the according table size
        protected void setPositionOfStatsWindowsWindowsAccordingToTableSize()
        {
            if (tableSize == 2)
            {
                positionControlsHeadsUp();
            } else if (tableSize == 3)
            {
                positionControls3Max();
            }
            else if (tableSize == 6)
            {
                positionControls6Max();
            }
            else if (tableSize == 9)
            {
                positionControls9Max();
            } 
        }

        //Positions the controls relative to the window size for HeadsUp
        private void positionControlsHeadsUp()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.4), Convert.ToInt32(Convert.ToDouble(this.Height / 1.25)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.65), Convert.ToInt32(Convert.ToDouble(this.Height / 6.5)));
        }

        //Positions the controls relative to the window size for 3max
        private void positionControls3Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.25), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.4), Convert.ToInt32(Convert.ToDouble(this.Height / 1.25)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 25.0), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
        }

        //Positions the controls relative to the window size for 6Max
        private void positionControls6Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.25), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.25), Convert.ToInt32(Convert.ToDouble(this.Height / 1.58)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.4), Convert.ToInt32(Convert.ToDouble(this.Height / 1.25)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 25.0), Convert.ToInt32(Convert.ToDouble(this.Height / 1.58)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 25.0), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.65), Convert.ToInt32(Convert.ToDouble(this.Height / 6.5)));
        }

        //Positions the controls relative to the window size for 9Max
        private void positionControls9Max()
        {
            statsWindow1.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.34), Convert.ToInt32(Convert.ToDouble(this.Height / 8)));
            statsWindow2.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.26), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
            statsWindow3.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.2), Convert.ToInt32(Convert.ToDouble(this.Height / 1.9)));
            statsWindow4.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 1.4), Convert.ToInt32(Convert.ToDouble(this.Height / 1.37)));
            statsWindow5.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 2.4), Convert.ToInt32(Convert.ToDouble(this.Height / 1.25)));
            statsWindow6.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 7.8), Convert.ToInt32(Convert.ToDouble(this.Height / 1.37)));
            statsWindow7.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 55.0), Convert.ToInt32(Convert.ToDouble(this.Height / 1.9)));
            statsWindow8.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 14.0), Convert.ToInt32(Convert.ToDouble(this.Height / 3.01)));
            statsWindow9.Location = new Point(Convert.ToInt32(Convert.ToDouble(this.Width) / 8), Convert.ToInt32(Convert.ToDouble(this.Height / 8)));
        }

        //Needed by the designer
        private void PokerStarsOverlay_Load(object sender, EventArgs e)
        {

        }
    }
}
