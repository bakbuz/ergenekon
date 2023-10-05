using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Domain.Entities;
using MediatR;

namespace Ergenekon.Application.Logging.Commands.CreateRequestLog;

public record CreateRequestLogCommand(RequestLog RequestLog) : IRequest
{
}

public class CreateRequestLogCommandHandler : IRequestHandler<CreateRequestLogCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateRequestLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateRequestLogCommand request, CancellationToken cancellationToken)
    {
        _context.RequestLogs.Add(request.RequestLog);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
