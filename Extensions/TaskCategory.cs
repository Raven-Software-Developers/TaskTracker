using System.ComponentModel;
using System.Reflection;
using TaskTracker.Models;

namespace TaskTracker.Extensions;

public static class TaskCategoryExtensions
{
    public static string GetDisplayName(this TaskCategory category)
    {
        var field = category.GetType().GetField(category.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? category.ToString();
    }
}