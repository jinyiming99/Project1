using UnityEngine;

namespace GameFrameWork.SingleInstance
{ 
/// <summary>
/// MonoBehaviour 的简单单例 ,不会在load scene的时候删除的持久单例
/// </summary>
/// <typeparam name="T">禁止使用不确定的模板类型</typeparam>
    public class MonoSingleInstanceDontDestroy<T> : MonoBehaviour where T:MonoBehaviour
    {
        protected static Transform s_transform = null;
        static string s_name = string.Empty;

        // protected void Awake()
        // {
        //     s_instance = GetComponent<T>();
        //     if (s_instance != null)
        //     {
        //         GameObject.DontDestroyOnLoad(s_instance);
        //     }
        //     OnAwake();
        //    
        // }

        public virtual bool IsNeedRegister => true;
        protected virtual void OnAwake() {  }

        protected static T s_instance = null;
        public static T GetInstance()
        {

            if (s_instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name.ToString();
                s_transform = go.transform;
                s_name = go.name;
                s_instance = go.AddComponent<T>();
                GameObject.DontDestroyOnLoad(go);
            }
            return s_instance;
        }

        public static Transform sTransform()
        {
            return s_transform;
        }

		public static T Instance
		{
			get { return GetInstance(); }
		}

        public void Release()
        {
            GameObject.DestroyImmediate(s_instance.gameObject);
            DestroyThis();
        }

        public string GetName()
        {
            return s_name;
        }

        public void DestroyThis()
        {
            if (s_instance != null)
                GameObject.DestroyImmediate(s_instance);
            s_instance = null;
        }
        protected void OnDestroy()
        {
            OnRelease();
        }
        protected virtual void OnRelease()
        {

        }
    }
}