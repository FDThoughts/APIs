using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities
{
    /// <summary>
    /// Task class
    /// </summary>
    public class ToDoTask
    {
        /// <summary>
        /// Task id
        /// </summary>
        public long TaskId { get; set; }

        /// <summary>
        /// Task title
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// Task description
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Task due date
        /// </summary>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Task done
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// Task category
        /// </summary>
        [Required]
        public Category Category { get; set; }

        /// <summary>
        /// Task status
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Set task status based on Done and Due Date
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public void SetStatus()
        {
            try
            {
                if (Done)
                {
                    Status = (int)StatusType.Done;
                }
                else if (DueDate == null)
                {
                    Status = (int)StatusType.DueSoon;
                }
                else if (DueDate.Value.Day > DateTime.Now.Day)
                {
                    Status = (int)StatusType.DueSoon;
                }
                else if (DueDate.Value.Day < DateTime.Now.Day)
                {
                    Status = (int)StatusType.DueDatePassed;
                }
                else
                {
                    Status = (int)StatusType.DueToday;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
