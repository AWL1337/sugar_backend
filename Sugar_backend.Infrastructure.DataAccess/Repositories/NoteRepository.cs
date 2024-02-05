using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;
using System.Collections.ObjectModel;
using Itmo.Dev.Platform.Postgres.Extensions;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Models.Notes;
using Sugar_backend.Application.Models.Products;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class NoteRepository : INoteRepository
{
    public static NoteRepository? Instance { get; private set; }
    private readonly IPostgresConnectionProvider _connectionProvider;

    private NoteRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public INoteRepository Create(IPostgresConnectionProvider connectionProvider)
    {
        if (Instance is null)
            return Instance = new NoteRepository(connectionProvider);
        return Instance;
    }

    public IEnumerable<Note> GetAllNotes(string login)
    {
        var userRepository = new UserRepository(_connectionProvider);

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        var userID = userRepository.GetUserId(login);


        const string sql = """
                           select *
                           from note_header
                           where user_id =: userID
                           """;

        using var command = new NpgsqlCommand(sql, connection).AddParameter("user_id", userID);
        ;
        using var reader = command.ExecuteReader();

        List<Note> notes = new();
        while (reader.Read())
        {
            notes.Add(new Note(
                reader.GetInt64(1),
                reader.GetFieldValue<NoteType>(2),
                reader.GetDateTime(3),
                reader.GetInt16(4),
                null));
        }

        return notes;
    }

    public int GetNoteCarbsAmount(DateTime dateTime, string login)
    {
        var note = GetNoteByDate(login, dateTime);

        var carbs = note.Products.Sum(noteProduct => noteProduct.Product.Carbs);
        return carbs;
    }

    public Note? GetNoteByDate(string login, DateTime dateTime)
    {
        var userRepository = new UserRepository(_connectionProvider);

        var userID = userRepository.GetUserId(login);

        const string sql = """
                           select *
                           from note_header
                           where create_date = :dateTime and user_id = :userId;
                           """;

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("dateTime", dateTime).AddParameter("user_id", userID);

        using var reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        var note = new Note(
            reader.GetInt64(1),
            reader.GetFieldValue<NoteType>(2),
            reader.GetDateTime(3),
            reader.GetInt16(4),
            null);

        var noteID = reader.GetInt64(0);

        const string sqlDetail = """
                                 select *
                                 from note_detail
                                 where note_id = :noteID;
                                 """;


        using var commandDetail = new NpgsqlCommand(sqlDetail, connection).AddParameter("note_id", noteID);
        using var readerDetail = commandDetail.ExecuteReader();

        while (readerDetail.Read())
        {
            var nameOfProduct = readerDetail.GetString(1);
            const string sqlProduct = """
                                      select carbs
                                      from product
                                      where product_name = :nameOfProduct
                                      """;

            using var commandProduct = new NpgsqlCommand(sql, connection)
                .AddParameter("product_name", nameOfProduct);

            using var readerProduct = command.ExecuteReader();

            if (reader.Read() is false)
                return null;
            var carbs = readerProduct.GetInt32(2);

            var product = new Product(nameOfProduct, carbs);
            note.Products.Add(new NoteProduct(product, readerDetail.GetInt32(2)));
        }

        return note;
    }

    public void DeleteNote(string login, DateTime date)
    {
        var userRepository = new UserRepository(_connectionProvider);

        var userID = userRepository.GetUserId(login);

        const string queryHeader = "DELETE FROM note_header WHERE create_date = :date and user_id = :userId";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var cmdHeader = new NpgsqlCommand(queryHeader, connection);
        cmdHeader.Parameters.AddWithValue(date);
        cmdHeader.Parameters.AddWithValue(userID);

        cmdHeader.ExecuteNonQueryAsync();

        const string sqlNote = """
                               select note_id
                               from note_header
                               where create_date = :date
                               """;

        using var commandNote = new NpgsqlCommand(sqlNote, connection)
            .AddParameter("create_date", date);

        using var readerNote = commandNote.ExecuteReader();

        if (readerNote.Read() is false)
            return;
        var noteId = readerNote.GetInt64(0);

        const string queryDetail = "DELETE FROM note_detail WHERE create_date = :date and note_id = :noteId";
        using var cmdDetail = new NpgsqlCommand(queryDetail, connection);
        cmdDetail.Parameters.AddWithValue(date);
        cmdDetail.Parameters.AddWithValue(noteId);

        cmdDetail.ExecuteNonQueryAsync();
    }

    public void AddNote(long userId, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        const string query =
            "INSERT INTO note_header (user_id, note_type, create_date, sugar_level) VALUES (($1), ($2), ($3), ($4))";

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var cmd = new NpgsqlCommand(query, connection);
        cmd.Parameters.AddWithValue(userId);
        cmd.Parameters.AddWithValue(type);
        cmd.Parameters.AddWithValue(date);
        cmd.Parameters.AddWithValue(sugarLevel);

        cmd.ExecuteNonQueryAsync();


        const string sqlNote = """
                               select note_id
                               from note_header
                               where create_date = :date and user_id = :userId
                               """;

        using var commandNote = new NpgsqlCommand(sqlNote, connection)
            .AddParameter("create_date", date);

        using var readerNote = commandNote.ExecuteReader();

        if (readerNote.Read() is false)
            return;
        var noteId = readerNote.GetInt64(0);

        foreach (var product in products)
        {
            const string sqlProduct = """
                                      select product_id
                                      from product
                                      where product_name = :nameOfProduct
                                      """;

            using var commandProduct = new NpgsqlCommand(sqlProduct, connection)
                .AddParameter("product_name", product.Product.Name);

            using var readerProduct = commandProduct.ExecuteReader();

            if (readerProduct.Read() is false)
                return;
            var productID = readerProduct.GetInt32(0);

            const string queryDetails =
                "INSERT INTO note_detail (note_id, product_id, product_amount) VALUES (($1), ($2), ($3))";

            using var cmdDetails = new NpgsqlCommand(queryDetails, connection);
            cmdDetails.Parameters.AddWithValue(noteId);
            cmdDetails.Parameters.AddWithValue(productID);
            cmdDetails.Parameters.AddWithValue(product.Amount);

            cmdDetails.ExecuteNonQueryAsync();
        }
    }
}