using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using GameFrameWork.Network.MessageBase;

namespace GameFrameWork.Network.Client
{
    public class TcpClient 
    {
        public static TcpConnect ConnectServer(string ip, int port)
        {
            var connect = Connect(ip, port);
            connect.IP = ip;
            connect.Port = port;
            return connect;
        }
        public static void ConnectServerAsync(string ip, int port,Action<TcpConnect> action)
        {
            Socket socket = CreateConnect();
            var address = IPAddress.Parse(ip);
            if (socket != null)
            {
                try
                {
                    socket.BeginConnect(address, port,ar =>
                    {
                        var connect = TcpConnect.CreateConnect(socket);
                        connect.IP = ip;
                        connect.Port = port;
                        action?.Invoke(connect); 
                    },null);
                }
                catch (Exception e)
                {
                    var str = e.ToString();
                    DebugTools.DebugHelper.LogError($"connect server failed ,error = {str}");
                    throw new NetworkException( NetworkErrorEnum.Socket_Connect_Failed, str);
                }
            }
            socket.NoDelay = true;
        }

        private static TcpConnect Connect(string ip,int port)
        {            
            var socket = CreateConnect();
            var address = IPAddress.Parse(ip);

            if (socket != null)
            {
                try
                {
                    //DebugTools.DebugHelper.Log("connect address " + address + "    port   = " + port);
                    socket.Connect(address, port); 
                }
                catch (Exception e)
                {
                    var str = e.ToString();
                    DebugTools.DebugHelper.LogError($"connect server failed ,error = {str}");
                    throw new NetworkException( NetworkErrorEnum.Socket_Connect_Failed, str);
                }
            }
            socket.NoDelay = true;
            
            var connect = TcpConnect.CreateConnect(socket);
            //TcpClient client = new TcpClient(connect);
            return connect;
        }
        
        
        
        private static AddressFamily CheckFamily()
        {
            AddressFamily af = AddressFamily.InterNetwork;

            try
            {
                IPAddress[] address = Dns.GetHostAddresses(Dns.GetHostName());
                if (address[0].AddressFamily == AddressFamily.InterNetworkV6)
                    af = AddressFamily.InterNetworkV6;
            }
            catch (Exception e)
            {

            }
            return af;
        }
        
        private static Socket CreateConnect()
        {
            Socket socket = null;
            AddressFamily af = CheckFamily();
            try
            {
                socket = new Socket(af, SocketType.Stream, ProtocolType.Tcp);
            }
            catch
            {
                if (af == AddressFamily.InterNetworkV6)
                    af = AddressFamily.InterNetwork;
                else if (af == AddressFamily.InterNetwork)
                    af = AddressFamily.InterNetworkV6;
                try
                {
                    socket = new Socket(af, SocketType.Stream, ProtocolType.Tcp);
                }
                catch(Exception e)
                {
                    var str = e.ToString();
                    DebugTools.DebugHelper.LogError($" create socket failed ,e ={str}");
                    throw new NetworkException(NetworkErrorEnum.Socket_Create_Failed, str);
                }
            }
            return socket;
        }
    }
}