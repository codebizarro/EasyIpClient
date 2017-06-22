using EasyIpClient.Channel.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;

namespace EasyIpClient.Channel
{
    public sealed class UdpChannelEx : IChannel, IDisposable
    {
        private Socket _socket;
        private IPEndPoint _endPoint;

        public UdpChannelEx(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(endPoint);
        }

        public UdpChannelEx(string host, int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(_endPoint);
        }

        public byte[] Execute(byte[] buffer)
        {
            _socket.SendTo(buffer, buffer.Length, SocketFlags.None, _endPoint);
            var recvBuffer = new byte[1024];
            var endPoint = (EndPoint)_endPoint;
            var recvLength = _socket.ReceiveFrom(recvBuffer, SocketFlags.None, ref endPoint);
            Array.Resize<byte>(ref recvBuffer, recvLength);
            return recvBuffer;
        }

        public int SendTimeout
        {
            get
            {
                return int.Parse(_socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout).ToString());
            }
            set
            {
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, value);

            }
        }

        public int ReceiveTimeout
        {
            get
            {
                return int.Parse(_socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout).ToString());
            }
            set
            {
                _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, value);
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (_socket != null)
                    {
                        _socket.Close();
                        _socket = null;
                    }
                }
                disposed = true;
            }
        }

        ~UdpChannelEx()
        {
            Dispose(false);
        }
    }
}
