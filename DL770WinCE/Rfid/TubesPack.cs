using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DL770.Rfid
{
    public struct TubesBundle
    {
        public int time;
        public ushort disrictId; //wellId нечитабельно
        public ushort tubesLength;

        public UInt64 test1;
        public UInt64 test2;
        public UInt64 test3;
        public UInt64 test4;
        public UInt64 test5;

        public static int GetSize()
        {
            return Marshal.SizeOf(new TubesBundle());
        }
    }
}
