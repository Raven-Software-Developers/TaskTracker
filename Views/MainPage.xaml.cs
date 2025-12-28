using TaskTracker.Repositories;
using TaskTracker.ViewModels;

namespace TaskTracker.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel(new MySqlTaskRepository());
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
    {
        // Не обязательно, но можно что-то сделать дополнительно
    }
}