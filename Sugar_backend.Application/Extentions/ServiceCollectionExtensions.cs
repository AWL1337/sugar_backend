using Microsoft.Extensions.DependencyInjection;
using Sugar_backend.Application.Contracts.Notes;
using Sugar_backend.Application.Contracts.Users;
using Sugar_backend.Application.Notes;
using Sugar_backend.Application.Products;
using Sugar_backend.Application.Users;
using Sugar_backendar.Application.Contracts.Products;

namespace Sugar_backend.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection.AddScoped<IUserService, UserService>();
        collection.AddScoped<IProductService, ProductService>();
        collection.AddScoped<INoteService, NoteService>();

        collection.AddScoped<CurrentUserManager>();
        collection.AddScoped<ICurrentUserService>(
            p => p.GetRequiredService<CurrentUserManager>());

        return collection;
    }
}