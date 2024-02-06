using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Auction.Contracts.Validation;

public static class DependencyInjection
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation(opt =>
        {
            opt.OverrideDefaultResultFactoryWith<FluentValidationAutoValidationCustomResultFactory>();
        });
        return services;
    }
}