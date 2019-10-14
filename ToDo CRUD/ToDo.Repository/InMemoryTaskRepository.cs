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
    /// Task repository
    /// </summary>
    public class InMemoryTaskRepository : InMemoryRepository, 
        IPaginationRepository<ToDoTask, TaskList>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public InMemoryTaskRepository(IConfiguration config) :
            base(config)
        {
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ToDoTask>> Get()
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    var keys = objRedisClient.GetAllKeys()
                        .FindAll(k => k.StartsWith("Task_") &&
                            k != "Task_TaskCount");
                    List<ToDoTask> tasks = new List<ToDoTask>();
                    keys.ForEach(k =>
                    {
                        tasks.Add(objRedisClient.Get<ToDoTask>(k));
                    });

                    tasks.ForEach(r => r.SetStatus());

                    return tasks;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        public async Task<TaskList> Get(int pageSize, long pageNumber)
        {
            try
            {
                TaskList taskList = new TaskList();

                using (var objRedisClient = new RedisClient(host))
                {
                    var keys = objRedisClient.GetAllKeys()
                       .FindAll(k => k.StartsWith("Task_") &&
                           k != "Task_TaskCount");
                    List<ToDoTask> tasks = new List<ToDoTask>();
                    keys.ForEach(k =>
                    {
                        tasks.Add(objRedisClient.Get<ToDoTask>(k));
                    });

                    tasks.ForEach(r => r.SetStatus());

                    taskList.TotalCount = tasks.Count;
                    taskList.Tasks = tasks as List<ToDoTask>;
                }

                return taskList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Add(ToDoTask task)
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    if (objRedisClient.Get<ToDoTask>(
                        $"Task_{task.TaskId}") == null)
                    {
                        int TaskCount = -1;
                        if (objRedisClient.Exists(
                            "Task_TaskCount") == 1)
                        {
                            TaskCount = objRedisClient.Get<int>(
                                "Task_TaskCount");
                        }
                        else
                        {
                            TaskCount = 1;
                            objRedisClient.Set<int>(
                                "Task_TaskCount",
                                TaskCount);
                        }
                        task.TaskId = TaskCount++;
                        objRedisClient.Set<int>(
                                "Task_TaskCount",
                                TaskCount);
                        return objRedisClient.Set<ToDoTask>(
                            $"Task_{task.TaskId}",
                            task);
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
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<bool> Update(long id, ToDoTask task)
        {
            try
            {
                using (var objRedisClient = new RedisClient(host))
                {
                    if (objRedisClient.Get<ToDoTask>(
                        $"Task_{id}") != null)
                    {
                        task.TaskId = id;
                        return objRedisClient.Set<ToDoTask>(
                            $"Task_{id}",
                            task);
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
                    if (objRedisClient.Get<ToDoTask>(
                        $"Task_{id}") != null)
                    {
                        objRedisClient.Remove($"Task_{id}");
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
