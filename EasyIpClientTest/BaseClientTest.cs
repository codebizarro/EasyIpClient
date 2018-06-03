using System.Net.EasyIp;
using System.Net.EasyIp.Interfaces;

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
