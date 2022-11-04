using UnityEngine;
using System.Collections;

//////////////////////
///持久单例,不需要继承MonoBehaviour的
///////////////////////////
namespace GameFrameWork.SingleInstance
{
    public class SingleInstance<T> where T :class, new()
    {
        protected static T s_instance = default(T);
        static string s_name;
        
        public static T GetInstance()
        {
            if (s_instance == null)
            {
                s_name = typeof(T).Name.ToString();
                s_instance = new T();                
            }
            return s_instance;
        }

        public static T Instance
        {
            get
            {
                return GetInstance();
            }
        }


        public void Release()
        {
            OnRelease();
            Destroy();
        }

        public virtual void OnRelease()
        {

        }

        public string GetName()
        {
            return s_name;
        }
        
        public virtual bool IsNeedRegister => true;

        public void Destroy()
        {
            s_instance = null;
        }
    }
}
