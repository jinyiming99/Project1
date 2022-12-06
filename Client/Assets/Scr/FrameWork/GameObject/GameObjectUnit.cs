using GameFrameWork;
using UnityEngine;
/// <summary>
/// 游戏基础对象
/// </summary>
public class GameObjectUnit : MonoBehaviour
{
    [HideInInspector]
    public Transform m_transform;
    [HideInInspector]
    public GameObject m_gameObject;
    protected void Awake()
    {
        m_transform = transform;
        m_gameObject = gameObject;
        OnAwake();
    }

    protected void SetParent(Transform t)
    {
        m_transform.parent = t;
    }

    protected virtual void OnAwake()    {}
    
    
    public virtual void Show()
    {
        m_gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        m_gameObject.SetActive(false);
    }
    
    
    public static void LoadResourceAsync<T>(string name, System.Action<T> action) where T : Object
    {
        FrameWork.GetFrameWork().Components.ResourceManager.LoadResource<T>(name,action);
    }

    public static T CreateNewGameObject<T>() where T: MonoBehaviour
    {
        GameObject go = new GameObject();
        T t = go.AddComponent<T>();
        return t;
    }
}
