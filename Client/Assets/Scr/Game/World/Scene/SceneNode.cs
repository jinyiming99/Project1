using UnityEngine;

namespace Game.World.Scene
{
    public class SceneNode<T>
    {
        protected T m_data;
        public T Data => m_data;

        private bool m_isCreated = false;
        public bool IsCreated => m_isCreated;
        public virtual void Create()
        {
            m_isCreated = true;
        }

    }
}