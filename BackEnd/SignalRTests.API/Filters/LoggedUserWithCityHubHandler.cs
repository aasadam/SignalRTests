using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Filters
{
    public class LoggedUserWithCityHubHandler : AuthorizationHandler<LoggedUserWithCityHubRequirement, HubInvocationContext>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedUserWithCityHubRequirement requirement, HubInvocationContext resource)
        {
            if (!resource.Context.User.Claims.Any(c => c.Type == "username"))
                return Task.CompletedTask;

            if (!resource.Context.User.Claims.Any(c => c.Type == "city"))
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
