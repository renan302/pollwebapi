using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using PollWebApi.Context;
using Microsoft.OpenApi.Models;

namespace PollWebApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PollWebApi Documentation", Version = "v1" });
            });

            if (_env.IsProduction())
            {

                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration =
                        Configuration.GetConnectionString("conexaoredisprod");
                    options.InstanceName = "PollWebApi";                    
                });

                services.AddDbContext<PollContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("pollmysqlprod")));

            }
            else
            {

                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration =
                        Configuration.GetConnectionString("conexaoredisdev");
                    options.InstanceName = "PollWebApi";
                });

                services.AddDbContext<PollContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("pollmysqldev")));                    
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PollWebApi Documentation");
                c.RoutePrefix = "doc";
            });

            app.UseMvc();


        }
    }
}
