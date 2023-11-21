using UnityEngine.EventSystems;

namespace Src.UI.Base
{
    public class ISelectComponentBase : IPressComponentBase , IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
    {
        protected bool _isPointerEnter = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerEnter = true;
            if (!_isPressing)
                SetState(UIComponentStates.hightLight,eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isPointerEnter = false;
            if (!_isPressing)
                SetState(UIComponentStates.normal,eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            
        }
    }
}