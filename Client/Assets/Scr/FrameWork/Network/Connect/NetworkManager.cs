using System;
using System.Collections.Generic;
using GameFrameWork.Network.Server;

namespace GameFrameWork.Network
{
    internal class NetworkManager
    {
        private ConnectProtoType  m_protoType= ConnectProtoType.None;

        public ConnectProtoType ProtoType
        {
            set => m_protoType = value;
            get => m_protoType;
        }
        private NetworkManagerWorkType m_workType;
        public NetworkManagerWorkType workType
        {
            set => m_workType = value;
            get => m_workType;
        }
        
        private NetworkUserManager m_userManager;
        private List<NetworkWorker> m_connectList = new List<NetworkWorker>(4);
        

        private static NetworkThreadWork s_thread = new NetworkThreadWork();

        public NetworkManager()
        {
            m_userManager = new NetworkUserManager();
            s_thread.Start();
        }

        public static void Post(Action action)
        {
            if (s_thread != null)
            {
                s_thread.Post(action);
            }
        }

        



        public void Release()
        {
            s_thread.Stop();
        }
    }
}