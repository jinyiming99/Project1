using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrameWork
{
    public class CoroutineWorkerComponent : MonoBehaviour
    {
        private uint m_count = 0;
        private Dictionary<uint, IEnumerator> m_dic = new Dictionary<uint, IEnumerator>();
        public uint CreateSecondTask(Action action)
        {
            var con = SecondWork(action);
            StartCoroutine(con);
            m_dic.Add(m_count++, con);
            return m_count;
        }

        public uint CreateNormalTask(Action action)
        {
            var con = Work(action);
            StartCoroutine(con);
            m_dic.Add(m_count++,con);
            return m_count;
        }

        public void StopTask(uint id)
        {
            if (m_dic.TryGetValue(id, out var v))
            {
                StopCoroutine(v);
                m_dic.Remove(id);
            }
        }

        IEnumerator SecondWork(Action action)
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                action?.Invoke();
            }
        }
        IEnumerator Work(Action action)
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                action?.Invoke();
            }
        }
    }
}
