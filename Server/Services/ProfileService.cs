using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Server.Services
{
    public class ProfileService: IProfileService
    {
        protected UserManager<IdentityUser> _userManager;
        private IUserClaimsPrincipalFactory<IdentityUser> _userClaimsPrincipalFactory;

        public ProfileService(UserManager<IdentityUser> userManager,
            IUserClaimsPrincipalFactory<IdentityUser> userClaimsPrincipalFactory)
            => (_userManager, _userClaimsPrincipalFactory) = (userManager, userClaimsPrincipalFactory);

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            IdentityUser user = await _userManager.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            claims.Add(new Claim(JwtClaimTypes.Id, sub));

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            IdentityUser user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
