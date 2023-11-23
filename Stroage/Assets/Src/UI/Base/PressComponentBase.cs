using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Base
{
    [System.Serializable]
    public abstract class PressComponentBase : IUIComponentBase , IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
    {
       
        protected bool _isPressing = false;
        protected bool _isPointerEnter = false;
        [Header("是否显示行为组件")]
        public bool _isShowExpression = false;
        [ShowIf("_isShowExpression")]
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
            if (!_isWorking) return;
            base.SetState(state,eventData);
            _expressionComponent.OnStateChange(state);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _isPressing = true;
            SetState(UIComponentStates.press);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!_isWorking) return;
            if (_isPointerEnter)
                OnClick();

            _isPressing = false;
            SetState(_isPointerEnter? UIComponentStates.hightLight:UIComponentStates.normal,eventData);
        }

        public override bool IsWorking
        {
            set
            {
                if (!value)
                {
                    SetState(UIComponentStates.disable);
                    _isWorking = value;
                }
                else
                {
                    _isWorking = value;
                    SetState(_isPointerEnter? UIComponentStates.hightLight:UIComponentStates.normal);
                }
            }
        }

        protected abstract void OnClick();
    }
}