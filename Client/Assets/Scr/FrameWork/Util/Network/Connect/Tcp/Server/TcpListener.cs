using System;
using System.Net;
using System.Net.Sockets;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public delegate void ListenerSocketCallBack(TcpConnect connect);
    public class TcpListener : IListener
    {
        private Socket m_socket;

        private ListenerSocketCallBack m_callback;
        public ListenerSocketCallBack Callback { set; get; }

        private Action<NetworkWorker> ListenCallBack;
        
        private TcpListener(Socket socket)
        {
            m_socket = socket;
        }

        public void StartAppept()
        {
            m_socket.BeginAccept(AcceptCallback, m_socket);
        }

        internal void AcceptCallback(IAsyncResult iar)
        {
            if (iar.IsCompleted)
            {
                try
                {
                    Socket socket = (Socket) iar.AsyncState;
                    if (socket != null)
                    {
                        Socket acceptSocket = socket.EndAccept(iar);
                        var connect = TcpConnect.CreateConnect(acceptSocket);
                        m_callback?.Invoke(connect);
                    }
                }
                catch (Exception e)
                {
                    DebugTools.DebugHelper.LogError($"Socket AcceptCallback error {e.ToString()}");
                }
                finally
                {
                    StartAppept();
                }
                
            }
        }
        public static TcpListener CreateTcpListener(string ip,int post,int listenCount)
        {
            IPAddress address = IPAddress.Parse(ip);
            IPEndPoint local = new IPEndPoint(address,post);
            Socket socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            try
            {
                socket.Bind(local);
            }
            catch (Exception e)
            {
                
            }
            
            socket.Listen(listenCount);

            TcpListener listener = new TcpListener(socket);
            return listener;
        }
    }
}