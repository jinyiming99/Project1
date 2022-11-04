using GameFrameWork.Addressable;
using GameFrameWork.Network;

namespace GameFrameWork
{
    internal class FrameWorkManagers
    {
        private static ResourceManager s_resourceManager = null;
        public static ResourceManager resource => s_resourceManager;
        
        private static StreamResourceStorage s_streamStorage = null;
        public static StreamResourceStorage streamStorage => s_streamStorage;
            
        private static NetworkManager s_networkManager = null;
        public static NetworkManager network => s_networkManager;
        public static void Init()
        {
            s_resourceManager = new ResourceManager();
            s_streamStorage = new StreamResourceStorage();
        }

        public static void InitNetWork(ConnectProtoType protoType,NetworkManagerWorkType workType)
        {

            if (s_networkManager != null)
                s_networkManager.Release();
            else
                s_networkManager = new NetworkManager();

            s_networkManager.ProtoType = protoType;
            s_networkManager.workType = workType;
        }
    }
}