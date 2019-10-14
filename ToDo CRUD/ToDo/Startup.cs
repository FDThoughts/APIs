using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Business;
using ToDo.Business.Interfaces;
using ToDo.Entities;
using ToDo.Middlewares;
using Microsoft.Extensions.Hosting;
using ToDo.Repository;
using ToDo.Repository.Interfaces;

namespace ToDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(); //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IManager<Category>, CategoryManager>();
            if (Configuration.GetValue<bool>("Databases:UseInMemory"))
            {
                // Use redis in memory database
                services.AddTransient<IRepository<Category>, InMemoryCategoryRepository>();
                services.AddTransient<IPaginationRepository<ToDoTask, TaskList>, InMemoryTaskRepository>();
            }
            else
            {
                // Use SQL database
                services.AddTransient<IRepository<Category>, CategoryRepository>();
                services.AddTransient<IPaginationRepository<ToDoTask, TaskList>, TaskRepository>();
            }
            services.AddTransient<IPaginationManager<ToDoTask, TaskList>, TaskManager>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("category",
                    "{controller=Category}/{action=Index}");
                endpoints.MapControllerRoute("default",
                    "{controller=Task}/{action=Index}");
            });
        }
    }
}
