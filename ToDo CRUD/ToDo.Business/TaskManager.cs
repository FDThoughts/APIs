using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities;
using ToDo.Repository.Interfaces;

namespace ToDo.Business
{
    /// <summary>
    /// Task manager
    /// </summary>
    public class TaskManager : IPaginationManager<ToDoTask, TaskList>
    {
        /// <summary>
        /// Task Repository
        /// </summary>
        IPaginationRepository<ToDoTask, TaskList> _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public TaskManager(IPaginationRepository<ToDoTask, TaskList> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoTask>> Get()
        {
            return await _repository.Get();
        }

        /// <summary>
        /// Get with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<TaskList> Get(int pageSize, long pageNumber)
        {
            return await _repository.Get(pageSize, pageNumber);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Add(ToDoTask task)
        {
            return await _repository.Add(task);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, ToDoTask task)
        {
            return await _repository.Update(id, task);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            return await _repository.Delete(id);
        }
    }
}
