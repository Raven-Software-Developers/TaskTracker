using TaskTracker.Repositories;
using TaskTracker.ViewModels;

namespace TaskTracker.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _viewModel;

    public MainPage()
    {
        InitializeComponent();
        _viewModel = new MainViewModel(new MySqlTaskRepository());
        BindingContext = _viewModel;
    }

    private async void OnAppeared(object sender, EventArgs e)
    {
        await _viewModel.LoadTasksAsync();  // Принудительная загрузка при открытии
    }
}