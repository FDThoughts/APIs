using System;
using System.Collections.Generic;
using Todo.Business.Test.MockRepositories;
using ToDo.Business;
using ToDo.Entities;
using Xunit;

namespace Todo.Business.Test
{
    /// <summary>
    /// Task manager test
    /// </summary>
    public class TaskManagerTest
    {
        /// <summary>
        /// Task manager
        /// </summary>
        private readonly TaskManager _manager;
        /// <summary>
        /// Mock of Task repository
        /// </summary>
        private MockTaskPagination _mockTaskRepository =
            new MockTaskPagination();

        /// <summary>
        /// Constructor
        /// </summary>
        public TaskManagerTest()
        {
            _manager = new TaskManager(_mockTaskRepository);
        }

        /// <summary>
        /// Get test
        /// </summary>
        [Fact]
        public void GetAllTest()
        {
            var result = _manager.Get().Result as IList<ToDoTask>;

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Get with pagination test
        /// </summary>
        [Fact]
        public void GetAllWithPageTest()
        {
            var result = _manager.Get(1, 1).Result as TaskList;
            Assert.Equal(1, result.TotalCount);
        }

        /// <summary>
        /// Add test
        /// </summary>
        [Fact]
        public void AddTest()
        {
            var result = _manager.Add(new ToDoTask
            {
                Title = "Contact Paul",
                Done = false,
                Category = new Category
                {
                    CategoryId = 1,
                    Name = "General"
                }
            }).Result;

            Assert.True(result);
        }

        /// <summary>
        /// Update test
        /// </summary>
        [Fact]
        public void UpdateTest()
        {
            var result = _manager.Update(2, new ToDoTask
            {
                TaskId = 1,
                Title = "Contact David",
                Done = false,
                Category = new Category
                {
                    CategoryId = 1,
                    Name = "General"
                }
            }).Result;

            Assert.True(result);
        }

        /// <summary>
        /// Delete test
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var result = _manager.Delete(1).Result;

            Assert.True(result);
        }
    }
}
