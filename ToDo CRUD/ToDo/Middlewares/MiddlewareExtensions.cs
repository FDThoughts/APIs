using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Middlewares
{
    /// <summary>
    /// Middleware Extensions: allow custom middleware in startup
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// UseMiddleware
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
