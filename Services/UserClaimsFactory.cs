using InfalibleRealEstate.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace InfalibleRealEstate.Services;

public class UserClaimsFactory : UserClaimsPrincipalFactory<ApplicationUser>
{
    public UserClaimsFactory(
        UserManager<ApplicationUser> userManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);
        if (!string.IsNullOrEmpty(user.Nombre))
        {
            identity.AddClaim(new Claim("nombre", user.Nombre));
        }
        if (!string.IsNullOrEmpty(user.Apellido))
        {
            identity.AddClaim(new Claim("apellido", user.Apellido));
        }
        var roles = await UserManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return identity;
    }
}