using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
namespace GameFrameWork
{
    public class ResourceManager
    {
        internal ResourceManager()
        {
            
        }
        public AsyncOperationHandle LoadGameObject(string name,Action<GameObject> action)
        {
            var loadHandle = Addressables.InstantiateAsync(name);
            loadHandle.Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
            return loadHandle;
        }

        public AsyncOperationHandle LoadResource<T>(string name, Action<T> action)
        {
            var loadHandle = Addressables.LoadAssetAsync<T>(name);
            loadHandle.Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
            return loadHandle;
        }

        public AsyncOperationHandle LoadScene(string name,LoadSceneMode loadMode ,Action<Scene> action)
        {
            var loadHandle = Addressables.LoadSceneAsync(name,loadMode);
            loadHandle.Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result.Scene);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
            return loadHandle;
        }

        public void Release()
        {
            
        }
        public void ReleaseInstance(GameObject obj)
        {
            if (obj != null)
                Addressables.ReleaseInstance(obj);
        }

        public void ReleaseAsset<T>(T obj) where T : class
        {
            if (obj != null)
                Addressables.Release(obj);
        }
    }
}