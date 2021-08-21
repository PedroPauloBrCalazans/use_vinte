using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => //configurando o swagger
            {
                c.OperationFilter<SwaggerDefaultValues>();

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] {} }
                }; // estou passando uma colecao de dados de segurança (Bearer = tipo do token)

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu Token}",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                }); // segurança do swagger, implementação basica JWT dentro do swagger

                c.AddSecurityRequirement(security); // segurança do swagger
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            // app.UseMiddleware<SwaggerAuthorizedMiddleware>(); // colocando na ordem que vai ser chamado no pipeLine, ficar acima do swagger
            app.UseSwagger(); //configurando o swagger
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = "API - peterDev.io",
                Version = description.ApiVersion.ToString(),
                Description = "Está API faz parte do curso REST com ASP.NET WEBApi",
                Contact = new Contact() { Name = "Pedro Paulo", Email = "contato@peterDev.io" },
                TermsOfService = "https://opensouce.org/licenses/MIT",
                License = new License() { Name = "MIT", Url = "https://opensouce.org/licenses/MIT" }
            };

            if (description.IsDeprecated)
            {
                info.Description += "Está versão estar obsoleta!";
            }

            return info;
        }
    }


    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            //operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = description.DefaultValue;
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }




}
