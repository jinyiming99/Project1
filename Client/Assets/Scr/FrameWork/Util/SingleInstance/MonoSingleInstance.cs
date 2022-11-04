using UnityEngine;
using System.Collections;


/// <summary>
/// MonoBehaviour 的简单单例 ,会在Load Scene 的时候删除的单例
/// </summary>
/// <typeparam name="T">禁止使用不确定的模板类型</typeparam>
namespace GameFrameWork.SingleInstance
{ 
    public class MonoSingleInstance<T> : MonoBehaviour  where T : MonoBehaviour
    {
        public virtual bool IsNeedRegister => true;
        public static string s_name = string.Empty;

        void Awake()
        {
            s_instance = GetComponent<T>();
            OnAwake();

        }

        protected virtual void OnAwake() {  }

        static T s_instance = null;
        public static T GetInstance()
        {
            if (s_instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name.ToString();
                s_name = go.name;
                s_instance = go.AddComponent<T>();
            }
            return s_instance;
        }

        void OnDestroy()
        {
            OnRelease();
        }

        protected virtual void OnRelease(){}


        public void Release()
        {
            if (s_instance != null && s_instance.gameObject != null)
                GameObject.DestroyImmediate(s_instance.gameObject);
            s_instance = null;
        }

        public string GetName()
        {
            //if (s_instance != null)
            //    return s_instance.gameObject.name;
            //else
            return s_name;
        }

        public static T Instance
		{
			get { return GetInstance(); }
		}
    }
}
