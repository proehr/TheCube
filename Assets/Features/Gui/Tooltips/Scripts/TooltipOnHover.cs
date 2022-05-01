using DataStructures.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string tooltipString;
    [SerializeField] private StringVariable tooltipStringVariable;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.tooltipStringVariable != null)
        {
            this.tooltipStringVariable.Set(this.tooltipString);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.tooltipStringVariable != null && this.tooltipStringVariable.Get() == tooltipString)
        {
            this.tooltipStringVariable.Set(string.Empty);
        }
    }
    
    public void UpdateString(string newString)
    {
        tooltipString = newString;
    }
}
