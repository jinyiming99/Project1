using System;
using System.Collections.Generic;
using GameFrameWork;
using GameFrameWork.Network;

namespace Scr.Game.Network
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