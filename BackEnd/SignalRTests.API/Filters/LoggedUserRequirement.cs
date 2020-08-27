using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Filters
{
    public class LoggedUserRequirement : IAuthorizationRequirement
    {
    }
}
