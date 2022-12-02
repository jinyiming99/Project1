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
        public void LoadGameObject(string name,Action<GameObject> action)
        {
            Addressables.InstantiateAsync(name).Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
        }

        public void LoadResource<T>(string name, Action<T> action)
        {
            Addressables.LoadAssetAsync<T>(name).Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
        }

        public void LoadScene(string name,LoadSceneMode loadMode ,Action<Scene> action)
        {
            Addressables.LoadSceneAsync(name,loadMode).Completed += (handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    action?.Invoke(handle.Result.Scene);
                else
                    DebugTools.DebugHelper.LogError(() =>handle.OperationException.ToString());
            });
        }

        public void Release()
        {
            
        }
        public void ReleaseInstance(ref GameObject obj)
        {
            if (obj != null)
                Addressables.ReleaseInstance(obj);
        }

        public void ReleaseAsset<T>(ref T obj) where T : class
        {
            if (obj != null)
                Addressables.Release(obj);
        }
    }
}