using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swich.Infra.Data.Context;

namespace Switch.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container. teste de envio
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json");
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var conn = Configuration.GetConnectionString("SwitchDB");
            services.AddDbContext<SwitchContext>(opption => opption.UseLazyLoadingProxies()
                        .UseMySql(conn, m => m.MigrationsAssembly("Switch.Infra.Data"))
            );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
