using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Filters
{
    public class LoggedUserWithCityHandler : AuthorizationHandler<LoggedUserWithCityRequirement>
    {
        private readonly LoggedUser _loggedUser;

        public LoggedUserWithCityHandler(LoggedUser loggedUser)
        {
            _loggedUser = loggedUser;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedUserWithCityRequirement requirement)
        {
            if (string.IsNullOrWhiteSpace(_loggedUser.UserName))
                return Task.CompletedTask;

            if (string.IsNullOrWhiteSpace(_loggedUser.City))
                return Task.CompletedTask;

            context.Succeed(requirement);
            return Task.CompletedTask;

        }
    }
}
