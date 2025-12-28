using TaskTracker.Models;

namespace TaskTracker.Repositories;

public interface ITaskRepository
{
    Task<List<DailyTask>> GetTasksForTodayAsync();
    Task AddTaskAsync(DailyTask task);
    Task UpdateTaskAsync(DailyTask task);
}