using System;
using GameFrameWork.Network.Interface;

namespace GameFrameWork.Network
{
    public class NetworkListener : INetworkWorker
    {

        private IListener m_listener;
        private Action<NetworkWorker> m_action;
        public static NetworkListener CreateListener(IListener l,Action<NetworkWorker> a)
        {
            NetworkListener listener = new NetworkListener(){m_listener = l,m_action = a};

            l.Callback = listener.OnAppept;
            return listener;
        }

        private void OnAppept(TcpConnect connect)
        {
            m_action?.Invoke(new NetworkWorker(connect));
        }
        


        public void StartListen()
        {
            m_listener?.StartAppept();
        }

        public void Release()
        {
            
        }
    }
}