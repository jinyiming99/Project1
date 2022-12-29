using System;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public class NetworkListener : INetworkWorker
    {
        private int m_id = NetworkCounter.GetCount();
        private IListener m_listener;
        private Action<NetworkWorker> m_action;
        public ListenerSocketCallBack AppeptCallback => OnAppept;
        public NetworkListener(IListener l, Action<NetworkWorker> a)
        {
            m_listener = l;
            m_action = a;
        }
        private void OnAppept(TcpConnect connect)
        {
            m_action?.Invoke(new NetworkWorker(connect));
        }

        public void StartListen()
        {
            m_listener?.StartAppept();
        }

        public int GetID()
        {
            return m_id;
        }

        public void Release()
        {
            
        }
    }
}