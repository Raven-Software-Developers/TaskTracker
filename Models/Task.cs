using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTracker.Models
{
    public class DailyTask
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime Date { get; set; }
    }
}
