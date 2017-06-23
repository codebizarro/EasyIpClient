using System;
using System.Threading.Tasks;

namespace EasyIpClient.Channel.Interfaces
{
    public interface IChannel: IDisposable
    {
        byte[] Execute(byte[] buffer);

        Task<byte[]> ExecuteAsync(byte[] buffer);

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
