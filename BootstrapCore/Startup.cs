using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootstrapCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BootstrapCore
{
    public class Startup
    {

        public Startup(IConfiguration configuration,IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            if(CurrentEnvironment.IsProduction())
            {
                //TODO: Conexion al verdadero ambiente   
            } else 
            {
                var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
                var connectionString = connectionStringBuilder.ToString();
                var connection = new SqliteConnection(connectionString);
                var builder = new DbContextOptionsBuilder<AppDbContext>();
                builder.UseSqlite(connection);

                services.AddDbContext<AppDbContext>(opts => opts.UseSqlite(connection));

                //Inicializo el contexto sino no se crean las tablas
                using (var ctx = new AppDbContext(builder.Options)){
                    ctx.Database.OpenConnection();
                    ctx.Database.EnsureCreated();
                }

            }
           


           
            services.AddTransient<InscriptionService,InscriptionService>();
            services.AddTransient<DataInitializer,DataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DataInitializer data)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                data.SeedData();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
