using TaskTracker.Models;

namespace TaskTracker.Repositories;

public interface ITaskRepository
{
    Task<List<DailyTask>> GetTasksForDateAsync(DateTime date);
    Task AddTaskAsync(DailyTask task);
    Task UpdateTaskAsync(DailyTask task);
}