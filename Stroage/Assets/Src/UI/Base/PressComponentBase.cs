using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Base
{
    [System.Serializable]
    public class PressComponentBase : IUIComponentBase , IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
    {
        protected Action _clickAction;
        public Action ClickAction
        {
            get { return _clickAction; }
            set { _clickAction = value; }
        }
        
        protected bool _isPressing = false;
        protected bool _isPointerEnter = false;

        [SerializeField]
        public UIExpressionComponent _expressionComponent;
        

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerEnter = true;
            if (!_isPressing)
                SetState(UIComponentStates.hightLight,eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _isPointerEnter = false;
            if (!_isPressing)
                SetState(UIComponentStates.normal,eventData);
        }

        public virtual void OnPointerMove(PointerEventData eventData)
        {
            
        }

        protected override void SetState(UIComponentStates state, PointerEventData eventData = null)
        {
            _expressionComponent.OnStateChange(state);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isPressing = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!_isPointerEnter)
                return;
            if (_isPressing)
                _clickAction?.Invoke();
            SetState(UIComponentStates.press);
            _isPressing = false;
        }
    }
}