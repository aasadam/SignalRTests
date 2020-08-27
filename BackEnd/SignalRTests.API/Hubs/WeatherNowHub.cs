using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Hubs
{
    [Authorize(Policy = "LoggedUser")]
    public class WeatherNowHub : Hub
    {
        public WeatherNowHub()
        {

        }

        public Task ChangeWeather(string weather)
        {
            return Clients.All.SendAsync("ChangeWeather", weather);
        }
    }
}
