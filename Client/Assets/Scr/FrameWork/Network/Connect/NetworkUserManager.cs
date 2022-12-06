using System.Collections.Generic;
using GameFrameWork.Network.Server;

namespace GameFrameWork.Network
{
    public class NetworkUserManager
    {
        private static Dictionary<long, NetworkWorker> m_userDic = new Dictionary<long, NetworkWorker>(4);
        static List<long> removeList = new List<long>();
        public static NetworkWorker CreateUser(TcpConnect connect)
        {
            NetworkWorker worker = new NetworkWorker(connect);
            m_userDic.Add(worker.GetHashCode(),worker);
            return worker;
        }

    }
}