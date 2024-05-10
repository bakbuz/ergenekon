namespace Ergenekon.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
