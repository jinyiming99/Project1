using System;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public class NetworkListener : INetworkWorker
    {
        private int m_id = NetworkCounter.GetCount();
        private IListener m_listener;
        private Action<TcpConnect> m_action;
        public ListenerSocketCallBack AppeptCallback => OnAppept;
        public NetworkListener(TcpListener l, Action<TcpConnect> a)
        {
            m_listener = l;
            l.Callback = OnAppept;
            m_action = a;
        }
        private void OnAppept(TcpConnect connect)
        {
            m_action?.Invoke(connect);
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