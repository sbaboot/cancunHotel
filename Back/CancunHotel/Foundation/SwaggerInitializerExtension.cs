using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Foundation.Swagger
{
    public static class SwaggerInitializerExtension
    {
        public static void InitializeSwagger(this IServiceCollection services)
        {
#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(api => api.First());
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Version = Assembly.GetEntryAssembly()?.GetName().Version.ToString(),
                        Title = Assembly.GetEntryAssembly()?.GetName().Name
                    });
                var xmlPath = "swagger.xml";
                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);

                c.IncludeXmlComments(xmlPath);

                // Utilise le nom complet du modèle pour éviter l'exception quand on a plusieurs modèles avec le même nom.
                c.CustomSchemaIds(x => x.FullName);
            });
#endif
        }
    }
}