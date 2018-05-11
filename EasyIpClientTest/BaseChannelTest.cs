using System.Net.EasyIp;
using System.Net.EasyIp.Interfaces;

namespace EasyIpClientTest
{
    public class BaseChannelTest
    {
        protected const int BENCHMARK_COUNT = 2;
        protected const byte SEND_DATA_SIZE = byte.MaxValue;
        protected const byte RECEIVE_DATA_SIZE = byte.MaxValue;
        protected const short REMOTE_OFFSET = 5000;
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
