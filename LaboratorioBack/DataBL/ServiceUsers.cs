using Microsoft.AspNetCore.Identity;

namespace LaboratorioBack.DataBL
{
    public interface IServiceUsers
    {
        Task<string?> GetUserId();
    }

    public class ServiceUsers : IServiceUsers
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;

        public ServiceUsers(IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }


        public async Task<string?> GetUserId()
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                return null;
            }
            var user = httpContext.User;
            if (user is null)
            {
                return null;
            }
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type == "email");
            if (emailClaim is null)
            {
                return null;
            }
            var email = emailClaim.Value;
            var userIdentity = await userManager.FindByEmailAsync(email);
            if (userIdentity is null)
            {
                return null;
            }
            return userIdentity.Id;
        }

    }
}
