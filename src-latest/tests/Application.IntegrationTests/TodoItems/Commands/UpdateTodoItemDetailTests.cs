using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.TodoItems.Commands.CreateTodoItem;
using Ergenekon.Application.TodoItems.Commands.UpdateTodoItem;
using Ergenekon.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Ergenekon.Application.TodoLists.Commands.CreateTodoList;
using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Ergenekon.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class UpdateTodoItemDetailTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new UpdateTodoItemCommand { Id = 99, Title = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateTodoItem()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        var command = new UpdateTodoItemDetailCommand
        {
            Id = itemId,
            ListId = listId,
            Note = "This is the note.",
            Priority = PriorityLevel.High
        };

        await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Note.Should().Be(command.Note);
        item.Priority.Should().Be(command.Priority);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModifiedAt.Should().NotBeNull();
        item.LastModifiedAt.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
