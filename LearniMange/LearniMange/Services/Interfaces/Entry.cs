using System;

namespace learn
{
    abstract class Entry
    {
        // Top level class that the entities are created upon
        public DateTime Date { get; set; }
        public string Name { get; set; }

        public Entry(string name, DateTime dateTime)
        {
            this.Date = dateTime;
            this.Name = name;
        }

        public Entry() { }

        public abstract string ToString();

        public int DateToTimestamp()
        {
            return (int)((DateTimeOffset)this.Date).ToUnixTimeSeconds();
        }

        public static DateTime TimestampToDate(int timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
            return dateTime;
        }
    }
}
















