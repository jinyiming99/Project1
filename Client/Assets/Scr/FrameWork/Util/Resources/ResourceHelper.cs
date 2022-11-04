using System;
using System.Collections;

using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrameWork
{
    public class ResourceHelper
    {
        public static T LoadResource<T>(string name) where T: Object
        {
            var type = ResourceStroageType.StreamFold;
            switch (type)
            {
                case ResourceStroageType.ResourceFold:
                {
                    ResourceRequirement req = new ResourceRequirement(name);
                    return FrameWorkManagers.resource.LoadResourceFromStorage<T>(req);
                }
                case ResourceStroageType.StreamFold :
                {
                    ResourceRequirement req = new ResourceRequirement(name);
                    return FrameWorkManagers.streamStorage.LoadResource<T>(req);
                }
                case ResourceStroageType.DownloadFold:
                    break;
            }

            return null;
        }
        public static void LoadResourceAsync<T>(string name,Action<T> callback) where T: Object
        {
            var type = ResourceStroageType.StreamFold;
            switch (type)
            {
                case ResourceStroageType.ResourceFold:
                {
                    ResourceRequirement req = new ResourceRequirement(name);
                    FrameWorkManagers.resource.LoadResourceFromStorageAsync<T>(req, callback);
                    break;
                }
                case ResourceStroageType.StreamFold :
                {
                    ResourceRequirement req = new ResourceRequirement(name);
                    FrameWorkManagers.streamStorage.LoadResourceAsync<T>(req,callback);
                    break;
                }
                case ResourceStroageType.DownloadFold:
                    break;
            }
            //return default;
        }

        private static void NextFrame(Action action)
        {
            CoroutineLoader.Do(() => LoadAsync(action));
        }
        
        private static IEnumerator LoadAsync(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
        }
    }
}