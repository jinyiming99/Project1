using UnityEngine;

namespace Game.Characters
{
    public class SceneObjectData
    {
        public long m_objectID;
        public Vector3 m_dir;
        public Vector3 m_pos;
        public Vector3 m_scale;

        public SceneObjectData()
        {
            m_dir = Vector3.right;
            m_pos = Vector3.zero;
            m_scale = Vector3.one;
        }

        public SceneObjectData(Vector3 pos)
        {
            m_dir = Vector3.right;
            m_pos = pos;
            m_scale = Vector3.one;
        }
    }
}