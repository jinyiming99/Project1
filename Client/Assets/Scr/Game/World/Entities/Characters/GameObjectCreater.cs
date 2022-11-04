using UnityEngine;

namespace Game.Characters
{
    public static class GameObjectCreater
    {
        public static T Creater<T>() where T : MonoBehaviour
        {
            GameObject go = new GameObject();
            T t = go.AddComponent<T>();
            return t;
        }
    }
}