using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public User? AddUser(string login, string password, string name, DateTime birthday, Gender gender, int weight,
        int carbohydrateRatio, int breadUnit)
    {
        throw new NotImplementedException();
    }

    public void DeleteUserById(long id)
    {
        throw new NotImplementedException();
    }

    public User? FindUserByLogin(string login)
    {
        const string sql = """
                           select *
                           from users
                           where login = :login;
                           """;
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("login", login);

        using var reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetFieldValue<UserInfo>(2));
    }

    public bool ChangeName(string login, string newName)
    {
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET name = :newName where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newName", newName)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
    }

    public bool ChangeBirthday(string login, DateTime newBirthday)
    {
        throw new NotImplementedException();
    }

    public bool ChangeGender(string login, Gender newGender)
    {
        throw new NotImplementedException();
    }

    public bool ChangeWeight(string login, int newWeight)
    {
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET weight = :newWeight where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newWeight", newWeight)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
    }

    public bool ChangeCarbohydrateRatio(string login, int newCarbohydrateRatio)
    {
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET carbohydrate_ratio = :newCarbohydrateRatio where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newCarbohydrateRatio", newCarbohydrateRatio)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
    }

    public bool ChangeBreadUnit(string login, int newBreadUnit)
    {
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET bread_unit = :newBreadUnit where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newBreadUnit", newBreadUnit)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
    }
}