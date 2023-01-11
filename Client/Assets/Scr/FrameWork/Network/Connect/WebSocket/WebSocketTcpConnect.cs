using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using GameFrameWork.Network.Interface;
using UnityEditor.Experimental.GraphView;

namespace GameFrameWork.Network.WebSocket
{
    public class WebSocketTcpConnect : IConnect
    {
        private ClientWebSocket _socket;
        public MessageConstDefine.ConnectCallBack ConnectCallBack { get; set; }
        public MessageConstDefine.ReveiceCallBack ReveiceCallback { get; set; }
        public MessageConstDefine.ErrorCallBack ErrorCallBack { get; set; }
        public MessageConstDefine.CloseCallBack CloseCallBack { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public bool IsConnected { get; }
        
        private byte[] m_receiveData = new byte[1024 * 1024];

        public WebSocketTcpConnect()
        {
            _socket = new ClientWebSocket();
        }

        public WebSocketTcpConnect(ClientWebSocket socket)
        {
            _socket = socket;
        }
        
        public async void ConnectAsync()
        {
            if (_socket != null && _socket.State != WebSocketState.Open)
            {
                await _socket.ConnectAsync(new Uri($"wss://{IP}:{Port}"),CancellationToken.None);
                if (_socket.State != WebSocketState.Open)
                {
                    ConnectCallBack?.Invoke(NetworkErrorEnum.Success);
                }
                else
                {
                    ConnectCallBack?.Invoke(NetworkErrorEnum.Socket_Connect_Failed);
                }
            }
        }

        public void DisConnect()
        {
            Dispose();
        }

        public async void BeginReceive()
        {
            if (_socket.State == WebSocketState.Open)
            {
                var result = await _socket.ReceiveAsync(new ArraySegment<byte>(m_receiveData),CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    Dispose();
                }
                else
                {
                    ReveiceCallback?.Invoke(m_receiveData,result.Count);
                    BeginReceive();
                }
            }
            
        }

        public async void SendAsync(byte[] data, int length, int offer)
        {
            if (_socket.State == WebSocketState.Open)
            {
                await _socket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, false,
                    CancellationToken.None);
            }
        }

        public void Dispose()
        {
            Task.Run( async () =>
            {
                await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, String.Empty, CancellationToken.None);
                _socket.Dispose();
            }).Wait();

        }
    }
}