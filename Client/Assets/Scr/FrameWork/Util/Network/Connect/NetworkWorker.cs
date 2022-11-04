using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using GameFrameWork.Network.Interface;
using GameFrameWork.Network.MessageBase;

namespace GameFrameWork.Network
{
    public class NetworkWorker :INetworkWorker
    {
        private IConnect m_connect;

        private Action<SocketError> m_onDisconnectCallback = null;

        public Action<SocketError> OnDisconnectCallback
        {
            get => m_onDisconnectCallback;
            set => m_onDisconnectCallback = value;
        }

        private NetworkKeeper m_keeper = new NetworkKeeper();

        private DataSegment _dataSegment = new DataSegment();
        internal NetworkWorker(IConnect connect)
        {
            m_connect = connect;
            m_connect.ReveiceCallback = OnReveiceData;
            m_connect.FailedCallBack = OnConnectError;
        }

        public NetworkConnectStatusEnum CheckConnectStatus()
        {
            if (m_connect == null)
                return NetworkConnectStatusEnum.None;

            if (m_connect.IsConnected)
            {
                return NetworkConnectStatusEnum.Connected;
            }
            else
            {
                return NetworkConnectStatusEnum.Disconnect;
            }
        }

        public void Disconnect()
        {
            m_connect.Dispose();
            
            m_onDisconnectCallback = null;
        }
        public void ReConnect()
        {
            
        }

        public void BeginReceive()
        {
            m_connect.BeginReceive();
        }
        
        public void SendAsync(IMessage message)
        {
            NetworkManager.Post(() =>
            {
                var data = message.GetData();
                m_connect.SendAsync(data.m_data,data.Length,0);
            });
            
        }

        

        void OnReveiceData(byte[] data , int length)
        {
            _dataSegment.Write(data,length);
            
            
        }

        void OnConnectError(ConnectErrorStatus status,SocketError error)
        {
            m_onDisconnectCallback?.Invoke(error);
        }


        public void Release()
        {
            if (m_connect.IsConnected)
                m_connect.Dispose();
        }
    }
}