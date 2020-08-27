using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Filters
{
    public class LoggedUserHandler : AuthorizationHandler<LoggedUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedUserRequirement requirement)
        {
            if (!context.User.Claims.Any(c => c.Type == "username"))
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
