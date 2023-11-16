using Microsoft.Extensions.Configuration;
using System;

namespace BlazorSongList.Utility
{
    public class ConfigHelper
    {
        private static readonly IConfiguration _config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, false).Build();

        // Connection to User DB
        public static string Db
        {
            get
            {
                return _config?["ConnectionStrings:DefaultConnection"] ?? throw new InvalidOperationException("Connection string is null.");
            }
        }
    }
}
