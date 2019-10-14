using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;
using ToDo.Repository.Interfaces;

namespace Todo.Business.Test.MockRepositories
{
    /// <summary>
    /// Mock Category Repository
    /// </summary>
    public class MockCategoryRepository: IRepository<Category>
    {
        /// <summary>
        /// Mock Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> Get()
        {
            IList<Category> list = new List<Category>();
            list.Add(new Category
            {
                CategoryId = 1,
                Name = "General"
            });
            return list;
        }

        /// <summary>
        /// Mock Add
        /// </summary>
        /// <param name="category"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Add(Category category)
        {
            return true;
        }

        /// <summary>
        /// Mock Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, Category category)
        {
            return true;
        }

        /// <summary>
        /// Mock Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            return true;
        }
    }
}
