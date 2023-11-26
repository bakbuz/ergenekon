using CsvHelper;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.TodoLists.Queries.ExportTodos;
using Ergenekon.Infrastructure.Files.Maps;
using System.Globalization;

namespace Ergenekon.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
