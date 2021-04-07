using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirlantaApi.Helpers
{
    public static class AppConstants
    {
        public static  int PageSize = int.Parse(Environment.GetEnvironmentVariable("PAGE_SIZE"));
        public static string ProjectId = Environment.GetEnvironmentVariable("PROJECT_ID");
    }
}
