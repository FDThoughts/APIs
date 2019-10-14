using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo.Entities
{
    /// <summary>
    /// Task status type: DueDate passed, Due today, Due later, Done
    /// </summary>
    public enum StatusType
    {
        DueDatePassed,
        DueToday,
        DueSoon,
        Done
    }
}
