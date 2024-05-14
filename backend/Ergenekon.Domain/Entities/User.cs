namespace Ergenekon.Domain.Entities;

public class UserInfo
{
    public UserInfo()
    {
    }

    public UserInfo(Guid userId)
    {
        Id = userId;
    }

    public Guid Id { get; set; }

    //[Column(Order = 94)]
    public UserStatus Status { get; set; }

    //[Column(Order = 95)]
    public string? FirstName { get; set; }

    //[Column(Order = 96)]
    public string? LastName { get; set; }

    //[Column(Order = 97)]
    public string? Image { get; set; }

    //[Column(Order = 98)]
    public string? Bio { get; set; }

    //[Column(Order = 99)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //public virtual ApplicationUser { get; set; }
}

public class UserFollow
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public static class UserExtensions
{
    public static string GetDisplayName(this UserInfo user)
    {
        return $"{user.FirstName} {user.LastName}".Trim();
    }
}