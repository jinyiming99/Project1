using System;
using UnityEngine;

namespace GameFrameWork.UI
{
    public class UIPrefabLoader
    {
        private bool m_isDel;
        private Action<GameObject> m_action;

        public static UIPrefabLoader CreateLoader<T>(string name, Action<T> action) where T : MonoBehaviour
        {
            UIPrefabLoader loader = new UIPrefabLoader();
            loader.SetLoader(name,action);
            return loader;
        }

        public void SetLoader<T>(string name, Action<T> action) where T : MonoBehaviour
        {
            m_isDel = false;
            m_action = (obj) =>
            {
                var t = obj.GetComponent<T>();
                action?.Invoke(t);
            };
            LoadResrouce(name);
        }

        public void Del()
        {
            m_isDel = true;
        }
        private void LoadResrouce(string name)
        {
            FrameWork.GetFrameWork().Components.ResourceManager.LoadGameObject(name, (obj) =>
            {
                if (m_isDel)
                    return;
                m_action?.Invoke(obj);
            });
            
        }
    }
}