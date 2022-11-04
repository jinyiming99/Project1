using UnityEngine;

namespace Game.Characters
{
    public class SceneObject<T> : GameObjectUnit where T:new()
    {
        protected T m_data;

        protected GameObject m_mesh;

        protected override void OnAwake()
        {
            m_data = new T();
        }
        public void LoadMesh(string path)
        {
            LoadResourceAsync<GameObject>(path,LoadOver);
        }

        protected virtual void LoadOver(GameObject go)
        {
            m_mesh = GameObject.Instantiate(go);
            m_mesh.transform.parent = this.m_transform;
        }

        public void SetPos(Vector3 vec)
        {
            transform.position = vec;
        }

        public void LookAt(Vector3 dir)
        {
            transform.LookAt(transform.position + dir);
        }
    }
}