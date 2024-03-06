using FluentMigrator.Runner;
using Npgsql;

public class DatabaseService
{
    private readonly string _defaultConnection = Config.GetEnv("ConnectionStrings:DefaultConnection");

    private readonly string _initialConnection = Config.GetEnv("ConnectionStrings:InitialConnection");

    public void MigrateDatabase()
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(this._defaultConnection)
                .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using (var scope = serviceProvider.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }

    public void EnsureDatabaseExists(string databaseName)
    {
        using (var connection = new NpgsqlConnection(this._initialConnection))
        {
            var encoding = "UTF8";
            connection.Open();
            using (var command = new NpgsqlCommand($"SELECT 1 FROM pg_database WHERE datname = '{databaseName}'", connection))
            {
                var result = command.ExecuteScalar();
                if (result == null)
                {
                    // Baza s tim imenom ne postoji, kreiraj novu bazu
                    command.CommandText = $"CREATE DATABASE {databaseName} ENCODING '{encoding}'";
                    command.ExecuteNonQuery();
                }
            }
            connection.Close(); // Zatvori konekciju
        }
    }

    public static void SetupDatabase()
    {
        var databaseService = new DatabaseService();
        databaseService.EnsureDatabaseExists(Config.GetEnv("ConnectionStrings:DbName"));
        databaseService.MigrateDatabase();
    }
}