using System;
using System.Collections.Generic;

namespace DL770.Rfid
{
    /// <summary>
    /// Класс, хранящий данные об одной сессии чтения
    /// Готов к сериализации в JSON.
    /// </summary>
    public class TubesSession
    {
        public enum DeliveryStatus { Unshipped = 0, Shipped = 1 };
        public enum ReadingStatus { Normal = 0, InterruptedByTimer = 1 };
        public enum Mode { Reading = 0, Registration = 1, Restoration = 2, Recycling = 3 };

        [NonSerialized()]
        public int id;
        public string time;
        public string location = String.Empty;
        [NonSerialized()]
        public DeliveryStatus deliveryStatus = DeliveryStatus.Unshipped;
        public ReadingStatus readingStatus = ReadingStatus.Normal;
        public Mode sessionMode = Mode.Reading;
        public List<string> tags;

        public TubesSession()
        {
            tags = new List<string>();
        }
    }
}
