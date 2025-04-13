using Microsoft.Extensions.DependencyInjection;
using Vypex.CodingChallenge.API.Services.EmployeeService;
using Vypex.CodingChallenge.API.Services.LeaveService;

namespace Vypex.CodingChallenge.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddApiModule(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ILeaveService, LeaveService>();
            return services;
        }

        public static IMvcBuilder AddApiControllers(this IMvcBuilder builder)
        {
            return builder.AddApplicationPart(typeof(ApiModule).Assembly);
        }
    }
}
