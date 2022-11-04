using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace GameFrameWork.Network.Server
{
    public static class TcpServer
    {
        public static NetworkListener CreateServer(string ip,int post,int listenCount,Action<NetworkWorker> action)
        {
            TcpListener listener = TcpListener.CreateTcpListener(ip,post,listenCount);
            var networker = NetworkListener.CreateListener(listener, action);
            return networker;
        }

        public static void CloseServer(NetworkListener listener)
        {
            if (listener == null)
                return;
            
        }
    }
}