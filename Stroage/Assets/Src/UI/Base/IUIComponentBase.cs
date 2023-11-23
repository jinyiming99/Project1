using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Base
{
    public abstract class IUIComponentBase : MonoBehaviour
    {
        protected UIComponentStates _state;
        protected bool _isWorking = true;
        public virtual bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; }
        }
        protected virtual void OnDisable()
        {
            SetState(UIComponentStates.disable);
        }

        protected void OnEnable()
        {
            SetState(UIComponentStates.normal);
        }


        protected virtual void SetState(UIComponentStates state , PointerEventData eventData = null)
        {
            if (state == _state)
                return;
            _state = state;
        }
    }
}