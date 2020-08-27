using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRTests.API
{
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
                return (string)_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "city")?.Value;
            }
        }

        public string UserName
        {
            get
            {
                return (string)_httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "username")?.Value;
            }
        }
    }
}
