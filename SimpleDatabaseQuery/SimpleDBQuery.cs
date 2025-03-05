using Microsoft.Data.Sqlite;

namespace SimpleDatabaseQuery;

public class SimpleDBQuery(string pathToDB)
{
    protected readonly SqliteConnection connection = new($"Data Source={pathToDB}");
    protected readonly SqliteCommand command = new();

    /// <summary>
    /// Выполнить запрос к базе данных
    /// </summary>
    /// <param name="query"></param>
    public void Query(string query)
    {
        connection.Open();
        command.Connection = connection;
        command.CommandText = query;
        command.ExecuteNonQuery();
        connection.Close();
    }

    /// <summary>
    /// Получить ответ на отправленный запрос
    /// </summary>
    /// <returns></returns>
    public List<Dictionary<string, object>> GetAnswer()
    {
        List<Dictionary<string, object>> results = [];

        connection.Open();
        command.Connection = connection;
        SqliteDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                Dictionary<string, object> row = [];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                results.Add(row);
            }
        }
        reader.Close();
        connection.Close();

        return results;
    }
}
