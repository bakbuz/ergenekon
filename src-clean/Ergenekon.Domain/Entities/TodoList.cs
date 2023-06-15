using Ergenekon.Domain.Common;
using Ergenekon.Domain.ValueObjects;

namespace Ergenekon.Domain.Entities
{
    public class TodoList : BaseAuditableEntity
    {
        public string? Title { get; set; }
        public Colour Colour { get; set; } = Colour.White;
        //public System.Drawing.Color Color { get; set; } = System.Drawing.Color.White;

        public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
    }
}
