namespace Ergenekon.Application.FunctionalTests.TodoLists.Queries;

using Ergenekon.Application.TodoLists.Queries.GetTodos;
using Ergenekon.Domain.Entities;
using Ergenekon.Domain.ValueObjects;
using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new TodoList
        {
            Name = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new TodoItem { Name = "Apples", Done = true },
                        new TodoItem { Name = "Milk", Done = true },
                        new TodoItem { Name = "Bread", Done = true },
                        new TodoItem { Name = "Toilet paper" },
                        new TodoItem { Name = "Pasta" },
                        new TodoItem { Name = "Tissues" },
                        new TodoItem { Name = "Tuna" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);

        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
