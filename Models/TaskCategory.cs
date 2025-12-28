using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TaskTracker.Models
{

    public enum TaskCategory
    {
        [Description("Без категории")]
        None = 0,
        [Description("Работа")]
        Work,
        [Description("Личные")]
        Personal,
        [Description("Покупки")]
        Shopping,
        [Description("Здоровье")]
        Health,
        [Description("Образование")]
        Education,
        [Description("Дом")]
        Home
    }
}
