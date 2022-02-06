using CancunHotel.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using CancunHotel.Infrastructure.DI;
using CancunHotel.Domain.DI;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Foundation.Swagger;

namespace CancunHotel.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IServiceCollection Services { get; set; }
        /// <summary>
        /// Configuration générale
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddRouting();
            services.PostConfigure<MvcNewtonsoftJsonOptions>(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<Infrastructure.Repository.CancunHotelDbContext>(options =>
                options.UseMySql("Data Source=Database.db", new MySqlServerVersion(new Version(10, 4, 10))));

            services
                .AddHotelCancunApiInfrastructure()
                .AddCancunHotelApiDomain();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.InitializeSwagger();
            Services = services;
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
                    }
                }
            };
            app.UseStaticFiles(sfoptions);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            var logger = loggerFactory.CreateLogger("Configure");
            try
            {

                logger.LogWarning("Configure - start");


                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }

                logger.LogWarning("Alert - UseHttpsRedirection");
                app.UseHttpsRedirection();
                logger.LogWarning("Alert - UseCors");
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

#if DEBUG
                logger.LogWarning("Settings - Swagger");
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "CancunHotel Api");
                    c.RoutePrefix = "api/swagger";
                });
#endif


            }
            catch (System.Exception ex)
            {
                if (logger != null)
                    logger.LogWarning($"Alert - ex : {ex.Message} {ex.StackTrace}");

                throw ex;
            }
            finally
            {
                if (logger != null)
                    logger.LogWarning("Alert - end");
            }
        }
    }
}

