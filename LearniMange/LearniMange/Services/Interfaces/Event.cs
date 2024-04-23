namespace learn
{
    class Event : Entry
    {
        // An event in a point in time. It is considered finished if the time of the event has passed
        public Event(string name, DateTime dateTime) : base(name, dateTime)
        {

        }

        public Event() { }

        public override string ToString()
        {
            return $"{Date}: {Name}";
        }
    }
}