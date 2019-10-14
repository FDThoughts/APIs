using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Repository.Interfaces
{
    /// <summary>
    /// IPagination interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Y"></typeparam>
    public interface IPaginationRepository<T, Y>: IRepository<T>
    {
        /// <summary>
        /// get with pagination
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<Y> Get(int pageSize, long pageNumber);
    }
}
