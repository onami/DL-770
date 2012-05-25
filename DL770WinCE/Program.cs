using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace DL770
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}