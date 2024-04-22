using System.Data.SQLite;

namespace learn
{
    class Database
    {
        private SQLiteConnection connection;
        public Database(string filePath)
        {
            connection = new SQLiteConnection($"Data Source={filePath}");
            connection.Open();
            CreateTables();
        }

        private void CreateTables()
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS tasks (
                name TEXT NOT NULL,
                date INTEGER NOT NULL,
                count INTEGER NOT NULL DEFAULT 0,
                completion_count INTEGER NOT NULL DEFAULT 1
            );";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS events (
                name TEXT NOT NULL,
                date INTEGER NOT NULL
            );";
            cmd.ExecuteNonQuery();
        }

        public void AddTask(Task task)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"INSERT INTO tasks VALUES ('{task.Name}', {task.DateToTimestamp()}, {task.Count}, {task.CompletionCount});";
            cmd.ExecuteNonQuery();
        }

        public List<Task> GetTasks(int limit = 4)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"SELECT * FROM tasks ORDER BY date LIMIT {limit};";

            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Task> tasks = new List<Task>();

            while (reader.Read())
            {
                string myreader = reader.GetString(0);
                tasks.Add(new Task(reader.GetString(0), Entry.TimestampToDate(reader.GetInt32(1)), reader.GetInt32(2), reader.GetInt32(3)));
            }
            return tasks;
        }

        public void AddTask(Event e)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"INSERT INTO events VALUES ('{e.Name}', {e.DateToTimestamp()});";
            cmd.ExecuteNonQuery();
        }
        public List<Event> GetEvents(int limit = 4)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"SELECT * FROM events ORDER BY date LIMIT {limit};";

            SQLiteDataReader reader = cmd.ExecuteReader();

            List<Event> events = new List<Event>();

            while (reader.Read())
            {
                events.Add(new Event(reader.GetString(0), Entry.TimestampToDate(reader.GetInt32(1))));
            }
            return events;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}





