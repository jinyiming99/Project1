using System;
using System.Collections.Generic;
using System.Linq;
using GameFrameWork;
using GameFrameWork.Network;
using GameFrameWork.Network.MessageBase;
using Google.Protobuf;

namespace Game.Network
{
    public class GameServer
    {
        private static GameServer s_server;
        public static GameServer CreaterServer(Action<NetworkWorker> action)
        {
            if (s_server == null)
            {
                s_server = new GameServer();
                var listener = FrameWork.GetFrameWork().Components.Network
                    .CreateServer(GameFrameWork.Network.NetworkManager.GetLocalIP(), 
                        GameNetworkDefine.GamePort, 
                        GameNetworkDefine.GameListenerCount, 
                        s_server.ListenerCallback);
                s_server.m_listener = listener;
                s_server.m_action = action;
            }
            return s_server;
        }

        public static void CloseServer()
        {
            if (s_server != null)
            {
                s_server.Release();
                s_server = null;
            }
        }

        private Action<NetworkWorker> m_action;
        private NetworkListener m_listener;
        private Dictionary<int, NetworkWorker> m_workDic = new Dictionary<int, NetworkWorker>();
        internal GameServer()
        {
            
        }

        public void ListenerCallback(NetworkWorker worker)
        {
            if (worker != null)
            {
                if (!m_workDic.ContainsKey(worker.GetID()))
                {
                    m_workDic.Add(worker.GetID(),worker);
                }
                m_action?.Invoke(worker);
            }
        }

        public void SendMessageToAll(int msgID, Google.Protobuf.IMessage msg)
        {
            if (m_workDic.Count > 0)
            {
                MessageBase messageBase = new MessageBase();
                messageBase.m_cmd = (short)MessageCommand.ProtoBuf_Message;
                messageBase.m_messageID = (short)msgID;
                messageBase.m_data = msg.ToByteArray();
                messageBase.m_length = messageBase.m_data.Length;
                var data = messageBase.GetData();
                foreach (var worker in m_workDic)
                {
                    
                    worker.Value.SendAsync(data);
                }
            }
        }
        
        

        public void Release()
        {
            if (m_workDic.Count > 0)
            {
                foreach (var worker in m_workDic)
                {
                    worker.Value.Disconnect();
                }
                m_listener.Release();
            }
        }
    }
}