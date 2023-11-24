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

        protected virtual bool IsSelected => false;
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _expressionComponent?.Release();
            _isPressing = false;
            _isPointerEnter = false;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            SetState(UIComponentStates.normal);
            
            _isPressing = false;
            _isPointerEnter = false;
        }

        private void OnDestroy()
        {
            _expressionComponent?.Release();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerEnter = true;
            
            if (!_isPressing)
                SetState(IsSelected?UIComponentStates.selected: UIComponentStates.hightLight,eventData);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _isPointerEnter = false;
            if (!_isPressing)
                SetState(IsSelected?UIComponentStates.selected:UIComponentStates.normal,eventData);
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
            SetState(IsSelected?UIComponentStates.selected:UIComponentStates.press);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (!_isWorking) return;
            _isPressing = false;
            SetState(IsSelected?UIComponentStates.selected:_isPointerEnter? UIComponentStates.hightLight:UIComponentStates.normal,eventData);
            if (_isPointerEnter)
                OnClick();
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
                    SetState(IsSelected?UIComponentStates.selected:_isPointerEnter? UIComponentStates.hightLight:UIComponentStates.normal);
                }
            }
        }

        protected abstract void OnClick();
    }
}