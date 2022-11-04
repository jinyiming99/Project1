using System;
using System.Collections.Generic;

namespace GameFrameWork.RequirementSystem
{
    public class RequirementSystemManager<T,U> where T:IRequirement
    {
        private static RequirementSystem<T,U> _system = new RequirementSystem<T,U>();

        public static void Add(T data,U action)
        {
            _system.Add(data,action);
        }

        public static List<U> Get(T data)
        {
            return _system.Get(data);
        }

        public static void Remove(T data)
        {
            _system.Remove(data);
        }

        public static void Clear(T data)
        {
            _system.Clear();
        }
    }
    public class RequirementSystem<T,U> where T:IRequirement
    {
        private Dictionary<T, List<U>> m_dic = new Dictionary<T, List<U>>();

        public void Add(T t,U action)
        {
            if (m_dic.TryGetValue(t ,out var v))
            {
                if (!v.Contains(action))
                    v.Add(action);
            }
            else
            {
                m_dic.Add(t,new List<U>(){action});
            }
        }

        public List<U> Get(T t)
        {
            if (m_dic.TryGetValue(t, out var v))
                return v;
            return null;
        }

        public void Remove(T t)
        {
            m_dic.Remove(t) ;
        }

        public void Clear()
        {
            m_dic.Clear();
        }
    }
}