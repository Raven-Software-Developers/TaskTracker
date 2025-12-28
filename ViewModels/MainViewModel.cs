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
    private DateTime selectedDate = DateTime.Today;

    [ObservableProperty]
    private string pageTitle = "Задачи на сегодня";

    [ObservableProperty]
    private ObservableCollection<DailyTask> tasks = new();

    public MainViewModel(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
        SelectedDate = DateTime.Today;
        LoadTasksCommand.Execute(null); 
    }

    partial void OnSelectedDateChanged(DateTime value)
    {
        UpdatePageTitle();
        LoadTasksCommand.Execute(null); 
    }

    private void UpdatePageTitle()
    {
        PageTitle = $"Задачи на {SelectedDate:dd MMMM yyyy}";
    }

    [RelayCommand]
    private async Task LoadTasksAsync()
    {
        try
        {
            var dateTasks = await _taskRepository.GetTasksForDateAsync(SelectedDate);
            Tasks.Clear();
            foreach (var task in dateTasks)
                Tasks.Add(task);
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task AddTaskAsync()
    {
        if (string.IsNullOrWhiteSpace(NewTaskDescription)) return;

        try
        {
            var newTask = new DailyTask
            {
                Description = NewTaskDescription.Trim(),
                IsCompleted = false,
                Date = SelectedDate 
            };

            await _taskRepository.AddTaskAsync(newTask);
            NewTaskDescription = string.Empty;
            await LoadTasksAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private async Task ToggleCompletedAsync(DailyTask task)
    {
        if (task == null) return;

        try
        {
            task.IsCompleted = !task.IsCompleted;
            await _taskRepository.UpdateTaskAsync(task);
            await LoadTasksAsync();
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Ошибка", ex.Message, "OK");
        }
    }

    [RelayCommand]
    private void SetToday()
    {
        SelectedDate = DateTime.Today;
    }

    [RelayCommand]
    private void SetYesterday()
    {
        SelectedDate = DateTime.Today.AddDays(-1);
    }

    [RelayCommand]
    private void SetTomorrow()
    {
        SelectedDate = DateTime.Today.AddDays(1);
    }
}