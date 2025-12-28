using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TaskTracker.Models;
using TaskTracker.Repositories;

namespace TaskTracker.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ITaskRepository _taskRepository;

    [ObservableProperty]
    private string newTaskDescription = string.Empty;

    [ObservableProperty]
    private ObservableCollection<DailyTask> tasks = new();

    public MainViewModel(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        LoadTasksCommand.Execute(null); // Загружаем задачи при инициализации
    }

    [RelayCommand]
    public async Task LoadTasksAsync()
    {
        try
        {
            var todayTasks = await _taskRepository.GetTasksForTodayAsync();

            // НЕ заменяем коллекцию целиком, а очищаем и добавляем
            Tasks.Clear();
            foreach (var task in todayTasks)
            {
                Tasks.Add(task);
            }

            // Для надёжности — вызов OnPropertyChanged
            OnPropertyChanged(nameof(Tasks));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTaskDescription))
            return;

        try
        {
            var newTask = new DailyTask
            {
                Description = NewTaskDescription.Trim(),
                IsCompleted = false,
                Date = DateTime.Today
            };

            await _taskRepository.AddTaskAsync(newTask);

            NewTaskDescription = string.Empty;
            await LoadTasksAsync(); // Обновляем список
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось добавить задачу: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task ToggleCompletedAsync(DailyTask task)
    {
        if (task == null)
            return;

        try
        {
            task.IsCompleted = !task.IsCompleted;
            await _taskRepository.UpdateTaskAsync(task);
            await LoadTasksAsync(); // Обновляем список для актуального отображения
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось обновить задачу: {ex.Message}", "OK");
        }
    }
}