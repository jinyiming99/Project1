using System.Net.Sockets;
using GameFrameWork;
using GameFrameWork.Network;
using GameFrameWork.Network.MessageBase;
using Google.Protobuf;
using TcpClient = GameFrameWork.Network.Client.TcpClient;

namespace Game.Network
{
    public class GameClient
    {
        private static ProtoBufMessageDistribute s_messageDistribute;
        public static GameClient CreateClient(string ip, int port)
        {
            if (s_messageDistribute == null)
            {
                s_messageDistribute = new ProtoBufMessageDistribute("");
                s_messageDistribute.CreateMessageDic();
            }
            NetworkWorker worker = TcpClient.CreateTcpConnect(ip, port);
            GameClient gameClient = new GameClient(worker)
            {
                _messageDistribute = s_messageDistribute,
            };
            return gameClient;
        }

        private NetworkWorker m_worker;
        private ProtoBufMessageDistribute _messageDistribute;
        internal GameClient(NetworkWorker worker)
        {
            m_worker = worker;
            m_worker.receiveMessageCallback = ReveiceMessage;
            m_worker.connectCallback = ConnectCallback;
            m_worker.closeCallback = OnCloseCallback;
            m_worker.errorCallback = OnErrorCallback;
        }

        public void SendAsync(short @enum,Google.Protobuf.IMessage message)
        {
            var size = message.CalculateSize();
            var array = message.ToByteArray();

            if (m_worker != null)
            {
                m_worker.SendMessageAsync(@enum,array,size);
            }
        }

        public void ReveiceMessage(MessageBase messageBase)
        {
            if (messageBase.m_cmd == (int) MessageCommand.ProtoBuf_Message)
            {
                _messageDistribute.Find(messageBase.m_messageID, out var creater);
                creater.CreateMessage(messageBase);
                creater.Processor();
                creater.ReleaseMessage();
            }
        }

        public void ConnectCallback(NetworkErrorEnum status)
        {
            
        }

        public void OnCloseCallback()
        {
            
        }

        public void OnErrorCallback(ConnectErrorStatus error,SocketError errorType)
        {
            
        }
    }
}