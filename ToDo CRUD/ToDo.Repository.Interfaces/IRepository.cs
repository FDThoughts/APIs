using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Repository.Interfaces
{
    /// <summary>
    /// IRepository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> Get();

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Add(T entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Update(long id, T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(long id);
    }
}
