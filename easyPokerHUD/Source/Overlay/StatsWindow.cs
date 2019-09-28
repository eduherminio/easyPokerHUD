using System;
using System.Drawing;
using System.Windows.Forms;

namespace easyPokerHUD
{
    public partial class StatsWindow : UserControl
    {
        public int SeatNumber { get; set; }

        private readonly Color _defaultColor = Color.GhostWhite;
        private readonly Color _strongPlayerColor = Color.Red;
        private readonly Color _mediumPlayerColor = Color.Orange;
        private readonly Color _weakPlayerColor = Color.Green;

        public StatsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populates the stats window with the proper values
        /// </summary>
        /// <param name="player"></param>
        public void PopulateStatsWindow(Player player)
        {
            PopulateLabels(player);

            //handsplayed.Text = player.name + "  "+ player.seat; //This line is for debugging purposes only
            Username.ForeColor = Color.GhostWhite;

            SetAFqColor();
            SetPFRColor();
            SetVPIPColor();
        }

        /// <summary>
        /// Populates the labels with the player data
        /// </summary>
        /// <param name="player"></param>
        private void PopulateLabels(Player player)
        {
            Username.Text = $"{player.name} ({GetNumberOfHandsPlayedString(player.handsPlayed)})";
            //Set VPIP, PFR, AFq and handsPlayed
            int vpip = player.CalculateVPIP();
            int pfr = player.CalculatePFR();
            int afq = player.CalculateAFq();
            HandsPlayed.Text = GetNumberOfHandsPlayedString(player.handsPlayed);
            VPIP.Text = vpip.ToString();
            AFq.Text = afq.ToString();

            //In case pfr is higher than vpip, vpip will be shown instead of pfr
            PFR.Text = pfr > vpip
                ? vpip.ToString()
                : pfr.ToString();
        }

        /// <summary>
        /// Adjusts the displaying of the number of hands played according to size
        /// </summary>
        /// <param name="numberOfHandsPlayed"></param>
        /// <returns></returns>
        private string GetNumberOfHandsPlayedString(int numberOfHandsPlayed)
        {
            if (numberOfHandsPlayed >= 1000 && numberOfHandsPlayed < 1_000_000)
            {
                return ((float)numberOfHandsPlayed / 1000).ToString("N1") + "k";
            }
            else if (numberOfHandsPlayed >= 1_000_000)
            {
                return ((float)numberOfHandsPlayed / 1_000_000).ToString("N1") + "m";
            }
            else
            {
                return numberOfHandsPlayed.ToString();
            }
        }

        /// <summary>
        /// Sets the color of the VPIP stat
        /// </summary>
        private void SetVPIPColor()
        {
            int vpip = Convert.ToInt16(VPIP.Text);
            if (vpip > 30)
            {
                VPIP.ForeColor = _weakPlayerColor;
            }
            else if (vpip > 18)
            {
                VPIP.ForeColor = _mediumPlayerColor;
            }
            else if (vpip > 1)
            {
                VPIP.ForeColor = _strongPlayerColor;
            }
            else
            {
                VPIP.ForeColor = _defaultColor;
            }
        }

        /// <summary>
        /// Sets the color of the PFR stat
        /// </summary>
        private void SetPFRColor()
        {
            int pfr = Convert.ToInt16(PFR.Text);
            int vpip = Convert.ToInt16(VPIP.Text);

            if (pfr >= vpip * 0.8)
            {
                PFR.ForeColor = _strongPlayerColor;
            }
            else if (pfr > vpip * 0.5)
            {
                PFR.ForeColor = _mediumPlayerColor;
            }
            else if (pfr > 0)
            {
                PFR.ForeColor = _weakPlayerColor;
            }
            else
            {
                PFR.ForeColor = _defaultColor;
            }
        }

        /// <summary>
        /// Sets the color of the AF stat
        /// </summary>
        private void SetAFqColor()
        {
            int afq = Convert.ToInt16(AFq.Text);
            if (afq > 39)
            {
                AFq.ForeColor = _strongPlayerColor;
            }
            else if (afq > 24)
            {
                AFq.ForeColor = _mediumPlayerColor;
            }
            else if (afq > 1)
            {
                AFq.ForeColor = _weakPlayerColor;
            }
            else
            {
                AFq.ForeColor = _defaultColor;
            }
        }

        private void StatsWindow_SizeChanged(object sender, EventArgs e)
        {
            AutoScaleMode = AutoScaleMode.Dpi;

            while (Username.Width < TextRenderer.MeasureText(Username.Text, new Font(Username.Font.FontFamily, Username.Font.Size, Username.Font.Style)).Width)
            {
                Username.Font = new Font(Username.Font.FontFamily, Username.Font.Size - 0.5f, Username.Font.Style);
            }
        }
    }
}
