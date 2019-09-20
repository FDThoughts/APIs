using Microsoft.AspNetCore.Builder;

namespace CSVMultiFormat.Middlewares
{
    /// <summary>
    /// MiddlewareExtensions class
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// UseMiddleware method to be called at Startup
        /// </summary>
        /// <param name="builder">IApplicationBuilder</param>
        /// <returns></returns>
        public static IApplicationBuilder UseMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}
