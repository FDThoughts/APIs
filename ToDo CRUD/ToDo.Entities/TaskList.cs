using System;
using System.Collections.Generic;

namespace ToDo.Entities
{
    /// <summary>
    /// Task list model for pagination
    /// </summary>
    public class TaskList
    {
        /// <summary>
        /// Tasks list
        /// </summary>
        public List<ToDoTask> Tasks { get; set; }

        /// <summary>
        /// Total count of tasks
        /// </summary>
        public long TotalCount { get; set; }
    }
}
