using System.Collections.Generic;
using UnityEngine;

namespace GameFrameWork.Containers
{
    public class SwapDictionary<Key,Value> : SwapContainer<Dictionary<Key,Value>>
    {
        public void Add(Key key, Value value)
        {
            lock (m_workingDataContainer)
            {
                m_waitingDataContainer.Add(key,value);
            }
        }
        
        public void AddRange(Dictionary<Key, Value> dic)
        {
            lock (m_workingDataContainer)
            {
                foreach (var value in dic)
                {
                    m_workingDataContainer.Add(value.Key,value.Value);
                }
            }
        }

        public void Remove(Key key)
        {
            lock (m_workingDataContainer)
            {
                m_waitingDataContainer.Remove(key);
            }
        }

        public bool TryGetValue(Key key, out Value value)
        {
            lock (m_workingDataContainer)
            {
                return m_workingDataContainer.TryGetValue(key, out value);
            }
        }

        public override int WaitingCount()
        {
            return m_waitingDataContainer.Count;
        }

        public override int WorkingCount()
        {
            return m_workingDataContainer.Count;
        }

        public void Clear()
        {
            m_waitingDataContainer.Clear();
            m_workingDataContainer.Clear();
        }
    }
}