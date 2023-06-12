using Ergenekon.Domain.Common;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}
