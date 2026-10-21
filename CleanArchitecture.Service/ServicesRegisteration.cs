using CleanArchitecture.Service.Factories;
using Microsoft.Extensions.DependencyInjection;
namespace CleanArchitecture.Service
{
    public static class ServicesRegisteration
    {
        public static void AddServicesRegistrations(this IServiceCollection services)
        {
            services.AddScoped(typeof(BawabtkFactory<,>));
            services.AddScoped(typeof(BawabtkAuthFactory<,>));
            services.AddScoped(typeof(AttendanceFactory<,>));
        }
    }
}
