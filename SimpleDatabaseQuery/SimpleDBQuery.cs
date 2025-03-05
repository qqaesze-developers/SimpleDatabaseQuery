/*
 * Simple Database Query - простой класс для выполнения запросов к базе данных
 * 
 * В классе 2 метода:
 *      - Query(string query) - выполнить запрос к базе данных
 *      - GetAnswer() - получить ответ на отправленный запрос. Возвращает List<Dictionary<string, object>>
 *      
 * Пример использования:
 * 
    SimpleDBQuery simpleDBQuery = new("data.db");
    simpleDBQuery.Query("""
        CREATE TABLE IF NOT EXISTS Users (
            firstName TEXT,
            lastName TEXT,
            age INT
        )
        """);
    simpleDBQuery.Query("INSERT INTO Users VALUES ('Ivan', 'Ivanov', 24)");
    simpleDBQuery.Query("INSERT INTO Users VALUES ('Petr', 'Alexandrovich', 30)");

    simpleDBQuery.Query("SELECT * FROM Users");
    List<Dictionary<string, object>> results = simpleDBQuery.GetAnswer();

    foreach (var row in results)
    {
        foreach (var data in row)
        {
            Console.WriteLine($"{data.Key}: {data.Value}");
        }
    }
 */

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
