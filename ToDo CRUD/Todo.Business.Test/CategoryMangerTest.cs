using System;
using System.Collections.Generic;
using Todo.Business.Test.MockRepositories;
using ToDo.Business;
using ToDo.Entities;
using Xunit;

namespace Todo.Business.Test
{
    /// <summary>
    /// Category manager test
    /// </summary>
    public class CategoryManagerTest
    {
        /// <summary>
        /// Category manager
        /// </summary>
        private readonly CategoryManager _manager;
        /// <summary>
        /// Mock of Category repository
        /// </summary>
        private MockCategoryRepository _mockCategoryRepository = 
            new MockCategoryRepository();

        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryManagerTest()
        {
            _manager = new CategoryManager(_mockCategoryRepository);
        }

        /// <summary>
        /// Get test
        /// </summary>
        [Fact]
        public void GetAllTest()
        {
            var result = _manager.Get().Result as IList<Category>;

            Assert.Equal(1, result.Count);
        }

        /// <summary>
        /// Add test
        /// </summary>
        [Fact]
        public void AddTest()
        {
            var result = _manager.Add(new Category
            {
                Name = "daily"
            }).Result;

            Assert.True(result);
        }

        /// <summary>
        /// Update test
        /// </summary>
        [Fact]
        public void UpdateTest()
        {
            var result = _manager.Update(2, new Category
            {
                Name = "Weekly"
            }).Result;

            Assert.True(result);
        }

        /// <summary>
        /// Delete test
        /// </summary>
        [Fact]
        public void DeleteTest()
        {
            var result = _manager.Delete(2).Result;

            Assert.True(result);
        }
    }
}
