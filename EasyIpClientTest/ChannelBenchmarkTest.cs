using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyIpClient.Channel.Interfaces;
using System;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelBenchmarkTest : BaseChannelTest
    {

        [TestMethod]
        public async Task ExecuteReadWriteBenchmarkTestAsync()
        {
            IChannel client = GetChannelInstance();
            var writePacket = GetWritePacket();
            var readPacket = GetReadPacket();

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                await Task.Run(async () =>
                    {
                        writePacket.Data[0] = (short)i;
                        var response = await client.ExecuteAsync(writePacket.BuildRequest());

                        Assert.IsNotNull(response);
                        Assert.IsTrue(response[1] == 0);

                        response = await client.ExecuteAsync(readPacket.BuildRequest());

                        Assert.IsNotNull(response);
                        Assert.IsTrue(response[1] == 0);
                        Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                        Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToInt16(response, 14));

                        ushort testValue = response[21];
                        testValue <<= 8;
                        testValue |= response[20];
                        Assert.AreEqual(i, testValue);
                        Debug.Write(string.Format($"{i} == {testValue}"));
                    }
                );
            }
        }

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
                Debug.Write(string.Format($"{i} == {testValue}"));
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
