using System;
using Object = UnityEngine.Object;
namespace GameFrameWork
{
    public class ResourceManager
    {
        public ResourceStorage m_resourceStorage = new ResourceStorage();

        public void Init()
        {
            
        }

        public T LoadResourceFromStorage<T>(ResourceRequirement requirement) where T:Object
        {
            return m_resourceStorage.LoadResource<T>(requirement);
        }
        public void LoadResourceFromStorageAsync<T>(ResourceRequirement requirement,Action<T> action) where T:Object
        {
            m_resourceStorage.LoadResourceAsync<T>(requirement,action);
        }
    }
}