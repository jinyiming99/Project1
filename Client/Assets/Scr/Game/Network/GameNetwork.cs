using Common.Proto;
using GameFrameWork.Network;

namespace Game.Network
{
    public class GameNetwork
    {
        private GameNetworkWorkMode _mode;
        private GameNetworkProtoType _type;
        public GameServer _Server;
        public GameClient _Client;
        public void CreateServer()
        {
            if (_mode == GameNetworkWorkMode.None)
                _Server = GameServer.CreaterServer(ConnectListener);
        }

        public void Close()
        {
            switch (_mode)
            {
                case GameNetworkWorkMode.None:
                    break;
                case GameNetworkWorkMode.Client:
                {
                    break;
                }
                case GameNetworkWorkMode.Server:
                {
                    break;
                }
            }
            GameServer.CloseServer();
        }

        public void CreateClient(string ip,int port)
        {
            if (_mode == GameNetworkWorkMode.None)
                _Client = GameClient.CreateClient(ip,port);
        }

        private void ConnectListener(NetworkWorker worker)
        {
            
        }

        public void SendMessage(MessageEnum id, Google.Protobuf.IMessage msg)
        {
            switch (_mode)
            {
                case GameNetworkWorkMode.Client:
                {
                    break;
                }
                case GameNetworkWorkMode.Server:
                {
                    break;
                }
            }
        }
    }
}