using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ToDo.Business.Interfaces;
using ToDo.Entities;

namespace ToDo.Controllers
{
    /// <summary>
    /// Category Controller: Get, Post, Put and Delete
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class CategoryController : ControllerBase
    {
        /// <summary>
        /// Category Manager
        /// </summary>
        IManager<Category> _manager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        /// <param name="manager"></param>
        public CategoryController(IConfiguration config,
            IManager<Category> manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Get: all categories
        /// </summary>
        /// <returns>List of Categories</returns>
        // GET api/category
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await _manager.Get();
        }

        /// <summary>
        /// Post: create new category
        /// </summary>
        /// <param name="value">Category</param>
        /// <returns>True if category created successfully</returns>
        // POST api/category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category value)
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
        /// Put: update existing category
        /// </summary>
        /// <param name="id">long</param>
        /// <param name="value">Categry</param>
        /// <returns>True if category updated successfully</returns>
        // PUT api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] Category value)
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
        /// Delete: delete a category
        /// </summary>
        /// <param name="id">long</param>
        /// <returns>True if category deleted successfully</returns>
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
