using api.Infrastructure.Context;
using api.Models.ServiceModel;
using api.Models.ServiceModel.Interfaces;
using api.Repository;
using api.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DbContextApi>();
            services.AddScoped<ITransactionCardRepository, TransactionCardRepository>();
            services.AddScoped<IInstallmentTransactionRepository, InstallmentTransactionRepository>();
            services.AddScoped<IAnticipationRequestRepository, AnticipationRequestRepository>();
            services.AddScoped<ITransactionAnticipationRequestRepository, TransactionAnticipationRequestRepository>();
            services.AddScoped<ITransactionCardService, TransactionCardService>();
            services.AddScoped<IAnticipationRequestService, AnticipationRequestService>();

            return services;
        }
    }
}
