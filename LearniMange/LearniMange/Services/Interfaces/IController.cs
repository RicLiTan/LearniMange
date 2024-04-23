using learn;
interface IController
{
    // Interface for the front end and back end to communicate
    double TaskCompletedPercentage();

    List<LearnTask> GetTasks();

    void AddTask(LearnTask t);

    int NoOfTasks();

    bool AreTasksEmpty();

    double EventsCompletedPercentage();

    List<Event> GetEvents();

    void AddEvent(Event e);

    int NoOfEvents();

    bool AreEventsEmpty();

    List<LearnTask> GetFinishedTasks();

    List<LearnTask> GetLateTasks();

    List<Event> GetFinishedEvents();
}