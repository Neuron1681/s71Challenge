using System;
using System.Configuration;

namespace s71Challenge
{
    public static class AppConfigManager
    {
        public static string GetConfigString(string property, string defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
                return ConfigurationManager.AppSettings[property];
            return defVal;
        }
        public static int GetConfigInt(string property, int defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
            {
                try
                {
                    return int.Parse(ConfigurationManager.AppSettings[property]);
                }
                catch { }
            }
            return defVal;
        }
        public static bool GetConfigBool(string property, bool defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
            {
                try
                {
                    return bool.Parse(ConfigurationManager.AppSettings[property]);
                }
                catch { }
            }
            return defVal;
        }
        public static DateTime GetConfigDateTime(string property, DateTime defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
            {
                try
                {
                    return DateTime.Parse(ConfigurationManager.AppSettings[property]);
                }
                catch { }
            }
            return defVal;
        }
        public static TimeSpan GetConfigTimeSpan(string property, TimeSpan defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
            {
                try
                {
                    return TimeSpan.Parse(ConfigurationManager.AppSettings[property]);
                }
                catch { }
            }
            return defVal;
        }
        public static T GetConfigEnum<T>(string property, T defVal)
        {
            if (ConfigurationManager.AppSettings[property] != null)
            {
                try
                {
                    return (T)Enum.Parse(typeof(T), ConfigurationManager.AppSettings[property], true);
                }
                catch { }
            }
            return defVal;
        }
    }
}
