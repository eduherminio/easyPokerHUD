﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace easyPokerHUD
{
    internal class PokerRoomOverlay : OverlayFrame
    {
        protected string playerName;
        protected int tableSize;
        protected System.Windows.Forms.Timer controlSizeUpdateTimer = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer statsFetcherTimer = new System.Windows.Forms.Timer();
        protected int oldHeight;
        protected int oldWidth;
        protected List<StatsWindow> statsWindowList = new List<StatsWindow>();

        protected StatsWindow statsWindow1 = new StatsWindow();
        protected StatsWindow statsWindow2 = new StatsWindow();
        protected StatsWindow statsWindow3 = new StatsWindow();
        protected StatsWindow statsWindow4 = new StatsWindow();
        protected StatsWindow statsWindow5 = new StatsWindow();
        protected StatsWindow statsWindow6 = new StatsWindow();
        protected StatsWindow statsWindow7 = new StatsWindow();
        protected StatsWindow statsWindow8 = new StatsWindow();
        protected StatsWindow statsWindow9 = new StatsWindow();
        protected StatsWindow statsWindow10 = new StatsWindow();
        protected StatsWindow tableStatsWindow = new StatsWindow();

        // These are needed for the table overview
        protected int handsPlayedOnThisTable;
        protected int tableHandsPlayed;
        protected int tablePreflopCalls;
        protected int tablePreflopBetsAndRaises;
        protected int tablePostflopBetsAndRaises;
        protected int tablePostflopCallsChecksAndFolds;

        /// <summary>
        /// Is needed by the get scaling factor method
        /// </summary>
        /// <param name="hdc"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
            LOGPIXELSY = 90,
        }

        protected void PrepareOverlay(string pTableName, int pTableSize, string pPlayerName)
        {
            //Set the basic functions
            ShowInTaskbar = false;
            tableName = pTableName;
            tableSize = pTableSize;
            playerName = pPlayerName;
            SetOwner();

            //Initialize the position of the overlay
            GetWindowRect(handle, out rect);
            SetTransparency();
            SetSize();
            SetPosition();

            //Initialize the form
            StartOverlayFrameUpdateTimer();

            //Setup the stats windows
            SetupStatsWindows();
            SetStatsWindowsFontSize();

            //Setup the timers
            statsFetcherTimer.Interval = 500;
            statsFetcherTimer.Enabled = true;

            controlSizeUpdateTimer.Interval = 250;
            controlSizeUpdateTimer.Enabled = true;
        }

        /// <summary>
        /// Sets the stats Windows up
        /// </summary>
        protected void SetupStatsWindows()
        {
            //Set the seat numbers
            statsWindow1.SeatNumber = 1;
            statsWindow2.SeatNumber = 2;
            statsWindow3.SeatNumber = 3;
            statsWindow4.SeatNumber = 4;
            statsWindow5.SeatNumber = 5;
            statsWindow6.SeatNumber = 6;
            statsWindow7.SeatNumber = 7;
            statsWindow8.SeatNumber = 8;
            statsWindow9.SeatNumber = 9;
            statsWindow10.SeatNumber = 10;

            //Add all the statswindows to the list
            statsWindowList.Add(statsWindow1);
            statsWindowList.Add(statsWindow2);
            statsWindowList.Add(statsWindow3);
            statsWindowList.Add(statsWindow4);
            statsWindowList.Add(statsWindow5);
            statsWindowList.Add(statsWindow6);
            statsWindowList.Add(statsWindow7);
            statsWindowList.Add(statsWindow8);
            statsWindowList.Add(statsWindow9);
            statsWindowList.Add(statsWindow10);

            //Add all the statsWindows to the overlay
            foreach (StatsWindow statsWindow in statsWindowList)
            {
                Controls.Add(statsWindow);
                statsWindow.Visible = false;
                statsWindow.TableLayoutPanel.AutoSize = true;
            }
        }

        /// <summary>
        /// Adjusts the font size according to the window size
        /// </summary>
        protected void SetStatsWindowsFontSize()
        {
            float fontSize = Width / 99 / GetScalingFactor();
            float fontSizeForWindow = (float)Width / 120;

            using (FontFamily fontFamily = new FontFamily("Arial"))
            {
                foreach (StatsWindow statsWindow in statsWindowList)
                {
                    statsWindow.Font = new Font(fontFamily, fontSizeForWindow);
                    Font fontForTheStats = new Font(fontFamily, fontSize, FontStyle.Bold);

                    statsWindow.VPIP.Font = fontForTheStats;
                    statsWindow.PFR.Font = fontForTheStats;
                    statsWindow.AFq.Font = fontForTheStats;
                    statsWindow.Username.Font = fontForTheStats;
                }
            }
        }

        /// <summary>
        /// Gets the scaling factor of the current dpi settings
        /// </summary>
        /// <returns></returns>
        protected float GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            int logpixelsy = GetDeviceCaps(desktop, (int)DeviceCap.LOGPIXELSY);
            float screenScalingFactor = (float)PhysicalScreenHeight / LogicalScreenHeight;
            float dpiScalingFactor = (float)logpixelsy / 96;

            return dpiScalingFactor; // 1.25 = 125%
            //return screenScalingFactor;
        }

        /// <summary>
        /// Prepares the player seat numbers according to the size of the table
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        protected List<Player> PreparePlayerListForCorrectPositioning(List<Player> players)
        {
            while (!players.Find(p => p.name.Equals(playerName)).seat.Equals(tableSize - (tableSize / 2)))
            {
                foreach (Player player in players)
                {
                    if (player.seat == tableSize)
                    {
                        player.seat = 0;
                    }
                    player.seat++;
                }
            }
            return players;
        }

        /// <summary>
        /// Populates the controls with the player infos
        /// </summary>
        /// <param name="players"></param>
        protected void PopulateStatsWindows(List<Player> players)
        {
            foreach (StatsWindow statsWindow in statsWindowList)
            {
                try
                {
                    var playerForThisSeat = players.Single(p => p.seat == statsWindow.SeatNumber);
                    statsWindow.PopulateStatsWindow(playerForThisSeat);
                    statsWindow.Visible = true;
                }
                catch (Exception)
                {
                    Console.Out.WriteLine("Hiding HUD for Seat " + statsWindow.SeatNumber);
                    statsWindow.Visible = false;
                }
            }
        }
    }
}
