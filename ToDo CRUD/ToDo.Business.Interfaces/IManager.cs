using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDo.Business.Interfaces
{
    /// <summary>
    /// IManager interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IManager<T>
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
