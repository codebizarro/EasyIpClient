using EasyIpClient.Channel.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace EasyIpClient.Channel
{
    public class TcpChannel: IChannel, IDisposable
    {
        private TcpClient Client;
        private NetworkStream stream;

        public TcpChannel(IPEndPoint endpoint)
        {
            Client = new TcpClient();
            Client.Connect(endpoint);
            stream = Client.GetStream();
            if (!stream.CanWrite) throw new Exception("Stream don't ready to write");
        }

        public byte[] Execute(byte[] buffer)
        {
            stream.Write(buffer, 0, buffer.Length);
            List<byte> lst = new List<byte>();
            if (stream.CanRead)
            {
                byte[] buff = new byte[1024];
                int n = 0;
                do
                {
                    n = stream.Read(buff, 0, buff.Length);
                    for (int i = 0; i < n; ++i)
                        lst.Add(buff[i]);
                }
                while (stream.DataAvailable);
            }
            return lst.ToArray();
        }

        public int SendTimeout
        {
            get
            {
                return Client.Client.SendTimeout;
            }
            set
            {
                Client.Client.SendTimeout = value;
            }
        }

        public int ReceiveTimeout
        {
            get
            {
                return Client.Client.ReceiveTimeout;
            }
            set
            {
                Client.Client.ReceiveTimeout = value;
            }
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                    if (Client != null)
                    {
                        Client.Close();
                        Client = null;
                    }
                }
                disposed = true;
            }
        }

        ~TcpChannel()
        {
            Dispose(false);
        }
    }
}
