using Giffinator.Shared.Models;
using Microsoft.Data.Sqlite;

namespace Giffinator.Host.Data;

public class SqliteDbSetup
{
    private readonly ILogger<SqliteDbSetup> _logger;
    private readonly SqliteConnection _sqliteConnection;

    public SqliteDbSetup(ILogger<SqliteDbSetup> logger, SqliteConnection sqliteConnection)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _sqliteConnection = sqliteConnection ?? throw new ArgumentNullException(nameof(sqliteConnection));
    }

    public async Task EnsureDbSetup()
    {
      
            _logger.LogInformation("Ensuring database exists at connection string '{ConnectionString}'",
                _sqliteConnection.ConnectionString);
            await _sqliteConnection.OpenAsync();

            var command2 = _sqliteConnection.CreateCommand();
            command2.CommandText = $@"CREATE TABLE IF NOT EXISTS {nameof(GifRecords)} (
                                                        {nameof(GifRecords.Id)} INTEGER PRIMARY KEY, 
                                                        {nameof(GifRecords.FullPath)} STRING NOT NULL,
                                                        {nameof(GifRecords.FileName)} STRING NOT NULL  ,
                                                        {nameof(GifRecords.CreatedDate)} DATETIME NOT NULL
)";
            await command2.ExecuteNonQueryAsync();
            
    }
}