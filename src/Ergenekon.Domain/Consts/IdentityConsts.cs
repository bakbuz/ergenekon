namespace Ergenekon.Domain.Consts;

public struct IdentityConsts
{
    public const int PasswordMinimumLength = 6;
    public const string IdentitySchema = "Identity";
    public const string IdentityServerSchema = "IdentityServer";

    public struct RoleNames
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}
