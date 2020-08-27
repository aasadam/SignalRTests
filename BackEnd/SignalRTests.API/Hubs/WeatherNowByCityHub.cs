using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Hubs
{
    [Authorize(Policy = "LoggedUserWithCity")]
    public class WeatherNowByCityHub : Hub
    {
        private readonly LoggedUser _loggedUser;

        public WeatherNowByCityHub(LoggedUser loggedUser)
        {
            _loggedUser = loggedUser;
        }

        public Task ChangeWeather(string weather, string city)
        {
            return Clients.Group(city).SendAsync("ChangeWeather", weather);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId, _loggedUser.City);


            return base.OnConnectedAsync();
        }
    }
}
