using Ergenekon.Application.Common.Interfaces;
using Ergenekon.Application.Common.Models;
using Ergenekon.Application.Users.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ergenekon.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
    }

    public async Task<UserSummaryDto?> GetUserAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
            return null;

        return new UserSummaryDto
        {
            Username = user.UserName
        };
    }

    public async Task<string?> GetUsernameAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
            return null;

        return user.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role, CancellationToken cancellationToken)
    {
        var user =await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId,cancellationToken);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName, CancellationToken cancellationToken)
    {
        var user =await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId,cancellationToken);
        if (user == null)
            return false;

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
            return Result.Failure(new List<string> { "Kullanıcı bulunamadı" });

        user.Status = UserStatus.Deleted;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }

    //public async Task<Result> DeleteUserAsync(ApplicationUser user)
    //{
    //    var result = await _userManager.DeleteAsync(user);

    //    return result.ToApplicationResult();
    //}
}
