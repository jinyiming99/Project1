using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFrameWork
{
    public abstract class ResourceLoader
    {
        protected AsyncOperationHandle _loadHandle;
        protected bool _isComplete = false;
        protected bool _isDel = false;
        public virtual void Del()
        {
            if (_isComplete)
                return;
            if (_loadHandle.IsValid() && _loadHandle.IsDone)
            {
                Addressables.Release(_loadHandle);
            }
            _isDel = true;
        }

        public virtual void LoadOver()
        {
            _isComplete = true;
        }

        protected abstract AsyncOperationHandle LoadResource(string name);
    }
}