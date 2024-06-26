﻿using Ergenekon.Application.Common.Exceptions;
using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using Ergenekon.Domain.Enums;

namespace Ergenekon.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .Where(q => q.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id.ToString());
        }

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
