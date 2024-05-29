using Business.Contracts;
using Services.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using System.Reflection;

namespace Services.Repositories;

public class GenericRepository<T>(SqlDataContext context) : IGenericRepository<T> where T : class
{
    public async Task<bool> AddAsync(T entity)
    {
        int rowsAffected = 0;
        try
        {
            string tableName = GetTableName();
            string columns = GenericRepository<T>.GetColumns(excludeKey: false);
            string properties = GetPropertyNames(excludeKey: false);
            string query = @$"WITH rows AS (
                               INSERT INTO {tableName}
                                ({columns})
                                VALUES
                                ({properties})
                                RETURNING 1
                                )
                                SELECT count(*) FROM rows;";

            rowsAffected = await context.Connection.ExecuteScalarAsync<int>(query, entity);
        }
        catch (Exception ex) { }

        return rowsAffected > 0;
    }

    private static string GetTableName()
    {
        var type = typeof(T);
        var tableAttr = type.GetCustomAttribute<TableAttribute>();
        if (tableAttr != null)
        {
            var tableName = tableAttr.Name;
            return tableName;
        }

        return type.Name;
    }

    private static string GetColumns(bool excludeKey = false)
    {
        var type = typeof(T);
        var columns = string.Join(", ", type.GetProperties()
            .Where(p => !excludeKey || !p.IsDefined(typeof(KeyAttribute)))
            .Select(p =>
            {
                var columnAttr = p.GetCustomAttribute<ColumnAttribute>();
                return columnAttr != null ? columnAttr.Name : p.Name;
            }));

        return columns;
    }

    protected string GetPropertyNames(bool excludeKey = false)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        var values = string.Join(", ", properties.Select(p =>
        {
            return $"@{p.Name}";
        }));

        return values;
    }
}
