using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyIpClientTest
{
    public class Configuration
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
