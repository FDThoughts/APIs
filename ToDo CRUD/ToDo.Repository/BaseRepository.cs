using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ToDo.Repository
{
    /// <summary>
    /// Repository base class
    /// </summary>
    public class BaseRepository : IDisposable
    {
        /// <summary>
        /// SQL connection
        /// </summary>
        protected IDbConnection conn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public BaseRepository(IConfiguration config)
        {
            string connectionString = config
                .GetValue<string>("Connections:ConnectionString");
            conn = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            conn.Close();
            conn.Dispose();
        }
    }
}
