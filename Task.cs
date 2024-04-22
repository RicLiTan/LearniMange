namespace learn
{
    class Task : Entry
    {
        public int Count { get; set; }
        public int CompletionCount { get; set; }

        public Task(string name, DateTime dateTime, int count, int completionCount) : base(name, dateTime)
        {
            this.Count = count;
            this.CompletionCount = completionCount;
        }

        public override string ToString()
        {
            return $"{Date}: {Name} - {Count} / {CompletionCount}";
        }
    }
}