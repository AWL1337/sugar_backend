using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;
using System.Collections.ObjectModel;
using Itmo.Dev.Platform.Postgres.Extensions;
using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Notes;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;
    
    public NoteRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }
    
    public IEnumerable<Note> GetAllNotes()
    {
        const string sql = """
                           select user_id, note_type, create_date, sugar_level
                           from note_header
                           """;

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Note(
                reader.GetInt64(0),
                reader.GetFieldValue<NoteType>(1),
                reader.GetDateTime(2),
                reader.GetInt16(3), 
                null);
        }
    }
    public Note? GetNoteByDate(long userId, DateTime dateTime)
    {
        const string sql = """
                           select user_id, note_type, create_date, sugar_level, note_id
                           from note_header
                           where create_date = :dateTime;
                           """;

        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var command = new NpgsqlCommand(sql, connection)
            .AddParameter("dateTime", dateTime);

        using var reader = command.ExecuteReader();

        if (reader.Read() is false)
            return null;

        return new Note(
            reader.GetInt64(0),
            reader.GetFieldValue<NoteType>(1),
            reader.GetDateTime(2),
            reader.GetInt16(3), 
            null);
		
		// const int noteID = reader.GetInt64(4);
		
		const string sqlDetail = """
                           select product_id, product_amount
                           from note_header
                           where note_id = :noteID;
                           """;


        using var commandDetail = new NpgsqlCommand(sqlDetail, connection);
        using var readerDetail = commandDetail.ExecuteReader();

        while (readerDetail.Read())
        {
            // NeededNote.Products.add(new NoteProduct(readerDetail.GetString(1)));
			
        }
    }

    public void DeleteNote(long userId, DateTime date)
    {
        const string queryHeader = "DELETE FROM note_header WHERE create_date = ($1)";
        const string queryDetail = "DELETE FROM note_detail WHERE create_date = ($1)";
        
        var connection = _connectionProvider
            .GetConnectionAsync(default)
            .GetAwaiter()
            .GetResult();

        using var cmdHeader = new NpgsqlCommand(queryHeader, connection);
        cmdHeader.Parameters.AddWithValue(date);

        cmdHeader.ExecuteNonQueryAsync();
        
        using var cmdDetail = new NpgsqlCommand(queryDetail, connection);
        cmdHeader.Parameters.AddWithValue(date);

        cmdDetail.ExecuteNonQueryAsync();
    }

    public void AddNote(long userId, NoteType type, DateTime date, int sugarLevel, Collection<NoteProduct> products)
    {
        const string query = "INSERT INTO note_header (user_id, note_type, create_date, sugar_level) VALUES (($1), ($2), ($3), ($4))";

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

        foreach (var product in products)
        {
            const string queryDetails = "INSERT INTO note_detail (product_id, product_amount) VALUES (($1), ($2))";

            using var cmdDetails = new NpgsqlCommand(queryDetails, connection);
            cmdDetails.Parameters.AddWithValue(product.Product.Name);
            cmdDetails.Parameters.AddWithValue(product.Amount);

            cmdDetails.ExecuteNonQueryAsync();
        }
    }
}