using Microsoft.Data.SqlClient;

namespace Services.Context;

public class SqlDataContext : IDisposable
{
    public SqlConnection Conenction { get; set; }

    public SqlDataContext(IConnectionString connectionString)
    {
        Conenction = new SqlConnection(connectionString.SqlConnectionString);
        Conenction.Open();
    }

    public void Dispose()
    {
        Conenction.Close();
        Conenction.Dispose();
    }
}
