﻿using System.Net.EasyIp.Common;
using System.Net.EasyIp.Enums;
using System.Net.EasyIp.Extensions;
using System.Net.EasyIp.Helpers;
using System.Net.EasyIp.Interfaces;

namespace System.Net.EasyIp
{
    public class EasyIpClient: Disposable, IEasyIpClient
    {
        private IChannel _channel;
        public EasyIpClient(IChannel channel)
        {
            _channel = channel;
        }

        public T[] BatchReadWord<T>(short point, DataTypeEnum dataType, byte length)
        {
            int typeSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            byte count = (byte)(length * typeSize / Constants.SHORT_SIZE);
            var packet = PacketFactory.GetReadPacket(point, dataType, count);
            byte[] recvBuffer = _channel.Execute(packet.ToByteArray());
            int dataLen = recvBuffer.Length - Constants.EASYIP_HEADERSIZE;
            int retLen = dataLen / typeSize;
            T[] ret = new T[retLen];
            Buffer.BlockCopy(recvBuffer, Constants.EASYIP_HEADERSIZE, ret, 0, dataLen);
            return ret;
        }

        public void BatchWriteWord<T>(short point, T[] val, DataTypeEnum dataType)
        {
            int typeSize = System.Runtime.InteropServices.Marshal.SizeOf(typeof(T));
            byte count = (byte)(val.Length * typeSize / Constants.SHORT_SIZE);
            var packet = PacketFactory.GetWritePacket(point, dataType, count);
            var sendBuffer = packet.ToByteArray();
            Buffer.BlockCopy(val, 0, sendBuffer, Constants.EASYIP_HEADERSIZE, packet.SendDataSize * Constants.SHORT_SIZE);
            var recvBuffer = _channel.Execute(sendBuffer);
        }

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
