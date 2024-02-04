using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Products;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public ProductRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public Product? GetProductByName(string name)
    {
        const string sql = """
                           select *
                           from product
                           where product_name = :name;
                           """;
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("name", name);

        using var reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Product(
            reader.GetString(1),
            reader.GetInt32(2));
    }

    public int GetCarbsAmount(string name)
    {
        const string sql = """
                           select *
                           from product
                           where product_name = :name;
                           """;
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("name", name);

        using var reader = command.ExecuteReader();

        if (reader.Read() is false)
            return 0;

        return reader.GetInt32(2);
    }

    public void AddProduct(string name, int carbs)
    {
        const string query = "INSERT INTO product(product_name, carbs) VALUES (($1),($2))";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue(name);
        cmd.Parameters.AddWithValue(carbs);

        cmd.ExecuteNonQueryAsync();
    }
}