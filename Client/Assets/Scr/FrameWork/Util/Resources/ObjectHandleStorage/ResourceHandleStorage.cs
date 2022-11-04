using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace GameFrameWork
{
    public class ResourceHandleStorage
    {
        private Dictionary<string, Dictionary<Type, IResourceHandle>> m_dic = new Dictionary<string, Dictionary<Type, IResourceHandle>>();
        public bool TryGetValue<T>(string name,out IResourceHandle data) 
        {
            data = null;
            if (m_dic.TryGetValue(name, out var dic))
            {
                Type type = typeof(T);
                if (dic.TryGetValue(type, out var d))
                {
                    data = d;
                    return true;
                }
            }

            return false;
        }
    }
}