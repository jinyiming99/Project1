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

        private int m_id = NetworkCounter.GetCount();

        public MessageConstDefine.ErrorCallBack errorCallback { set; private get; }
        
        public MessageConstDefine.ReveiceMessageBaseCallBack receiveMessageCallback { set; private get; }

        public MessageConstDefine.ConnectCallBack connectCallback { set; private get; }
        
        public MessageConstDefine.CloseCallBack closeCallback { set; private get; }
        

        private NetworkKeeper m_keeper = new NetworkKeeper();

        private DataSegment _dataSegment = new DataSegment();
        internal NetworkWorker(IConnect connect)
        {
            m_connect = connect;
            m_connect.ReveiceCallback = OnReveiceData;
            m_connect.ErrorCallBack = OnErrorCallback;
            m_connect.CloseCallBack = OnClose;
            m_connect.ConnectCallBack = OnConnectCallBack;
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

        private void OnClose()
        {
            
        }

        public void StartConnect()
        {
            m_connect.ConnectAsync();
        }

        public void Disconnect()
        {
            if (m_connect.IsConnected)
            {
                m_connect.Dispose();
                closeCallback?.Invoke();
            }
        }
        public void ReConnect()
        {
            
        }

        void OnConnectCallBack(NetworkErrorEnum status)
        {
            connectCallback?.Invoke(status);
            if (status == NetworkErrorEnum.Success)
            {
                BeginReceive();
            }
        }

        public void BeginReceive()
        {
            m_connect.BeginReceive();
        }
        
        public void SendAsync(DataSegment data)
        {
            m_connect.SendAsync(data.m_data,data.Length,0);
        }

        void OnReveiceData(byte[] data , int length)
        {
            _dataSegment.Write(data,length);

            if (MessageBase.MessageBase.TryGetMessage(_dataSegment, out var msg))
                receiveMessageCallback?.Invoke(msg);
        }

        void OnErrorCallback(ConnectErrorStatus status,SocketError error)
        {
            errorCallback?.Invoke(status,error);
        }


        public int GetID()
        {
            return m_id;
        }

        public void Release()
        {
            if (m_connect.IsConnected)
                m_connect.Dispose();
        }
    }
}