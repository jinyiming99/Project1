using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFrameWork.UI
{
    public class UIPrefabLoader :ResourceLoader
    {

        private Action<GameObject> m_action;

        public static UIPrefabLoader CreateLoader<T>(string name, Action<T> action) where T : MonoBehaviour
        {
            UIPrefabLoader loader = new UIPrefabLoader();
            loader.SetLoader(name,action);
            return loader;
        }

        public void SetLoader<T>(string name, Action<T> action) where T : MonoBehaviour
        {
            m_action = (obj) =>
            {
                var t = obj.GetComponent<T>();
                action?.Invoke(t);
            };
            _loadHandle = LoadResource(name);
        }

        protected override AsyncOperationHandle LoadResource(string name)
        {
            return FrameWork.GetFrameWork().Components.ResourceManager.LoadGameObject(name, (obj) =>
            {
                if (_isDel)
                    return;
                m_action?.Invoke(obj);
            });
            
        }
    }
}