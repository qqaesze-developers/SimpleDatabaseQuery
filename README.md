<center>

# Simple Database Query

<br>

<img src="img/SimpleDatabaseQuery.png" width="80%">

</center>

***

Простой класс для выполнения запросов к базе данных

В классе 2 метода:

- **`Query(string query)`** - выполнить запрос к базе данных
- **`GetAnswer()`** - получить ответ на отправленный запрос. Возвращает `List<Dictionary<string, object>>`

***

A simple class to perform database queries

There are 2 methods in the class:

- **`Query(string query)`** - execute a query to the database
- **`GetAnswer()`** - get the answer to the sent query. Returns `List<Dictionary<string, object>>`.

## Пример использования / Example usage:

```csharp
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

simpleDBQuery.Query("SELECT * FROM Users"); // Отправляем запрос [Send a query]
List<Dictionary<string, object>> results = simpleDBQuery.GetAnswer(); // Получаем ответ [Get the answer]

foreach (var row in results)
{
    foreach (var data in row)
    {
        Console.WriteLine($"{data.Key}: {data.Value}");
    }
}
```