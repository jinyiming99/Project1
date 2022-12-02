using GameFrameWork.Thread;

namespace GameFrameWork
{
    public class FrameWorkComponents
    {
        private CoroutineWorkerComponent _mCoroutineWorkerComponent;
        public CoroutineWorkerComponent CoroutineWorkerComponent => _mCoroutineWorkerComponent;

        private LifecycleComponent m_lifecycleComponent;
        public LifecycleComponent LifecycleComponent => m_lifecycleComponent;

        private ResourceManager _resourceManager;
        public ResourceManager ResourceManager => _resourceManager;
        
        private DebugTools.DebugManager _debugManager;
        
        internal DebugTools.DebugManager DebugManager => _debugManager;

        private ThreadWorker _threadWorker; 
        public ThreadWorker ThreadWorker => _threadWorker; 
        public void Init()
        {
            _mCoroutineWorkerComponent = GameObjectInstance.Instance.AddComponent<CoroutineWorkerComponent>();
            m_lifecycleComponent = GameObjectInstance.Instance.AddComponent<LifecycleComponent>();
            _debugManager = GameObjectInstance.Instance.AddComponent<DebugTools.DebugManager>();
            _resourceManager = new ResourceManager();
            _threadWorker = new ThreadWorker();
            _threadWorker.Start();
        }

        public void Release()
        {
            _threadWorker.Stop();
            _threadWorker = null;
            _resourceManager.Release();
            _resourceManager = null;
            // GameObjectInstance.Instance.GetComponent<CoroutineWorkerComponent>();
            // _mCoroutineWorkerComponent = null;
            // GameObjectInstance.Instance.GetComponent<LifecycleComponent>();
            // m_lifecycleComponent = null;
            // GameObjectInstance.Instance.GetComponent<DebugTools.DebugManager>();
            // _debugManager = null;
        }
    }
}