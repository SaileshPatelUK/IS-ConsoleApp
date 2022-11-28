using FluentValidation;
using IS.Domain.DomainModels;
using IS.Services;
using IS.Services.Interfaces;
using IS.Services.Validator;
using IS.Validator;
using ISConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ISConsoleApp.Helpers
{
    public static class RegisterServicesHelper
    {
        public static void AddServiceLayerServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMovementService<RobotModel, MovementModel>, RobotService>();
            serviceCollection.AddScoped(typeof(ITable<,>), typeof(Table<,>));
            serviceCollection.AddScoped(typeof(IValidatorService<>), typeof(ValidatorService<>));
            serviceCollection.AddScoped<IValidator<MovementModel>, MoveValidator>();
        }
    }
}
