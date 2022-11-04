using UnityEngine;

namespace Game.Characters
{
    public class CharacterBase<T> : SceneObject<T> where T:SceneObjectData,new()
    {
        protected AnimationControl m_animator;
        public AnimationControl animator => m_animator;

        protected override void LoadOver(GameObject go)
        {
            base.LoadOver(go);
            //m_animator = m_mesh.AddComponent<anima>()
        }

        public virtual void MoveTo(Vector3 pos, Vector3 dir)
        {
            
        }
    }
}