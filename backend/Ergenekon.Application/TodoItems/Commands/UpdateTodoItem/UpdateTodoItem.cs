﻿using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.TodoItems.Commands.UpdateTodoItem;

public record UpdateTodoItemCommand : IRequest
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }
}

public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(TodoItem), request.Id.ToString());

        entity.Title = request.Title;
        entity.Done = request.Done;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
