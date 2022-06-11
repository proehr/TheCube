using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UnityEvent onMouseClick;
    [SerializeField] private UnityEvent onMouseDown;
    [SerializeField] private UnityEvent onMouseUp;
    [SerializeField] private UnityEvent onMouseEnter;
    [SerializeField] private UnityEvent onMouseExit;

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        onMouseExit?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onMouseClick?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onMouseDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.onMouseUp?.Invoke();
    }
}
