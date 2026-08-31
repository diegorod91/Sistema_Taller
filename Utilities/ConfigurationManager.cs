using System.Collections.Specialized;

namespace Utilities
{
    public class ConfigurationManager
    {
        public static NameValueCollection AppSettings { get { return System.Configuration.ConfigurationManager.AppSettings == null ? null : System.Configuration.ConfigurationManager.AppSettings; } private set { } }
    }
}
