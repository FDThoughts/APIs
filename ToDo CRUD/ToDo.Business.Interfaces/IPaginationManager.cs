using System;
using System.Threading.Tasks;

namespace ToDo.Business.Interfaces
{
    /// <summary>
    /// IPagination interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public interface IPaginationManager<T, Y>: IManager<T>
    {
        /// <summary>
        /// Get with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<Y> Get(int pageSize, long pageNumber);
    }
}
