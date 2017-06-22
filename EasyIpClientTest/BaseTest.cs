using EasyIpClient.Channel;
using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Constants;
using EasyIpClient.Enums;
using EasyIpClient.Model;

namespace EasyIpClientTest
{
    public class BaseTest
    {
        protected const int BENCHMARK_COUNT = 1000;
        protected const short SEND_DATA_SIZE = 200;
        protected const short RECEIVE_DATA_SIZE = 200;

        protected IChannel GetChannelInstance()
        {
            return new UdpChannel(Configuration.Address, Constants.EASYIP_PORT);
        }

        protected EasyIpPacket GetReadPacket()
        {
            return new EasyIpPacket
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
        }

        protected EasyIpPacket GetWritePacket()
        {
            return new EasyIpPacket
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
        }
    }
}
