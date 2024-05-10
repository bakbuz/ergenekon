using Ergenekon.Domain.Entities;

namespace Ergenekon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<UserInfo> UserInfos { get;  }
    DbSet<UserFollow> UserFollows { get;  }

    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
