using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.EasyIp;
using System.Net.EasyIp.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace EasyIpClientTest
{
    public class BaseClientTest: BaseChannelTest
    {
        protected IEasyIpClient GetClientInstance()
        {
            return new EasyIpClient(GetChannelInstance());
        }
    }
}
