using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using GameFrameWork.Network.Interface;
using TcpClient = GameFrameWork.Network.Client.TcpClient;

namespace GameFrameWork.Network
{
    public class NetworkManager
    {
        private NetworkUserManager m_userManager;
        internal NetworkUserManager userManager => m_userManager;
        public NetworkManager()
        {
            m_userManager = new NetworkUserManager();
        }

        public static string GetLocalIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }

            return string.Empty;
        }

        #region tcp server

        public NetworkListener CreateServer(string ip,int post,int listenCount,Action<NetworkWorker> action)
        {
            TcpListener listener = TcpListener.CreateTcpListener(ip,post,listenCount);
            var networker = m_userManager?.CreateListener(listener, (connect) =>
            {
                var con = m_userManager?.CreateUser(connect);
                action?.Invoke(con);
            });
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