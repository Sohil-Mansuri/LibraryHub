

using LibraryHub.Core.Context;
using LibraryHub.Core.Repository;
using LibraryHub.Core.Services;
using Microsoft.Extensions.DependencyInjection;
    
namespace LibraryHub.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreProjectServices(this IServiceCollection services)
        {
            services.AddSingleton<BookService>();
            services.AddSingleton<IBookRepository, BookRepository>();
            services.AddSingleton<MongoContext>();

            return services;
        }
    }
}
