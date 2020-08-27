using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API.Hubs
{
    //Authorize is necessary on hub to have jwt bearer claims in context.user
    [Authorize]
    public class WeatherNowByCityHub : Hub
    {
        [Authorize(Policy = "LoggedUserWithCityHub")]
        public Task ChangeWeather(string weather, string city)
        {
            return Clients.Group(city).SendAsync("ChangeWeather", weather);
        }

        public Task AbortConnection()
        {
            //Client can reconnect, must stop at authorize.
            Context.Abort();

            return Task.CompletedTask;
        }

        public override Task OnConnectedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Context?.User?.Claims?.FirstOrDefault(c => c.Type == "city")?.Value))
                Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Claims.FirstOrDefault(c => c.Type == "city").Value);

            return base.OnConnectedAsync();
        }
    }
}
