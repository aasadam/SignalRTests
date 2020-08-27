using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRTests.API.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API
{
    /// <summary>
    /// User will need to be passaed down as parameter from hub
    /// </summary>
    public class LoggedUser
    {
        //DO NOT WORK WITH SIGNALR
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public LoggedUser(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public string City
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_city))
                    return _city;

                return (string)_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "city")?.Value;
            }
        }

        public string UserName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_username))
                    return _username;

                return (string)_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "username")?.Value;
            }
        }

        private string _username;
        private string _city;
        internal void setData(string username, string city)
        {
            _username = username;
            _city = city;
        }
    }
}
