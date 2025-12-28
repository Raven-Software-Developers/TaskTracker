using MySqlConnector;
using TaskTracker.Models;

namespace TaskTracker.Repositories;

public class MySqlTaskRepository : ITaskRepository
{
    private const string ConnectionString = "Server=127.0.0.1;Port=3306;Database=task_tracker;User Id=root;Password=root";

    public async Task<List<DailyTask>> GetTasksForDateAsync(DateTime date)
    {
        var tasks = new List<DailyTask>();
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        using var command = new MySqlCommand("SELECT * FROM Tasks WHERE Date = @Date ORDER BY Id;", connection);
        command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));

        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            tasks.Add(new DailyTask
            {
                Id = reader.GetInt32("Id"),
                Description = reader.GetString("Description"),
                IsCompleted = reader.GetBoolean("IsCompleted"),
                Date = reader.GetDateTime("Date")
            });
        }

        return tasks;
    }

    public async Task AddTaskAsync(DailyTask task)
    {
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        using var command = new MySqlCommand(
            "INSERT INTO Tasks (Description, IsCompleted, Date) VALUES (@Description, @IsCompleted, @Date);",
            connection);

        command.Parameters.AddWithValue("@Description", task.Description);
        command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
        command.Parameters.AddWithValue("@Date", task.Date.ToString("yyyy-MM-dd"));

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateTaskAsync(DailyTask task)
    {
        using var connection = new MySqlConnection(ConnectionString);
        await connection.OpenAsync();

        using var command = new MySqlCommand(
            "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id;",
            connection);

        command.Parameters.AddWithValue("@IsCompleted", task.IsCompleted);
        command.Parameters.AddWithValue("@Id", task.Id);

        await command.ExecuteNonQueryAsync();
    }
}