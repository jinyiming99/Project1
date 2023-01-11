using System.Collections.Generic;
using UnityEngine;

namespace GameFrameWork
{
    public class GameObjectInstance : SingleInstance.MonoSingleInstanceDontDestroy<GameObjectInstance>
    {
        private Dictionary<string, MonoBehaviour> m_dic = new Dictionary<string, MonoBehaviour>();

        protected override void OnAwake()
        {
            base.OnAwake();
            gameObject.hideFlags = HideFlags.HideAndDontSave;
        }

        public T AddComponent<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            if (m_dic.TryGetValue(type.Name, out var outData))
            {
                if (outData is T data)
                    return data;
                else
                    return null;
            }
            else
            {
                T t = gameObject.AddComponent<T>();
                m_dic.Add(type.Name,t);
                return t;
            }
        }

        public T GetComponent<T>() where T : MonoBehaviour
        {
            var type = typeof(T);
            m_dic.TryGetValue(type.Name, out var outData);
            if (outData is T data)
                return data;
            else
                return null;
        }

        protected override void OnRelease()
        {
            foreach (var v in m_dic)
            {
                Destroy(v.Value.gameObject);
            }
        }
    }
}