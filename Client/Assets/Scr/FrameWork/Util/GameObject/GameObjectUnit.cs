using UnityEngine;
/// <summary>
/// 游戏基础对象
/// </summary>
public class GameObjectUnit : MonoBehaviour
{
    [HideInInspector]
    public Transform m_transform;
    
    private void Awake()
    {
        m_transform = transform;
        OnAwake();
    }

    protected void SetParent(Transform t)
    {
        m_transform.parent = t;
    }

    protected virtual void OnAwake()    {}

    public static T LoadResource<T>(string name) where T : UnityEngine.Object
    {
        return GameFrameWork.ResourceHelper.LoadResource<T>(name);
    }

    public static void LoadResourceAsync<T>(string name, System.Action<T> action) where T : Object
    {
        GameFrameWork.ResourceHelper.LoadResourceAsync(name,action);
    }

    public static T CreateNewGameObject<T>() where T: MonoBehaviour
    {
        GameObject go = new GameObject();
        T t = go.AddComponent<T>();
        return t;
    }
}
