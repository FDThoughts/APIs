using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Entities;
using ToDo.Repository.Interfaces;

namespace Todo.Business.Test.MockRepositories
{
    /// <summary>
    /// Mock Task Repository
    /// </summary>
    public class MockTaskPagination: IPaginationRepository<ToDoTask, TaskList>
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoTask>> Get()
        {
            IList<ToDoTask> list = new List<ToDoTask>();
            list.Add(new ToDoTask
            {
                TaskId = 1,
                Title = "Contact Paul",
                Done = false,
                Category = new Category
                {
                    CategoryId = 1,
                    Name = "General"
                }
            });
            return list;
        }

        /// <summary>
        /// Get with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<TaskList> Get(int pageSize, long pageNumber)
        {
            List<ToDoTask> list = new List<ToDoTask>();
            list.Add(new ToDoTask
            {
                TaskId = 1,
                Title = "Contact Paul",
                Done = false,
                Category = new Category
                {
                    CategoryId = 1,
                    Name = "General"
                }
            });
            return new TaskList
            {
                Tasks = list,
                TotalCount = 1
            };
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Add(ToDoTask task)
        {
            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, ToDoTask task)
        {
            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            return true;
        }
    }
}
