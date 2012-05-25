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

        public static int GetSize()
        {
            return Marshal.SizeOf(new TubesBundle());
        }
    }

    public class TubesBundleSession
    {
        public enum DeliveryStatus { Unshipped = 0, Shipped = 1 };
        public enum Mode { Reading = 0, Writing = 1 };

        [NonSerialized]
        public Mode sessionMode;
        [NonSerialized]
        public DeliveryStatus deliveryStatus = DeliveryStatus.Unshipped;

        public TubesBundle bundle;
    }

}
