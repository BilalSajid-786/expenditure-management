using Expenditure.Entities;
using Expenditure.Repository;
using Expenditure.RepositoryContracts;
using Expenditure.Service;
using Expenditure.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace Expenditure.Management
{
    public static class ApplicationDependenyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ITransactionService, TransactionService>();

            //Repositories
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();

            //DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
