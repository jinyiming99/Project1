using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
namespace GameFrameWork.Addressable
{
    internal enum HandleLoadStatus
    {
        None,
        Loading,
        Loaded,
    }
    public class AddressableHandleLoader
    {
        private AsyncOperationHandle m_handle;

        private int m_number = 0;

        private HandleLoadStatus m_states = HandleLoadStatus.None;

        public AddressableHandleLoader()
        {
            m_number = 0;
            m_states = HandleLoadStatus.None;
        }

        public T LoadResources<T>(string name) where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(name);
            T obj = handle.WaitForCompletion() as T;
            return obj;
        }
        
        public void LoadResourcesAsync<T>(string name,Action<T> action) where T :UnityEngine.Object
        {
            switch (m_states)
            {
                case HandleLoadStatus.None:
                {
                    m_handle = Addressables.LoadAssetAsync<T>(name);
                    m_handle.Completed += (result) =>
                    {
                        if (result.Status == AsyncOperationStatus.Succeeded)
                        {
                            action?.Invoke(result.Result as T);
                        }
                        else
                        {
                            action?.Invoke(null);
                        }
                        m_states= HandleLoadStatus.Loaded;
                    };
                    m_states = HandleLoadStatus.Loading;
                    break;
                }
                case HandleLoadStatus.Loading:
                {
                    m_handle.Completed +=(result) =>
                    {
                        if (result.Status == AsyncOperationStatus.Succeeded)
                        {
                            action?.Invoke(result.Result as T);
                        }
                        else
                        {
                            action?.Invoke(null);
                        }
                        m_states= HandleLoadStatus.Loaded;
                    };
                    break;
                }
                case HandleLoadStatus.Loaded:
                {
                    action?.Invoke(m_handle.Result as T);
                    break;
                }
            }
            
        }

        public virtual void Release()
        {
            
        }
    }
}