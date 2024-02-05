using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sugar_backend.Application.Extensions;
using Sugar_backend.Infrastructure.DataAccess.Extensions;

namespace Sugar_backend.Startup;

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
                configuration.Port = 6432;
                configuration.Username = "postgres";
                configuration.Password = "postgres";
                configuration.Database = "postgres";
                configuration.SslMode = "Prefer";
            })
            .AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        /*builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
*/
        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        //scope.ResetDataBase();
        //scope.SetUpDataBase();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            
            /*
             or
             app.UseSwagger();
            app.UseSwaggerUI();
             */
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
