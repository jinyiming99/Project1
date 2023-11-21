using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IDropComponentBase : MonoBehaviour , IBeginDragHandler,IDragHandler,IEndDragHandler
{
    protected bool _isDraging = false;
    public bool IsDraging => _isDraging;
    protected Vector2 _pivot;
    public Vector2 Pivot => _pivot;
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        _isDraging = true;
        _pivot = eventData.position;
    }
    
    public virtual void OnDrag(PointerEventData eventData)
    {
        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _isDraging = false;
    }
    
    protected Vector2 GetPointerDragVector2(Vector2 position)
    {
        Vector2 vector2 = position - _pivot;
        _pivot = position;
        return vector2;
    }
}
