using System;
using System.Collections;
using GameFrameWork.RequirementSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrameWork
{
    public class ResourceStorage : IResourceStorage
    {
        public void Init()
        {
            
        }

        public T LoadResource<T>(ResourceRequirement requirement) where T : Object
        {
            var name = requirement.Arg1;
            var obj =Resources.Load<T>(name);
            if (obj == null)
                return null;
            return obj;
        }

        public void LoadResourceAsync<T>(ResourceRequirement resourceRequirement,Action<T> action) where T : Object
        {
            var name = resourceRequirement.Arg1;
            var obj =Resources.LoadAsync<T>(name);
            RequirementSystemManager<ResourceRequirement,Action<T>>.Add(resourceRequirement,action);
            CoroutineLoader.Do(() =>
            {
                return CheckResourceRequestIsDone(obj, () =>
                {
                    var list = RequirementSystemManager<ResourceRequirement,Action<T>>.Get(resourceRequirement);
                    foreach (var act in list)
                    {
                        act?.Invoke((T)obj.asset);
                    }
                });
            });
        }

        public void Release(ResourceRequirement resourceRequirement)
        {
            //Resources.UnloadAsset();
        }

        private IEnumerator CheckResourceRequestIsDone(ResourceRequest rr,Action action)
        {
            yield return rr.isDone;
            action?.Invoke();
        }
    }
}