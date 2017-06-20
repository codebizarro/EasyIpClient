namespace EasyIpClient.Channel.Interfaces
{
    public interface IChannel
    {
        byte[] Execute(byte[] buffer);

        int SendTimeout
        {
            get;
            set;
        }

        int ReceiveTimeout
        {
            get;
            set;
        }
    }
}
