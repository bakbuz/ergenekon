namespace Ergenekon.Domain.Consts;

public static class IdentityConsts
{
    public const int PasswordMinimumLength = 6;
    public const int PasswordMaximumLength = 36;
    public const string IdentitySchema = "Identity";
    public const string IdentityServerSchema = "IdentityServer";

    public const string DefaultUserName = "bayram";
    public const string DefaultUserEmail = "bayram@maydere.com";
    public const string DefaultUserPass = "Ab123,,";

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
    }
}