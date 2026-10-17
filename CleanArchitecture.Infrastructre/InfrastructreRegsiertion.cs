using CleanArchitecture.Infrastructre.Data;
using huzcodes.Persistence.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructre
{
    public static class InfrastructreRegsiertion
    {
        public static void AddBawabtkDbContext(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<BawabtkDbContext>(o => o.UseSqlServer(connectionString));
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<BawabtkDbContext>());
        }
        public  static void AddInfrastructureResgistertion(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BawabtkRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(BawabtkRepository<>));
        }

    }
}
