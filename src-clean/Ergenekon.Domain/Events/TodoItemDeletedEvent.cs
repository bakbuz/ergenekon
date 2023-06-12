using Ergenekon.Domain.Common;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
