namespace Ergenekon.Domain.Consts;

public static class IdentityConsts
{
    public const int PasswordMinimumLength = 6;
    public const int PasswordMaximumLength = 36;
    public const string IdentitySchema = "Identity";
    public const string IdentityServerSchema = "IdentityServer";

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}