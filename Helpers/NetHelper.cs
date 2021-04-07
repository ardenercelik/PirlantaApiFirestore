using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PirlantaApi.Helpers
{
    public static class NetHelper
    {
        public static  Object CreateErrorMessage(string message)
        {
            return new
            {
                success= false,
                message = message
            };
        }

        public static Object CreateSuccessMesseage(string message)
        {
            return new
            {
                success = true,
                message = message
            };
        }
        public static string GetClaim(ClaimsPrincipal principal, string key)
        {
            return principal?.Claims?.SingleOrDefault(p => p.Type == key)?.Value;
        }
    }
}
