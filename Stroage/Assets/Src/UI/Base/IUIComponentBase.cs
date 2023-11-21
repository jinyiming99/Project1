using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Src.UI.Base
{
    public abstract class IUIComponentBase : MonoBehaviour
    {
        protected UIComponentStates _state;
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