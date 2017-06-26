using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.EasyIp.Interfaces;
using System.Net.EasyIp.Helpers;
using System.Net.EasyIp.Enums;
using System.Net.EasyIp.Extensions;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelBenchmarkTest : BaseChannelTest
    {
        protected IChannel _channel;

        [TestInitialize]
        public void ChannelTestInitialize()
        {
            _channel = GetChannelInstance();
        }


        [TestMethod]
        public async Task ExecuteReadWriteBenchmarkTestAsync()
        {
            var writePacket = PacketFactory.GetWritePacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);
            var readPacket = PacketFactory.GetReadPacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                await Task.Run(async () =>
                    {
                        writePacket.Data[0] = (short)i;
                        var response = await _channel.ExecuteAsync(writePacket.ToByteArray());

                        Assert.IsNotNull(response);
                        Assert.IsTrue(response[1] == 0);

                        response = await _channel.ExecuteAsync(readPacket.ToByteArray());

                        Assert.IsNotNull(response);
                        Assert.IsTrue(response[1] == 0);
                        Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                        Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToUInt16(response, 14));

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
            var writePacket = PacketFactory.GetWritePacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);
            var readPacket = PacketFactory.GetReadPacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);

            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {

                writePacket.Data[0] = (short)i;
                var response = _channel.Execute(writePacket.ToByteArray());

                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);

                response = _channel.Execute(readPacket.ToByteArray());

                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
                Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToUInt16(response, 14));

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
            var readPacket = PacketFactory.GetReadPacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);
            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                var response = _channel.Execute(readPacket.ToByteArray());

                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
                Assert.AreEqual((byte)readPacket.ReqDataType, response[13]);
                Assert.AreEqual(readPacket.ReqDataSize, BitConverter.ToUInt16(response, 14));
            }
        }

        [TestMethod]
        public void ExecuteWriteBenchmarkTest()
        {
            var writePacket = PacketFactory.GetWritePacket(REMOTE_OFFSET, DataTypeEnum.FlagWord, byte.MaxValue);
            for (int i = 0; i < BENCHMARK_COUNT; i++)
            {
                writePacket.Data[0] = (short)i;
                var response = _channel.Execute(writePacket.ToByteArray());

                Assert.IsNotNull(response);
                Assert.IsTrue(response[1] == 0);
            }
        }
    }
}
