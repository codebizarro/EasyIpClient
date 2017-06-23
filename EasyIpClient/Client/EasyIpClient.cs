using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Common;
using EasyIpClient.Enums;
using EasyIpClient.Helpers;
using EasyIpClient.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyIpClient.Client
{
    public class EasyIpClient: Disposable, IEasyIpClient
    {
        private IChannel _channel;
        EasyIpClient(IChannel channel)
        {
            _channel = channel;
        }

        //public T[] BatchReadWord<T>(short point, DataTypeEnum dataType, byte count)
        //{
        //    int typeSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
        //    var packet = PacketFactory.GetReadPacket(point, dataType, count);
        //    byte[] recvBuffer = _channel.Execute(packet.ToArray());
        //    int dataLen = recvbuffer.Length - ReturnValuePosition;
        //    int retLen = dataLen / typeSize;
        //    T[] ret = new T[retLen];
        //    Buffer.BlockCopy(recvbuffer, Constants.EASYIP_HEADERSIZE, ret, 0, dataLen);
        //    return ret;
        //}

        ~EasyIpClient()
        {
            Dispose(false);
        }

        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            if (disposing)
            {
                if (_channel != null)
                {
                    _channel.Dispose();
                }
                GC.SuppressFinalize(this);
            }

            base.Dispose(disposing);
            // free unmanaged resources here
        }
    }
}
