
using SQLite;

namespace learn
{
    static class Constants
    {
        // Flags to configure the SQL connection

        public const string DatabaseFilename = "learn.db";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    }


    class LearnDatabase : IController
    {
        SQLiteConnection db;
        public LearnDatabase(string filePath)
        {
            // Establishes the connection and creates the tables

            db = new SQLiteConnection(filePath, Constants.Flags);
            db.CreateTable<LearnTask>();
            db.CreateTable<Event>();
        }

        // Tasks

        public void AddTask(LearnTask t)
        {
            db.Insert(t);
        }

        public double TaskCompletedPercentage()
        {
            return GetTasks().Count;
        }

        public List<LearnTask> GetTasks()
        {
            return db.Query<LearnTask>($@"SELECT * FROM LearnTask ORDER BY Date;");
        }
        public double TasksCompletedPercentage()
        {
            // Calculates the percentage of completed tasks relative to all tasks

            List<LearnTask> tasks = GetTasks();
            int maxCount = tasks.Count;
            int completeedCount = 0;
            DateTime now = DateTime.Now;
            foreach (LearnTask t in tasks)
            {
                // Less than zero - t1 is earlier than t2.
                // Greater than zero - t1 is later than t2.
                if (t.Count == t.CompletionCount)
                {
                    completeedCount++;
                }
            }
            return (double)completeedCount / (double)maxCount * 100;
        }
        public int NoOfTasks()
        {
            return GetTasks().Count;
        }

        public bool AreTasksEmpty()
        {
            if (GetTasks().Count == 0)
            {
                return true;
            }
            return false;
        }

        public List<LearnTask> GetLateTasks()
        {
            // Gets all late tasks. Late tasks are tasks with time earlier than now

            List<LearnTask> l = GetTasks();
            List<LearnTask> lateTasks = new List<LearnTask>();
            foreach (LearnTask t in l)
            {
                if (DateTime.Compare(DateTime.Now, t.Date) > 0)
                {
                    lateTasks.Add(t);
                }
            }
            return lateTasks;
        }

        public List<LearnTask> GetFinishedTasks()
        {
            // Gets all finished tasks. Finished tasks are tasks that have count == completion count

            List<LearnTask> l = GetTasks();
            List<LearnTask> finishedTasks = new List<LearnTask>();
            foreach (LearnTask t in l)
            {
                if (t.Count == t.CompletionCount)
                {
                    finishedTasks.Add(t);
                }
            }
            return finishedTasks;
        }

        // Events

        public void AddEvent(Event e)
        {
            db.Insert(e);
        }
        public List<Event> GetEvents()
        {
            return db.Query<Event>($@"SELECT * FROM Event ORDER BY Date;");

        }
        public double EventsCompletedPercentage()
        {
            // Calculates the percentage of completed events with all events

            List<Event> events = GetEvents();
            int maxCount = events.Count;
            int pastCount = 0;
            DateTime now = DateTime.Now;
            foreach (Event e in events)
            {
                // Less than zero - t1 is earlier than t2.
                // Greater than zero - t1 is later than t2.
                if (DateTime.Compare(now, e.Date) > 0)
                {
                    pastCount++;
                }
            }
            return (double)pastCount / (double)maxCount * 100;
        }

        public int NoOfEvents()
        {
            return GetEvents().Count;
        }
        public bool AreEventsEmpty()
        {
            if (GetTasks().Count == 0)
            {
                return true;
            }
            return false;
        }
        public List<Event> GetFinishedEvents()
        {
            // Get all events that are finished

            List<Event> l = GetEvents();
            List<Event> finishedEvents = new List<Event>();
            foreach (Event t in l)
            {
                if (DateTime.Compare(DateTime.Now, t.Date) > 0)
                {
                    finishedEvents.Add(t);
                }
            }
            return finishedEvents;
        }
    }

}





