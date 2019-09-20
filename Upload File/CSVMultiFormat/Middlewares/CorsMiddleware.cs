using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CSVMultiFormat.Middlewares
{
    /// <summary>
    /// CorsMiddleware class to manage request communication
    /// with Angular apps
    /// </summary>
    public class CorsMiddleware
    {
        /// <summary>
        /// Request delegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// CorsMiddleware constructor
        /// </summary>
        /// <param name="next">RequestDelegate</param>
        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invoke methode
        /// </summary>
        /// <param name="httpContext">HttpContext</param>
        /// <returns></returns>
        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.Headers.Add("Access-Control-Allow-Origin",
                "*");
            httpContext.Response.Headers.Add("Access-Control-Allow-Headers",
                "Content-Type, Accept");
            httpContext.Response.Headers.Add("Access-Control-Allow-Methods",
                "POST,GET"); // limited to Get and Post
            return _next(httpContext);
        }
    }
}
