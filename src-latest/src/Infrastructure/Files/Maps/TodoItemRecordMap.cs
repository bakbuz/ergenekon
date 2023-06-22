using CsvHelper.Configuration;
using Ergenekon.Application.TodoLists.Queries.ExportTodos;
using System.Globalization;

namespace Ergenekon.Infrastructure.Files.Maps;

public class TodoItemRecordMap : ClassMap<TodoItemRecord>
{
    public TodoItemRecordMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        Map(m => m.Done).Convert(c => c.Value.Done ? "Yes" : "No");
    }
}
