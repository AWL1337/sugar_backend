using Itmo.Dev.Platform.Postgres.Plugins;
using Npgsql;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Infrastructure.DataAccess.Plugins;

public class MappingPlugin : IDataSourcePlugin
{
    public void Configure(NpgsqlDataSourceBuilder builder)
    {
        builder.MapEnum<NoteType>();
        builder.MapEnum<Gender>();
    }
}