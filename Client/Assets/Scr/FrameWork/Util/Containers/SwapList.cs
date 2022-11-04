using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameFrameWork.Containers
{
    /// <summary>
    /// 交换用list，用于lock的时候，减少lock次数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SwapList<T>
    {
        BetterList<T>[]     m_lists         = new BetterList<T>[2];
        BetterList<T>       m_workingList   = null;
        BetterList<T>       m_waitingList   = null;

        public SwapList()
        {
            m_lists[0] = new BetterList<T>();
            m_lists[1] = new BetterList<T>();
            m_workingList = m_lists[0];
            m_waitingList = m_lists[1];
        }
        /// <summary>
        /// 交换列表
        /// </summary>
        public void Swap()
        {
            lock(m_workingList)
            {
                BetterList<T> temp = m_workingList;
                m_workingList = m_waitingList;
                m_waitingList = temp;
            }
        }
        /// <summary>
        /// 获得长度
        /// </summary>
        /// <returns></returns>
        public int GetWorkingLength()
        {
            return m_workingList.size;
        }
        
        public int GetWaitingLength()
        {
            return m_waitingList.size;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="t"></param>
        public void Add(T t)
        {
            lock (m_workingList)
            {
                m_workingList.Add(t);
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public T[] GetWaitingData()
        {
            return m_waitingList.ToArray();
        }
        /// <summary>
        /// 清除waiting列表数据
        /// </summary>
        public void ClearWaitingData()
        {
            lock (m_workingList)
            {
                m_waitingList.Clear();
            }
        }
        
        public bool TryRemove(T t)
        {
            lock (m_workingList)
            {
                if (m_workingList.Contains(t))
                    return m_workingList.Remove(t);
                return false;
            }
        }

        public void Clear()
        {
            m_waitingList.Clear();
            m_workingList.Clear();
        }
        
    }
}
