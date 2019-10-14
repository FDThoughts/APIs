using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Business.Interfaces;
using ToDo.Entities;
using ToDo.Repository.Interfaces;

namespace ToDo.Business
{
    /// <summary>
    /// Category Manager
    /// </summary>
    public class CategoryManager : IManager<Category>
    {
        /// <summary>
        /// Category repository
        /// </summary>
        IRepository<Category> _repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public CategoryManager(IRepository<Category> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> Get()
        {
            return await _repository.Get();
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Add(Category category)
        {
            return await _repository.Add(category);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, Category category)
        {
            return await _repository.Update(id, category);
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
