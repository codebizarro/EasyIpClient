using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Channel;
using EasyIpClient.Constants;
using EasyIpClient.Model;
using EasyIpClient.Enums;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelBenchmarkTest: BaseTest
    {

        [TestMethod]
        public void ExecuteReadWriteBenchmarkTest()
        {
            IChannel client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
            var writePacket = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataSize = SEND_DATA_SIZE,
                SendDataOffset = 0,
                SendDataType = DataTypeEnum.FlagWord,
                ReqDataSize = 0,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };
            var readPacket = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataType = 0,
                SendDataSize = 0,
                SendDataOffset = 0,
                ReqDataType = DataTypeEnum.FlagWord,
                ReqDataSize = RECEIVE_DATA_SIZE,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                writePacket.Data[0] = (short)i;
                var response = client.Execute(writePacket.BuildRequest());
                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);

                response = client.Execute(readPacket.BuildRequest());
                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
                Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                Assert.AreEqual(readPacket.ReqDataSize, response[14]);

                ushort testValue = response[21];
                testValue <<= 8;
                testValue |= response[20];
                Assert.AreEqual(i, testValue);
            }
        }

        [TestMethod]
        public void ExecuteReadBenchmarkTest()
        {
            IChannel client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
            var readPacket = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataType = 0,
                SendDataSize = 0,
                SendDataOffset = 0,
                ReqDataType = DataTypeEnum.FlagWord,
                ReqDataSize = RECEIVE_DATA_SIZE,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                var response = client.Execute(readPacket.BuildRequest());
                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
                Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                Assert.AreEqual(readPacket.ReqDataSize, response[14]);
            }
        }

        [TestMethod]
        public void ExecuteWriteBenchmarkTest()
        {
            IChannel client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
            var writePacket = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataSize = SEND_DATA_SIZE,
                SendDataOffset = 0,
                SendDataType = DataTypeEnum.FlagWord,
                ReqDataSize = 0,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                writePacket.Data[0] = (short)i;
                var response = client.Execute(writePacket.BuildRequest());
                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
            }
        }
    }
}
