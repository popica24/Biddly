using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;

namespace Services.Context;

public class SqlDataContext(IConnectionString connectionString) : IDisposable
{
    private IDbConnection? connection;
    private IDbTransaction? transaction;

    public IDbConnection? Connection
    {
        get
        {
            if (connection is null || connection.State != ConnectionState.Open)
                connection = new NpgsqlConnection(connectionString.SqlConnectionString);
            return connection;
        }
    }

    public IDbTransaction? Transaction
    {
        get
        {
            return transaction;
        }
        set
        {
            transaction = value;
        }
    }

    public void Dispose()
    {
        connection.Close();
        connection.Dispose();
    }
}
