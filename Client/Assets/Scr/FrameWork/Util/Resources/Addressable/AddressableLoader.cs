using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFrameWork.Addressable
{
    public class AddressableLoader
    {
        private static Dictionary<string, AddressableHandleLoader> m_dic = new Dictionary<string, AddressableHandleLoader>(32);
        public static T LoadResources<T>(string name) where T:UnityEngine.Object
        {
            if (m_dic.TryGetValue(name, out var handle))
            {
                return handle.LoadResources<T>(name);
            }
            else
            {
                handle = new AddressableHandleLoader();
                m_dic.Add(name,handle);
                var t =handle.LoadResources<T>(name);
                return t;
            }
        }
        public static void LoadResourcesAsync<T>(string name,Action<T> action) where T :UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(name);
            handle.Completed += (result) =>
            {
                if (result.Status == AsyncOperationStatus.Succeeded)
                {
                    action?.Invoke(result.Result as T);
                }
                else
                {
                    action?.Invoke(null);
                }
            };
        }
        
        
    }
}