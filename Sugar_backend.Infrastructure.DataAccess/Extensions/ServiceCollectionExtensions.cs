using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Microsoft.Extensions.DependencyInjection;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Infrastructure.DataAccess.Plugins;
using Sugar_backend.Infrastructure.DataAccess.Repositories;

namespace Sugar_backend.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection.AddSingleton<IUserRepository, UserRepository>();
        collection.AddSingleton<INoteRepository, NoteRepository>();
        collection.AddSingleton<IProductRepository, ProductRepository>();

        return collection;
    }
}