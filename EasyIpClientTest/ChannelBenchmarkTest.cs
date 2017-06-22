using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyIpClient.Channel.Interfaces;
using System;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelBenchmarkTest: BaseTest
    {

        [TestMethod]
        public void ExecuteReadWriteBenchmarkTest()
        {
            IChannel client = GetChannelInstance();
            var writePacket = GetWritePacket();
            var readPacket = GetReadPacket();

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
                Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToInt16(response, 14));

                ushort testValue = response[21];
                testValue <<= 8;
                testValue |= response[20];
                Assert.AreEqual(i, testValue);
            }
        }

        [TestMethod]
        public void ExecuteReadBenchmarkTest()
        {
            IChannel client = GetChannelInstance();
            var readPacket = GetReadPacket();
            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                var response = client.Execute(readPacket.BuildRequest());

                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
                Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToInt16(response, 14));
            }
        }

        [TestMethod]
        public void ExecuteWriteBenchmarkTest()
        {
            IChannel client = GetChannelInstance();
            var writePacket = GetWritePacket();
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
