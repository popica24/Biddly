using Business.Contracts;
using Services.Context;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using System.Reflection;
using System.Data;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Services.Repositories;

public class GenericRepository<T>(SqlDataContext context) : IGenericRepository<T> where T : class
{
    public async Task<bool> AddAsync(T entity)
    {
        int rowsAffected = 0;
        try
        {
            string tableName = GetTableName();
            string columns = GetColumns(excludeKey: false);
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

    public async Task<bool> UpdateAsync(T entity, params string[] columnsToUpdate)
    {
        int rowsAffected = 0;
        try
        {
            string tableName = GetTableName();
            string keyColumn = GetKeyColumnName();
            string keyProperty = GetKeyPropertyName();

            StringBuilder query = new();

            query.Append($"UPDATE {tableName} SET ");
            //"UPDATE _____ SET

            foreach(var prop in GetProperties(true))
            {
                var columnAttribute = prop.GetCustomAttribute<ColumnAttribute>();

                string propName = prop.Name;
                string colName = columnAttribute.Name;

                query.Append($"{colName} = @{propName},");
                //"____ = @____,____ = @____,
            }
            query.Remove(query.Length - 1, 1);
            //remove the last ","

            query.Append($" WHERE {keyColumn} = @{keyProperty}");

            //now the query looks like this : UPDATE _____ SET ____ = @____ WHERE _____ = @______

            string queryForAffectedRows = $"WITH ROWS AS ({query} RETURNING 1) SELECT COUNT(*) FROM ROWS";

            rowsAffected = await context.Connection.ExecuteAsync(queryForAffectedRows, entity);
        }
        catch (Exception ex) { }

        return rowsAffected > 0;
    }

    protected static string GetKeyPropertyName()
    {
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<KeyAttribute>() != null);

        return properties.Any() ? properties.FirstOrDefault().Name : null;


    }

    protected IEnumerable<PropertyInfo> GetProperties(bool excludeKey = false)
    {
        var properties = typeof(T).GetProperties()
            .Where(p => !excludeKey || p.GetCustomAttribute<KeyAttribute>() == null);

        return properties;
    }

    public async Task<IEnumerable<T>> GetByColumnAsync(string columnName, string columnValue, params string[] selectData)
    {

        string tableName = GetTableName();

        var columns = !(selectData.IsNullOrEmpty()) ?  string.Join(", ", selectData) : "*";

        string query = $"SELECT {columns} FROM {tableName} WHERE {columnName} = '{columnValue}'";

        var result = await context.Connection.QueryAsync<T>(query);

        return result;
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

    public static string GetKeyColumnName()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object[] keyAttributes = property.GetCustomAttributes(typeof(KeyAttribute), true);

            if (keyAttributes != null && keyAttributes.Length > 0)
            {
                object[] columnAttributes = property.GetCustomAttributes(typeof(ColumnAttribute), true);

                if (columnAttributes != null && columnAttributes.Length > 0)
                {
                    ColumnAttribute columnAttribute = (ColumnAttribute)columnAttributes[0];
                    return columnAttribute.Name;
                }
                else
                {
                    return property.Name;
                }
            }
        }

        return null;
    }
}
