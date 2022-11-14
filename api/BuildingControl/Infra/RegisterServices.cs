using Application.Usecases.RegisterUser;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class RegisterServices
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<RegisterUser>();
    }
}
