using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDo.Business.Interfaces;
using ToDo.Entities;

namespace ToDo.Controllers
{
    /// <summary>
    /// Task Controller: Get, Post, Put and Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// Task manager
        /// </summary>
        IPaginationManager<ToDoTask, TaskList> _manager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="manager"></param>
        public TaskController(IConfiguration config,
            IPaginationManager<ToDoTask, TaskList> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get: get all tasks
        /// </summary>
        /// <returns>List of Tasks</returns>
        // GET api/task
        [HttpGet]
        public async Task<IEnumerable<ToDoTask>> Get()
        {
            return await _manager.Get();
        }

        /// <summary>
        /// Get: get tasks with pagination
        /// </summary>
        /// <param name="urlQuery">url query string</param>
        /// <returns>List of tasks</returns>
        [HttpGet]
        [Route("taskList")]
        public async Task<TaskList> Get([FromQuery] UrlQuery urlQuery)
        {
            return await _manager.Get(urlQuery.PageSize, 
                (long)urlQuery.PageNumber);
        }

        /// <summary>
        /// Post: create a new Task
        /// </summary>
        /// <param name="value">Task</param>
        /// <returns>True if task created successfully</returns>
        // POST api/task
        [HttpPost()]
        public async Task<IActionResult> Post(
            [FromBody] ToDoTask value)
        {
            try
            {
                if (await _manager.Add(value))
                    return Ok();
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Put: update existing task
        /// </summary>
        /// <param name="id">long</param>
        /// <param name="value">Task</param>
        /// <returns>True if task updated successfully</returns>
        // PUT api/task/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] ToDoTask value)
        {
            try
            { 
                if(await _manager.Update(id, value))
                    return Ok();
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete: delete a task
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>True if task deleted successfully</returns>
        // DELETE api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                if(await _manager.Delete(id))
                    return Ok();
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
