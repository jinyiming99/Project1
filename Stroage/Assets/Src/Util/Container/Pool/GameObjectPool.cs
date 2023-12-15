using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public class GameObjectPool : MonoBehaviour
    {
        public Stack<GameObject> m_Pool = new Stack<GameObject>();
        
        public void Push(GameObject obj)
        {
            obj.SetActive(false);
            if (!m_Pool.Contains(obj))
            {
                m_Pool.Push(obj);
            }
        }
        public GameObject Pop()
        {
            if (m_Pool.Count > 0)
            {
                var obj = m_Pool.Pop();
                obj.SetActive(true);
                return obj;
            }
            return null;
        }

        public void Clear()
        {
            int count = m_Pool.Count;
            for (int i = 0; i < count; i++)
            {
                var t = m_Pool.Pop();
                Destroy(t.gameObject);
            }
        }

        private void OnDestroy()
        {
            Clear();
        }
    }
}