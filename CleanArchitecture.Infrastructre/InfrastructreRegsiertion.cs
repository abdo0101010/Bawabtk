using CleanArchitecture.Infrastructre.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructre
{
    public static class InfrastructreRegsiertion
    {
        public static void AddBawabtkDbContext(this IServiceCollection services, string connectionString)=>
            services.AddDbContext<BawabtkDbContext>(o => o.UseSqlServer(connectionString));

    }
}
