
**Simple Database Query** - простой класс для выполнения запросов к базе данных

В классе 2 метода:

    **- `Query(string query)`** - выполнить запрос к базе данных
    
    **- `GetAnswer()`** - получить ответ на отправленный запрос. Возвращает `List<Dictionary<string, object>>`

## Пример использования:

```
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

simpleDBQuery.Query("SELECT * FROM Users"); // Отправляем запрос
List<Dictionary<string, object>> results = simpleDBQuery.GetAnswer(); // Получеам ответ

foreach (var row in results)
{
    foreach (var data in row)
    {
        Console.WriteLine($"{data.Key}: {data.Value}");
    }
}
```