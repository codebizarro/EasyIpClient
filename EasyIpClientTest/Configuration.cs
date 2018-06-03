using System.Configuration;

namespace EasyIpClientTest
{
    public static class Configuration
    {
        public static string Address
        {
            get
            {
                return ConfigurationManager.AppSettings["address"];
            }
        }
    }
}
