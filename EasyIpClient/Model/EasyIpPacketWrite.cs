namespace EasyIpClient.Model
{
    public class EasyIpPacketWrite : EasyIpPacket
    {
        /// <summary>
        /// N*2 bytes
        /// Data send by client or requested data
        /// </summary>
        public short[] Data = new short[256];
    }
}
