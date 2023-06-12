using Ergenekon.Domain.Common;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
