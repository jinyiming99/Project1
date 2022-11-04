using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace GameFrameWork.Addressable
{
    public class StreamResourceStorage : IResourceStorage
    {
        private Dictionary<string, AddressableHandleLoader> m_dic = new Dictionary<string, AddressableHandleLoader>(32);


        public T LoadResource<T>(ResourceRequirement requirement) where T : Object
        {
            if (m_dic.TryGetValue(requirement.Arg1, out var value))
            {
                return value.LoadResources<T>(requirement.Arg1);
            }
            else
            {
                value = new AddressableHandleLoader();
                m_dic.Add(requirement.Arg1,value);
                return value.LoadResources<T>(requirement.Arg1);
            }
        }

        public void LoadResourceAsync<T>(ResourceRequirement requirement, Action<T> action) where T : Object
        {
            if (m_dic.TryGetValue(requirement.Arg1, out var value))
            {
                value.LoadResourcesAsync<T>(requirement.Arg1,action);
            }
            else
            {
                value = new AddressableHandleLoader();
                m_dic.Add(requirement.Arg1,value);
                value.LoadResourcesAsync<T>(requirement.Arg1,action);
            }
        }
    }
}