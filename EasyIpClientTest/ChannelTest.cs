using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Channel;
using EasyIpClient.Constants;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelTest
    {
        [TestMethod]
        public void CreationTest()
        {
            var client = new UdpChannel(new IPEndPoint(IPAddress.Parse(Configuration.Address), Constants.EASYIP_PORT));
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(IChannel));
        }
    }
}
