using Core;
using Core.ServiceInterfaces;
using Core.Services;
using Data.Repositories;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString")
                                    ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

            services.AddDbContext<Data.Context.ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(typeof(DtoMapping));

            //Adicionar Repositórios
            services.AddScoped(typeof(ITarefaRepository), typeof(TarefaRepository));

            //Adicionar Serviços
            services.AddScoped(typeof(ITarefaService), typeof(TarefaService));

            return services;
        }
    }
}
