using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.EasyIp.Enums;
using System.Net.EasyIp.Extensions;
using System.Net.EasyIp.Helpers;
using System.Net.EasyIp.Interfaces;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelTest: BaseChannelTest
    {
        protected IChannel _channel;

        [TestInitialize]
        public void ChannelTestInitialize()
        {
            _channel = GetChannelInstance();
        }

        [TestMethod]
        public void CreationTest()
        {
            Assert.IsNotNull(_channel);
            Assert.IsInstanceOfType(_channel, typeof(IChannel));
        }

        [TestMethod]
        public void ExecuteReadTest()
        {
            var readPacket = PacketFactory.GetReadPacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, RECEIVE_DATA_SIZE);
            var response = _channel.Execute(readPacket.ToByteArray());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
            Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
            Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToUInt16(response, 14));
        }

        [TestMethod]
        public void ExecuteWriteTest()
        {
            var writePacket = PacketFactory.GetWritePacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, SEND_DATA_SIZE);
            writePacket.Data[0] = 255;
            writePacket.Data[1] = 254;
            writePacket.Data[254] = 254;
            var response = _channel.Execute(writePacket.ToByteArray());

            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
        }
    }
}
