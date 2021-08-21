using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using DevIO.Business.Services;
using DevIO.Data.Context;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DevIO.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>(); //melhor forma de resolver uma dependencia que esta trabalhando no contexto web
            services.AddScoped<IClienteRepository, ClienteRepository>();

            //services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            //services.AddScoped<IProdutoRepository, ProdutoRepository>();
            //services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            //
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IClienteService, ClienteService>();

            //services.AddScoped<IProdutoService, ProdutoService>();
            //services.AddScoped<IFornecedorService, FornecedorService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
