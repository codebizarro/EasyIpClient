namespace EasyIpClient.Model
{
    public class EasyIpPacket
    {
        public char Flags;
        public char Error;
        public short Counter;
        public short ShortIndex;
        public char Spare1;
        public char SendDataType;
        public short SendDataSize;
        public short SendDataOffset;
        public char Spare2;
        public char ReqDataType;
        public short ReqDataSize;
        public short ReqDataOffsetServer;
        public short ReqDataOffsetClient;
        public short[] Data = new short[256];
    }
}
