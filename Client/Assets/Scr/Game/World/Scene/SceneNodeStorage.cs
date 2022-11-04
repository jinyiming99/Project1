using System.Collections.Generic;
using System.Numerics;


namespace Game.World.Scene
{
    public class SceneNodeStorage<T> 
    {
        private Dictionary<Vector2, SceneNode<T>> m_dic = new Dictionary<Vector2, SceneNode<T>>();

        public bool TryGetValue(Vector2 pos, out SceneNode<T> node)
        {
            return m_dic.TryGetValue(pos, out node);
        }

        public SceneNode<T> Create(Vector2 pos)
        {
            if (TryGetValue(pos, out var node))
            {
                return node;
            }

            node = new SceneNode<T>();
            return node;
        }
    }
}