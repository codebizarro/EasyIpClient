using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Channel;
using EasyIpClient.Constants;
using EasyIpClient.Model;
using EasyIpClient.Enums;
using System;

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

        [TestMethod]
        public void ExecuteReadTest()
        {
            IChannel client = new UdpChannel(new IPEndPoint(IPAddress.Parse(Configuration.Address), Constants.EASYIP_PORT));
            var packet = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataType = 0,
                SendDataSize = 0,
                SendDataOffset = 0,
                ReqDataType = DataTypeEnum.FlagWord,
                ReqDataSize = 20,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };
            
            //client.Execute()
            
        }
    }
}
