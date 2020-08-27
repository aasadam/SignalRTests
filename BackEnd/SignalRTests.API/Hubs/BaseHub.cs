using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Hubs
{
    public class BaseHub : Hub
    {
        protected readonly LoggedUser _loggedUser;

        public BaseHub(LoggedUser loggedUser)
        {
            _loggedUser = loggedUser;
            //Context not set in ctor
            _loggedUser.setData(Context?.User?.Claims?.FirstOrDefault(c => c.Type == "username")?.Value,
                                Context?.User?.Claims?.FirstOrDefault(c => c.Type == "city")?.Value);
        }
    }
}
