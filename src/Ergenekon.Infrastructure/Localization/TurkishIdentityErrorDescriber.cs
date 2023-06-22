using Microsoft.AspNetCore.Identity;

namespace Ergenekon.Infrastructure.Localization;

public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
{
    /// <summary>
    /// Returns the default <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>The default <see cref="IdentityError"/>.</returns>
    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = nameof(DefaultError),
            Description = Resources.DefaultError
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a concurrency failure.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a concurrency failure.</returns>
    public override IdentityError ConcurrencyFailure()
    {
        return new IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = Resources.ConcurrencyFailure
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password mismatch.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a password mismatch.</returns>
    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = Resources.PasswordMismatch
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating an invalid token.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating an invalid token.</returns>
    public override IdentityError InvalidToken()
    {
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = Resources.InvalidToken
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a recovery code was not redeemed.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a recovery code was not redeemed.</returns>
    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        return new IdentityError
        {
            Code = nameof(RecoveryCodeRedemptionFailed),
            Description = Resources.RecoveryCodeRedemptionFailed
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating an external login is already associated with an account.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating an external login is already associated with an account.</returns>
    public override IdentityError LoginAlreadyAssociated()
    {
        return new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = Resources.LoginAlreadyAssociated
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified user <paramref name="userName"/> is invalid.
    /// </summary>
    /// <param name="userName">The user name that is invalid.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specified user <paramref name="userName"/> is invalid.</returns>
    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = string.Format(Resources.Culture, Resources.InvalidUserName, userName)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is invalid.
    /// </summary>
    /// <param name="email">The email that is invalid.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is invalid.</returns>
    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = string.Format(Resources.Culture, Resources.InvalidEmail, email)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="userName"/> already exists.
    /// </summary>
    /// <param name="userName">The user name that already exists.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="userName"/> already exists.</returns>
    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = string.Format(Resources.Culture, Resources.DuplicateUserName, userName)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is already associated with an account.
    /// </summary>
    /// <param name="email">The email that is already associated with an account.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specified <paramref name="email"/> is already associated with an account.</returns>
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = string.Format(Resources.Culture, Resources.DuplicateEmail, email)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="role"/> name is invalid.
    /// </summary>
    /// <param name="role">The invalid role.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specific role <paramref name="role"/> name is invalid.</returns>
    public override IdentityError InvalidRoleName(string? role)
    {
        return new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = string.Format(Resources.Culture, Resources.InvalidRoleName, role)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating the specified <paramref name="role"/> name already exists.
    /// </summary>
    /// <param name="role">The duplicate role.</param>
    /// <returns>An <see cref="IdentityError"/> indicating the specific role <paramref name="role"/> name already exists.</returns>
    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = string.Format(Resources.Culture, Resources.DuplicateRoleName, role)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a user already has a password.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a user already has a password.</returns>
    public override IdentityError UserAlreadyHasPassword()
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = Resources.UserAlreadyHasPassword
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating user lockout is not enabled.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating user lockout is not enabled.</returns>
    public override IdentityError UserLockoutNotEnabled()
    {
        return new IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = Resources.UserLockoutNotEnabled
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a user is already in the specified <paramref name="role"/>.
    /// </summary>
    /// <param name="role">The duplicate role.</param>
    /// <returns>An <see cref="IdentityError"/> indicating a user is already in the specified <paramref name="role"/>.</returns>
    public override IdentityError UserAlreadyInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = string.Format(Resources.Culture, Resources.UserAlreadyInRole, role)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a user is not in the specified <paramref name="role"/>.
    /// </summary>
    /// <param name="role">The duplicate role.</param>
    /// <returns>An <see cref="IdentityError"/> indicating a user is not in the specified <paramref name="role"/>.</returns>
    public override IdentityError UserNotInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = string.Format(Resources.Culture, Resources.UserNotInRole, role)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password of the specified <paramref name="length"/> does not meet the minimum length requirements.
    /// </summary>
    /// <param name="length">The length that is not long enough.</param>
    /// <returns>An <see cref="IdentityError"/> indicating a password of the specified <paramref name="length"/> does not meet the minimum length requirements.</returns>
    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = string.Format(Resources.Culture, Resources.PasswordTooShort, length)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password does not meet the minimum number <paramref name="uniqueChars"/> of unique chars.
    /// </summary>
    /// <param name="uniqueChars">The number of different chars that must be used.</param>
    /// <returns>An <see cref="IdentityError"/> indicating a password does not meet the minimum number <paramref name="uniqueChars"/> of unique chars.</returns>
    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = string.Format(Resources.Culture, Resources.PasswordRequiresUniqueChars, uniqueChars)
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a non-alphanumeric character, which is required by the password policy.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a non-alphanumeric character.</returns>
    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = Resources.PasswordRequiresNonAlphanumeric
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a numeric character, which is required by the password policy.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a numeric character.</returns>
    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = Resources.PasswordRequiresDigit
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain a lower case letter, which is required by the password policy.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain a lower case letter.</returns>
    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = Resources.PasswordRequiresLower
        };
    }

    /// <summary>
    /// Returns an <see cref="IdentityError"/> indicating a password entered does not contain an upper case letter, which is required by the password policy.
    /// </summary>
    /// <returns>An <see cref="IdentityError"/> indicating a password entered does not contain an upper case letter.</returns>
    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = Resources.PasswordRequiresUpper
        };
    }
}
