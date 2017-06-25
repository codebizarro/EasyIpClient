using System.Net.EasyIp;
using System.Net.EasyIp.Interfaces;

namespace EasyIpClientTest
{
    public class BaseChannelTest
    {
        protected const int BENCHMARK_COUNT = 10;
        protected const short SEND_DATA_SIZE = 256;
        protected const short RECEIVE_DATA_SIZE = 256;
        protected const short REMOTE_OFFSET = 9744;
        public const int EASYIP_PORT = 995;

        protected IChannel GetChannelInstance()
        {
            var channel = new UdpChannel(Configuration.Address, EASYIP_PORT);
            channel.SendTimeout = 100;
            channel.ReceiveTimeout = 100;
            return channel;
        }
    }
}
