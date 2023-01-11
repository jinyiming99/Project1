namespace GameFrameWork.Network.WebSocket
{
    public class WebSocketTcpClient
    {
        public static NetworkWorker CreateWebClient(string ip, int port)
        {
            WebSocketTcpConnect connect = new WebSocketTcpConnect();
            connect.IP = ip;
            connect.Port = port;
            var user = FrameWork.GetFrameWork().Components.Network.userManager?.CreateUser(connect);
            return user;
        }
    }
}