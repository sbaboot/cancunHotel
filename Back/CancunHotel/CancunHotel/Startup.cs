using CancunHotel.Infrastructure.DI;
using CancunHotel.Domain.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CancunHotel.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CancunHotel
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                    .AddHotelCancunApiInfrastructure()
                    .AddCancunHotelApiDomain();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().AllowAnyOrigin());
            //});

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost")
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                });
            });


            services.AddDbContext<CancunHotelDbContext>(options =>
                options.UseMySql("Host='cancunhotel.czfxewceanxd.eu-west-3.rds.amazonaws.com';Port=3306;Database='cancunhotel';Username='admin';Password='Toulouse31+'", 
                new MySqlServerVersion(new Version(10, 5, 13))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                       .AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            app.UseHttpsRedirection();


            app.UseRouting();
            UseAngular(app);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void UseAngular(IApplicationBuilder app)
        {
            DefaultFilesOptions dfoptions = new DefaultFilesOptions();
            dfoptions.DefaultFileNames.Clear();
            dfoptions.DefaultFileNames.Add("/index.html");
            app.UseDefaultFiles(dfoptions);

            StaticFileOptions sfoptions = new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    var path = context.Context.Request.Path;
                if (!string.IsNullOrEmpty(Path.GetExtension(path.Value)))
                {
                    // Disable caching of all static files.
                    context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store";
                    context.Context.Response.Headers["Pragma"] = "no-cache";
                    context.Context.Response.Headers["Expires"] = "-1";
                    context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
                    context.Context.Response.Headers["Access-Control-Allow-Credentials"] = "true";
                    context.Context.Response.Headers["Access-Control-Allow-Headers"] = "*";
                    context.Context.Response.Headers["Access-Control-Allow-Methods"] = "POST,GET,PUT,DELETE,OPTIONS";
                    }
                }
            };
            app.UseStaticFiles(sfoptions);
        }
    }
}
