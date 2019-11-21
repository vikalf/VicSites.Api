using System;
using System.Collections.Generic;
using System.Text;

namespace VicSites.Common.Infrastructure
{
    public static class EnvironmentSettings
    {
        public static string GetEnvironmentVariable(string key, string defaultValue = null) 
        {
            try
            {
                var value = System.Environment.GetEnvironmentVariable(key);

                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
                else
                    return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
