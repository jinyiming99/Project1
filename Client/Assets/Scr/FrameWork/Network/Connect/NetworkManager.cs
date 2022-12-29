using System;
using System.Collections.Generic;
using GameFrameWork.Network.Client;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public class NetworkManager
    {
        private NetworkUserManager m_userManager;

        public NetworkManager()
        {
            m_userManager = new NetworkUserManager();
        }

        #region tcp server

        public NetworkListener CreateServer(string ip,int post,int listenCount,Action<NetworkWorker> action)
        {
            TcpListener listener = TcpListener.CreateTcpListener(ip,post,listenCount);
            var networker = m_userManager?.CreateListener(listener,action);
            return networker;
        }
        
        public void CloseServer(NetworkListener listener)
        {
            if (listener == null)
                return;
            listener.Release();
            m_userManager?.ReleaseWorker(listener);
        }

        #endregion

        #region tcpworker

        public void CreateTcpConnectAsync(string ip, int port, Action<NetworkWorker> action)
        {
            TcpClient.ConnectServerAsync(ip,port, (connect) =>
            {
                var user = m_userManager?.CreateUser(connect);
                action?.Invoke(user);
            });
        }

        public NetworkWorker CreateTcpConnect(string ip, int port)
        {
            var connect = TcpClient.ConnectServer(ip, port);
            var user = m_userManager?.CreateUser(connect);
            return user;
        }

        public void CloseTcpConnect(NetworkWorker worker)
        {
            worker.Release();
            m_userManager?.ReleaseWorker(worker);
        }
        #endregion
        public void Release()
        {
            m_userManager?.Release();
        }
    }
}