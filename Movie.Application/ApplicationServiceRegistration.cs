using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Movie.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this  IServiceCollection services)
    {
        return services;
    }
}