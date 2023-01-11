using System;
using System.Net;
using System.Net.Sockets;

namespace GameFrameWork.Network.Client
{
    public class TcpClient 
    {
        public static NetworkWorker CreateTcpConnect(string ip, int port)
        {
            var connect = TcpConnect.CreateConnect();
            connect.IP = ip;
            connect.Port = port;
            var user = FrameWork.GetFrameWork().Components.Network.userManager?.CreateUser(connect);
            return user;
        }
    }
}