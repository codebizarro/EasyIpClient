﻿using EasyIpClient.Channel.Interfaces;
using EasyIpClient.Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EasyIpClient.Channel
{
    public sealed class UdpChannel : Disposable, IChannel
    {
        private UdpClient _client;
        private IPEndPoint _endPoint;

        public UdpChannel(IPEndPoint endPoint)
        {
            _endPoint = endPoint;
            _client = new UdpClient();
            _client.Connect(endPoint);
        }

        public UdpChannel(string host, int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _client = new UdpClient();
            _client.Connect(_endPoint);
        }

        public byte[] Execute(byte[] buffer)
        {
            _client.Send(buffer, buffer.Length);
            return _client.Receive(ref _endPoint);
        }

        public async Task<byte[]> ExecuteAsync(byte[] buffer)
        {
            await _client.SendAsync(buffer, buffer.Length);
            var result = await _client.ReceiveAsync();
            return result.Buffer;
        }

        public int SendTimeout
        {
            get
            {
                return _client.Client.SendTimeout;
            }
            set
            {
                _client.Client.SendTimeout = value;
            }
        }

        public int ReceiveTimeout
        {
            get
            {
                return _client.Client.ReceiveTimeout;
            }
            set
            {
                _client.Client.ReceiveTimeout = value;
            }
        }

        ~UdpChannel()
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
                if (_client != null)
                {
                    _client.Close();
                    _client = null;
                }
                GC.SuppressFinalize(this);
            }

            base.Dispose(disposing);
            // free unmanaged resources here
        }
    }
}
