using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Src.UI.Base
{
    public abstract class IPressComponentBase : IUIComponentBase , IPointerDownHandler,IPointerUpHandler
    {
        protected Action _clickAction;
        public Action ClickAction
        {
            get { return _clickAction; }
            set { _clickAction = value; }
        }
        
        protected bool _isPressing = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressing = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isPressing)
                _clickAction?.Invoke();
            SetState(UIComponentStates.press);
            _isPressing = false;
        }
    }
}