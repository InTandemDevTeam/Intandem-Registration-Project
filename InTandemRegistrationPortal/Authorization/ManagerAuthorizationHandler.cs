using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using InTandemRegistrationPortal.Models;

namespace InTandemRegistrationPortal.Authorization
{
    public class ManagerAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, RideEvents>
    {
        protected readonly UserManager<InTandemUser> _userManager;

        public ManagerAuthorizationHandler(UserManager<InTandemUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            OperationAuthorizationRequirement requirement, RideEvents resource)
        {
            if (context.User == null || resource == null)
                return Task.CompletedTask;

            if (requirement.Name != Constants.UpdateOperationName)
                return Task.CompletedTask;

            var userId = _userManager.GetUserId(context.User);
            if (context.User.IsInRole(Constants.AdministratorsRole) || resource.RideLeaderAssignments.Any(x => x.InTandemUserId == userId))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
