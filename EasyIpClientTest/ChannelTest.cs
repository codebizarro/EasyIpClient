using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyIpClient.Channel.Interfaces;
using System;
using EasyIpClient.Helpers;
using EasyIpClient.Extensions;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelTest: BaseChannelTest
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
            var readPacket = PacketFactory.GetReadPacket(REMOTE_OFFSET, EasyIpClient.Enums.DataTypeEnum.FlagWord, byte.MaxValue);
            var response = client.Execute(readPacket.ToByteArray());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
            Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
            Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToUInt16(response, 14));
        }

        [TestMethod]
        public void ExecuteWriteTest()
        {
            IChannel client = GetChannelInstance();
            var writePacket = PacketFactory.GetWritePacket(REMOTE_OFFSET, EasyIpClient.Enums.DataTypeEnum.FlagWord, byte.MaxValue);
            writePacket.Data[0] = 255;
            writePacket.Data[1] = 254;
            writePacket.Data[254] = 254;
            var response = client.Execute(writePacket.ToByteArray());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
        }
    }
}
