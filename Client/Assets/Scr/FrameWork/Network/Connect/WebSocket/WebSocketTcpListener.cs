using GameFrameWork.Network.Interface;
using System.Net.WebSockets;

namespace GameFrameWork.Network.WebSocket
{
    public class WebSocketTcpListener : IListener
    {
        
        public ListenerSocketCallBack Callback { get; set; }
        public void StartAppept()
        {
            //System.Net.Sockets.TcpListener
        }
    }
}