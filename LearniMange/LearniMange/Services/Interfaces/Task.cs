namespace learn
{
    class LearnTask : Entry
    {

        // Task that has a count and completion count to determine if it is finished
        public int Count { get; set; }
        public int CompletionCount { get; set; }

        public LearnTask(string name, DateTime dateTime, int count, int completionCount) : base(name, dateTime)
        {
            this.Count = count;
            if (completionCount < 1)
            {
                throw new ArgumentException("Completion count must be greater than 0");
            }
            this.CompletionCount = completionCount;
        }

        public LearnTask() {}

        public override string ToString()
        {
            return $"{Date}: {Name} - {Count} / {CompletionCount}";
        }
    }
}