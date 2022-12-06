using UnityEngine;

namespace Game
{
    public class GameObjectMesh : GameObjectUnit
    {
        public void SetParent(Transform t)
        {
            m_transform.parent = t;
        }
    }
}