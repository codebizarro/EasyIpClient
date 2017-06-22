using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Channel;
using EasyIpClient.Constants;
using EasyIpClient.Model;
using EasyIpClient.Enums;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace EasyIpClientTest
{
    [TestClass]
    public class ChannelTest
    {
        [TestMethod]
        public void CreationTest()
        {
            var client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
            Assert.IsNotNull(client);
            Assert.IsInstanceOfType(client, typeof(IChannel));
        }

        byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                var ret = stream.ToArray();
                return ret;
            }
        }

        [TestMethod]
        public void ExecuteReadTest()
        {
            IChannel client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
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

            var response = client.Execute(packet.BuildRequest());
            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
            Assert.AreEqual((byte)packet.ReqDataType, response[13]);
            Assert.AreEqual(packet.ReqDataSize, response[14]);
        }

        [TestMethod]
        public void ExecuteWriteTest()
        {
            IChannel client = new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
            var packet = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataSize = 2,
                SendDataOffset = 0,
                SendDataType = DataTypeEnum.FlagWord,
                ReqDataSize = 0,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };
            packet.Data[0] = 255;
            packet.Data[1] = 254;
            packet.Data[255] = 255;
            var response = client.Execute(packet.BuildRequest());
            Assert.IsNotNull(response);
            Assert.IsTrue(response[1] == 0);
        }

        //[TestMethod]
        public void ExecuteReadWriteBenchmarkTest()
        {
            IChannel client = new UdpChannel(new IPEndPoint(IPAddress.Parse(Configuration.Address), Constants.EASYIP_PORT));
            var writePacket = new EasyIpPacket
            {
                Flags = 0,
                Error = 0,
                Counter = 0, // Must increment in client
                SendDataSize = 2,
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
                ReqDataSize = 20,
                ReqDataOffsetServer = 0,
                ReqDataOffsetClient = 0
            };

            for (int i = 0; i < 1000 /*ushort.MaxValue*/; i++)
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
    }
}
