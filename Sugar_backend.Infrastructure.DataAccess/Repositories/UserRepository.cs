using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class UserRepository: IUserRepository
{
    private static IPostgresConnectionProvider _connectionProvider;

    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }
    public User? AddUser(
        string login,
        string password,
        string name,
        DateTime birthday,
        Gender gender,
        int weight,
        int height,
        int carbohydrateRatio,
        int breadUnit)
    {
        const string sql = """
                           INSERT INTO users (login, password)
                           VALUES (@login, @password)
                           RETURNING user_id
                           """;

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("login", login)
            .AddParameter("password", password);

        var reader = command.ExecuteReader();
        if (reader.Read() is false)
            return new User();

        var id = reader.GetInt64(0);

        const string sqlInfo = """
                               INSERT INTO user_info(user_id, name, birthday, gender, weight, height, carbohydrate_ratio, bread_unit)
                               VALUES(@id, @name, @birthday, @gender, @weight, @height, @carbohydrateRatio, @breadUnit)
                               """;

        using var commandInfo = new NpgsqlCommand(sqlInfo, connection)
            .AddParameter("id", id)
            .AddParameter("name", name)
            .AddParameter("birthday", birthday)
            .AddParameter("gender", gender)
            .AddParameter("weight", weight)
            .AddParameter("height", height)
            .AddParameter("carbohydrateRatio", carbohydrateRatio)
            .AddParameter("breadUnit", breadUnit);

        commandInfo.ExecuteNonQueryAsync();

        return new User(id, login, new UserInfo(name, birthday, gender, weight, carbohydrateRatio, breadUnit));
    }

    public User? FindUser(string login, string password)
    {
        const string sql = """
                           select *
                           from users
                           where login = :login
                           and password = :password
                           """;
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();
        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("login", login)
            .AddParameter("password", password);

        using var reader = command.ExecuteReader();
        if (reader.Read() is false)
            return new User();

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetFieldValue<UserInfo>(2));
    }

    public void DeleteUserById(long id)
    {
        const string sql = """
                           delete from users
                           where login = :id;
                           """;
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("id", id);

        command.ExecuteNonQuery();
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
            return new User();

        return new User(
            reader.GetInt64(0),
            reader.GetString(1),
            reader.GetFieldValue<UserInfo>(2));
    }

    public static long GetUserId(string login)
    {
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        const string sqlUser = """
                               select user_id
                               from users
                               where login = :login
                               """;

        using var commanduser = new NpgsqlCommand(sqlUser, connection)
            .AddParameter("login", login);

        using var readerUser = commanduser.ExecuteReader();

        return readerUser.Read() is false ? 
            0 : readerUser.GetInt32(0);
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
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET birthday = :newBirthday where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newBirthday", newBirthday)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
    }

    public bool ChangeGender(string login, Gender newGender)
    {
        var userId = FindUserByLogin(login)?.Id;
        const string sql = "UPDATE user_info SET gender = :newGender where user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("newGender", newGender)
            .AddParameter("userId", userId);

        command.ExecuteNonQueryAsync();

        return true;
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