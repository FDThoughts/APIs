using Microsoft.Extensions.Configuration;
using System;
using System.Data;

namespace ToDo.Repository
{
    /// <summary>
    /// In Memory Repository base class
    /// </summary>
    public class InMemoryRepository
    {
        /// <summary>
        /// Redis host
        /// </summary>
        protected string host;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public InMemoryRepository(IConfiguration config)
        {
            host = config.GetValue<string>("Hosts:Redis");
        }
    }
}
