using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CommonLibrary
{
    public class Configuration
    {
        /// <summary>
        /// Map the configuration file "appsettings.json"
        /// </summary>
        /// <returns></returns>
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        /// <summary>
        /// Get Connection String of a specific Database from the configuration file
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string databaseName)
        {
            var configuration = GetConfiguration();
            return _ = configuration.GetSection("ConnectionStrings").GetSection(databaseName).Value;
        }
        /// <summary>
        /// Get config value of a specific key from configuration file
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetConfigValue(string item)
        {
            var configuration = GetConfiguration();
            return _ = configuration.GetSection("Config").GetSection(item).Value;
        }
        /// <summary>
        /// Get current environment whether Dev,Stg or Prod from configuration file
        /// </summary>
        /// <returns></returns>
        public static bool GetEnvironment()
        {
            var configuration = GetConfiguration();
            return Convert.ToBoolean(configuration.GetSection("Environment").GetSection("isProd").Value);
        }
    }
}
