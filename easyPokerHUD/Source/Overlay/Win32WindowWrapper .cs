using System;
using System.Windows.Forms;

namespace easyPokerHUD
{
    //Extends my handle, so I can use additional functions
    public class Win32WindowWrapper : IWin32Window
    {
            private IntPtr handle;

            public Win32WindowWrapper(IntPtr handle)
            {
                this.handle = handle;
            }

            public IntPtr Handle
            {
                get { return handle; }
            }
    }
}
