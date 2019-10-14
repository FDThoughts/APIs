using System;
using System.Collections.Generic;
using ToDo.Entities;
using ToDo.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ToDo.Repository
{
    /// <summary>
    /// Category repository
    /// </summary>
    public class InMemoryCategoryRepository : InMemoryRepository, 
        IRepository<Category>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public InMemoryCategoryRepository(IConfiguration config):
            base(config)
        {
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> Get()
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    var keys = objRedisClient.GetAllKeys()
                        .FindAll(k => k.StartsWith("Category_") &&
                            k != "Category_CategoryCount");
                    if (keys.Count == 0)
                    {
                        Category category = new Category()
                        {
                            CategoryId = 1,
                            Name = "General"
                        };
                        objRedisClient.Set<Category>(
                            $"Category_{category.CategoryId}",
                            category);
                        objRedisClient.Set<int>(
                            "Category_CategoryCount",
                            1);
                        keys = objRedisClient.GetAllKeys()
                        .FindAll(k => k.StartsWith("Category_") &&
                            k != "Category_CategoryCount");
                    }
                    List<Category> categories = new List<Category>();
                    keys.ForEach(k =>
                    {
                        categories.Add(objRedisClient.Get<Category>(k));
                    });
                    return categories;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Add(Category category)
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    if (objRedisClient.Get<Category>(
                        $"Category_{category.CategoryId}") == null)
                    {
                        int CategoryCount = -1;
                        if (objRedisClient.Exists(
                            "Category_CategoryCount") == 1)
                        {
                            CategoryCount = objRedisClient.Get<int>(
                                "Category_CategoryCount");
                        }
                        else
                        {
                            Category defaultCategory = new Category()
                            {
                                CategoryId = 1,
                                Name = "General"
                            };
                            objRedisClient.Set<Category>(
                                $"Category_{defaultCategory.CategoryId}",
                                defaultCategory);
                            CategoryCount = 1;
                            objRedisClient.Set<int>(
                                "Category_CategoryCount",
                                CategoryCount);
                        }
                        category.CategoryId = CategoryCount++;
                        objRedisClient.Set<int>(
                                "Category_CategoryCount",
                                CategoryCount);
                        return objRedisClient.Set<Category>(
                            $"Category_{category.CategoryId}",
                            category);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, Category category)
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    if (id != 1 && objRedisClient.Get<Category>(
                        $"Category_{id}") != null)
                    {
                        category.CategoryId = id;
                        return objRedisClient.Set<Category>(
                            $"Category_{id}",
                            category);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(long id)
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    if (id != 1 && objRedisClient.Get<Category>(
                        $"Category_{id}") != null)
                    {
                        objRedisClient.Remove($"Category_{id}");
                    }
                    else
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
