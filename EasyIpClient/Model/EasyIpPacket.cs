using EasyIpClient.Enums;
using System;

namespace EasyIpClient.Model
{
    public class EasyIpPacket
    {
        /// <summary>
        /// 1 byte
        /// Bit 0 information packet (request or response)
        /// Bit 1-2 bit operations 1
        /// Bit 6 do not respond
        /// Bit 7 response packet
        /// </summary>
        public byte Flags;
        /// <summary>
        /// 1 byte
        /// Only used in response packets
        /// 0: no error
        /// 1: operand type error
        /// 2: offset error
        /// 4: size error
        /// 16: no support
        /// </summary>
        public byte Error;
        /// <summary>
        /// 4 bytes
        /// Set by client, copied by server
        /// </summary>
        public int Counter;
        /// <summary>
        /// 1 byte
        /// Reserved
        /// Set to 0
        /// </summary>
        public byte Spare1;
        /// <summary>
        /// 1 byte
        /// Type of operand, some types may not be available
        /// 1 memory word
        /// 2 input word
        /// 3 output word
        /// 4 register
        /// 5 timer
        /// 11 strings3
        /// </summary>
        public DataTypeEnum SendDataType;
        /// <summary>
        /// 2 bytes
        /// Number of words
        /// </summary>
        public short SendDataSize;
        /// <summary>
        /// 2 bytes
        /// Target offset in server
        /// </summary>
        public short SendDataOffset;
        /// <summary>
        /// 1 byte
        /// Reserved
        /// Set to 0
        /// </summary>
        public byte Spare2;
        /// <summary>
        /// 1 byte
        /// Type of operand, some types may not be available (see senddata type for list of types)
        /// </summary>
        public DataTypeEnum ReqDataType;
        /// <summary>
        /// 2 bytes
        /// Number of words
        /// </summary>
        public short ReqDataSize;
        /// <summary>
        /// 2 bytes
        /// Offset in server
        /// </summary>
        public short ReqDataOffsetServer;
        /// <summary>
        /// 2 bytes
        /// Target offset in client
        /// </summary>
        public short ReqDataOffsetClient;
        /// <summary>
        /// N*2 bytes
        /// Data send by client or requested data
        /// </summary>
        public short[] Data = new short[256];

        private byte[] _buffer = new byte[532];

        public byte[] BuildRequest()
        {
            _buffer = new byte[20 + SendDataSize * 2];
            _buffer[0] = Flags;
            _buffer[1] = Error;
            var counter = BitConverter.GetBytes(Counter);
            _buffer[2] = counter[0];
            _buffer[3] = counter[1];
            _buffer[4] = counter[2];
            _buffer[5] = counter[3];
            _buffer[7] = (byte)SendDataType;
            var sendDataSize = BitConverter.GetBytes(SendDataSize);
            _buffer[8] = sendDataSize[0];
            _buffer[9] = sendDataSize[1];
            var sendDataOffset = BitConverter.GetBytes(SendDataOffset);
            _buffer[10] = sendDataOffset[0];
            _buffer[11] = sendDataOffset[1];
            _buffer[13] = (byte)ReqDataType;
            var reqDataSize = BitConverter.GetBytes(ReqDataSize);
            _buffer[14] = reqDataSize[0];
            _buffer[15] = reqDataSize[1];
            var reqDataOffsetServer = BitConverter.GetBytes(ReqDataOffsetServer);
            _buffer[16] = reqDataOffsetServer[0];
            _buffer[17] = reqDataOffsetServer[1];
            var reqDataOffsetClient = BitConverter.GetBytes(ReqDataOffsetClient);
            _buffer[18] = reqDataOffsetClient[0];
            _buffer[19] = reqDataOffsetClient[1];

            if (SendDataSize > 0)
            {
                Buffer.BlockCopy(Data, 0, _buffer, 20, SendDataSize * 2);
            }
            return _buffer;
        }
    }
}
