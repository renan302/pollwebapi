using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PollWebApi.Context;

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



            if (_env.IsProduction())
            {
                services.AddDbContext<PollContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("pollmysqlprod")));
            }
            else
            {                
                services.AddDbContext<PollContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("pollmysqldev")));
                    //options.UseMySql("server = 192.168.15.14; database = polldb; uid = root; pwd = root;"));
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
            app.UseMvc();


        }
    }
}
