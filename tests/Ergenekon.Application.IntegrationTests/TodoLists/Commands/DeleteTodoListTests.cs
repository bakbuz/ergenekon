using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.TodoLists.Commands.CreateTodoList;
using Ergenekon.Application.TodoLists.Commands.DeleteTodoList;
using Ergenekon.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Ergenekon.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
