using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using InTandemRegistrationPortal.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InTandemRegistrationPortal.Authorization
{
    public class RegisterAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, RideEvent>
    {
        protected readonly UserManager<InTandemUser> _userManager;

        public RegisterAuthorizationHandler(UserManager<InTandemUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement, RideEvent resource)
        {
            if (context.User == null || resource == null)
                return Task.CompletedTask;

            if (requirement.Name != Constants.RegisterOperationName)
                return Task.CompletedTask;

            if (context.User.IsInRole(Constants.AdministratorsRole))
                return Task.CompletedTask;

            var userId = _userManager.GetUserId(context.User);
            if (!resource.RideLeaderAssignments.Any(x => x.InTandemUserID == userId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
