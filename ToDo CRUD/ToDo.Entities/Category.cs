using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Entities
{
    /// <summary>
    /// Category class
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Category id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
