using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sugar_backend.Application.Extensions;
using Sugar_backend.Infrastructure.DataAccess.Extensions;

namespace begin;

public static class Startup
{
    public static void Start()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services
            .AddApplication()
            .AddInfrastructureDataAccess(configuration =>
            {
                configuration.Host = "localhost";
                configuration.Port = 6430;
                configuration.Username = "postgres";
                configuration.Password = "postgres";
                configuration.Database = "postgres";
                configuration.SslMode = "Prefer";
            })
            .AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}
