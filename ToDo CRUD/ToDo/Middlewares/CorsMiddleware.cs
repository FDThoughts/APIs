using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Middlewares
{
    /// <summary>
    /// Cors Middleware: enable angular calls
    /// </summary>
    public class CorsMiddleware
    {
        /// <summary>
        /// Request delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke: allow Get, Post, Put and Delete
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin",
                "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers",
                "Content-Type, Accept");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods",
                "POST,GET,PUT,DELETE");
            return _next(httpContext);
        }
    }
}
