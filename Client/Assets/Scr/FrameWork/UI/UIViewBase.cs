using System;
using UnityEngine;

namespace GameFrameWork.UI
{
    public class UIViewBase : GameObjectUnit
    {
        private AnimationControl _animator;

        protected override void OnAwake()
        {
            _animator = m_gameObject.GetComponent<AnimationControl>();
        }
        
        
    }
}