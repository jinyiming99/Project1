
using System.Collections.Generic;
using UnityEngine;

namespace GameFrameWork.InputComponent
{
    public class InputComponent<T> : MonoBehaviour
    {
        private Dictionary<KeyCode, T> dic = new Dictionary<KeyCode, T>();
        
    
        public virtual void CallBack(T action)
        {
            
        }
        
        public virtual void Init()
        {}
    
        public void Add(KeyCode code, T t)
        {
            if (dic.ContainsKey(code))
            {
                dic[code] = t;
            }
            else
            {
                dic.Add(code,t);
            }
        }
    
        private void Update()
        {
            
            foreach (var pair in dic)
            {
                if (Input.GetKey( pair.Key))
                {
                    //try
                    {
                        CallBack(pair.Value);
                    }
                    // catch (Exception e)
                    // {
                    //     DebugHelper.LogError(e.ToString());
                    // }
                    
                }
            }
        }
    
        public void Remove(KeyCode code)
        {
            if (dic.ContainsKey(code))
                dic.Remove(code);
        }
    }
}


