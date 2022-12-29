using System;
using System.Collections.Generic;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public class NetworkUserManager
    {
        private Dictionary<long, INetworkWorker> m_userDic = new Dictionary<long, INetworkWorker>(4);
        static List<long> removeList = new List<long>();
        public NetworkWorker CreateUser(TcpConnect connect)
        {
            NetworkWorker worker = new NetworkWorker(connect);
            m_userDic.Add(worker.GetID(), worker);
            return worker;
        }
        
        public NetworkListener CreateListener(TcpListener l,Action<TcpConnect> a)
        {
            NetworkListener listener = new NetworkListener(l,a);
            m_userDic.Add(listener.GetID(), listener);
            return listener;
        }

        public void ReleaseWorker(INetworkWorker worker)
        {
            if (worker == null)
                return;
            if (m_userDic.ContainsKey(worker.GetID()))
            {
                m_userDic.Remove(worker.GetID());
            }
            worker.Release();
        }

        public void Release()
        {
            foreach (var iworker in m_userDic)
            {
                iworker.Value.Release();
            }
            m_userDic.Clear();
        }
    }
}