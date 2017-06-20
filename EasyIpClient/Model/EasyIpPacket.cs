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
        public short Counter;
        /// <summary>
        /// 1 byte
        /// Reserved
        /// Set to 0
        /// </summary>
        public short ShortIndex;
        /// <summary>
        /// ???
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
        public byte SendDataType;
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
        public byte ReqDataType;
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
    }
}
