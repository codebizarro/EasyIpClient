using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyIpClient.Channel.Interfaces;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelTest: BaseTest
    {
        [TestMethod]
        public void CreationTest()
        {
            var client = GetChannelInstance();

            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(IChannel));
        }

        [TestMethod]
        public void ExecuteReadTest()
        {
            IChannel client = GetChannelInstance();
            var packet = GetReadPacket();
            var response = client.Execute(packet.BuildRequest());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
            Assert.AreEqual((byte)packet.ReqDataType, response[13]);
            Assert.AreEqual(packet.ReqDataSize, response[14]);
        }

        [TestMethod]
        public void ExecuteWriteTest()
        {
            IChannel client = GetChannelInstance();
            var packet = GetWritePacket();
            packet.Data[0] = 255;
            packet.Data[1] = 254;
            packet.Data[255] = 255;
            var response = client.Execute(packet.BuildRequest());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
        }
    }
}
