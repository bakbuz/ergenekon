using Ergenekon.Application.Common.Mappings;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
