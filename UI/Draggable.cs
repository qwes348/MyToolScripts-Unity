using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 이 컴포넌트는 드레그 되는 UI에 이벤트를 넣어 주는 역할이다
    // ex => 인벤토리 아이템

    [SerializeField] protected Canvas canvas;
    protected RectTransform rectTransform;

    protected virtual void Awake() =>    
        rectTransform = GetComponent<RectTransform>();
    
    // 드래그 시작 시 수행할 작업
    public virtual void OnBeginDrag(PointerEventData eventData) { }

    // 드래그 중일 때 UI 위치 업데이트
    public virtual void OnDrag(PointerEventData eventData) => 
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    
    // 드래그 종료 시 수행할 작업
    public abstract void OnEndDrag(PointerEventData eventData);
}