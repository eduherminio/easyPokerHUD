using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Win32Interop.WinHandles;
using System.Threading;

namespace easyPokerHUD
{
    public partial class OverlayFrame : Form
    {
        protected RECT rect;
        protected RECT oldrect = new RECT();
        protected IntPtr handle;
        protected string tableName;
        protected string tableWindowName;
        protected System.Windows.Forms.Timer overlayFrameUpdateTimer = new System.Windows.Forms.Timer();

        //Structure needed for defining a window size
        protected struct RECT
        {
            public int left, top, right, bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        protected static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        protected static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        protected static extern int SetWindowLong(HandleRef hWnd, int nIndex, HandleRef dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        protected static extern IntPtr FindWindow(string lp1, string lp2);

        [DllImport("user32.dll", SetLastError = true)]
        protected static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        //Starts the update timer for the overlay
        protected void startOverlayFrameUpdateTimer()
        {
            overlayFrameUpdateTimer.Interval = 250;
            overlayFrameUpdateTimer.Tick += new EventHandler(updateOverlayFrame);
            overlayFrameUpdateTimer.Enabled = true;
        }

        //Updates the size, transparency and position of the form
        protected void updateOverlayFrame(Object obj, EventArgs eve)
        {
            GetWindowRect(handle, out rect);
            if (!rect.Equals(oldrect))
            {
                oldrect = rect;
                setTransparency();
                setSize();
                setPosition();
            }
        }

        //Makes the form window transparent
        protected void setTransparency()
        {
            this.BackColor = Color.White;
            this.TransparencyKey = Color.White;
            this.FormBorderStyle = FormBorderStyle.None;
            int initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }

        //Resizes the form to the size of the table-window
        protected void setSize()
        {
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;
        }

        //Positions the form above the table-window
        protected void setPosition()
        {
            if (rect.left == -32000)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Location = new Point(rect.left, rect.top);
            }
        }

        //Sets the PokerStars table window as the owner of this window
        protected void SetOwner()
        {
            if (getTableWindowName() == null)
            {
                Close();
                Thread.CurrentThread.Abort();
            }
            handle = FindWindow(null, getTableWindowName());
            Win32WindowWrapper wrapper = new Win32WindowWrapper(handle);
            SetWindowLong(new HandleRef(this, this.Handle), -8, new HandleRef(wrapper, handle));
        }

        //Gets the window name of specified table
        protected string getTableWindowName()
        {
            try
            {
                var window = TopLevelWindowUtils.FindWindow(wh => wh.GetWindowText().Contains(tableName) && !wh.GetWindowText().Contains("Lobby"));
                string tableWindowName = window.GetWindowText();
                return tableWindowName;
            } catch
            {
                return null;
            }
        }
    }
}