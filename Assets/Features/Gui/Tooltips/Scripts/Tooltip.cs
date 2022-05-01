using DataStructures.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Vector2 offsetToCursor;
    [SerializeField] private StringVariable tooltipStringVariable;
    [SerializeField] private TMP_Text tooltipText;
    [SerializeField] private GameObject visibility;
    [SerializeField] private RectTransform backgroundRect;
    
    private void LateUpdate()
    {
        if (!visibility.activeSelf) return;
        if (string.IsNullOrEmpty(tooltipStringVariable.Get()))
        {
            visibility.SetActive(false);
            return;
        }
        if (Mouse.current == null) return;
        transform.position = Mouse.current.position.ReadValue() + this.offsetToCursor;
    }

    public void OnTooltipChanged()
    {
        var tooltipSet = !string.IsNullOrEmpty(tooltipStringVariable.Get());
        this.tooltipText.text = tooltipStringVariable.Get();
        UpdateBackgroundSizeToText();

        if (visibility.activeSelf != tooltipSet)
        {
            visibility.SetActive(tooltipSet);
        }
    }

    private void UpdateBackgroundSizeToText()
    {
        var preferredValues = this.tooltipText.GetPreferredValues();
        this.backgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, preferredValues.x);
        this.backgroundRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, preferredValues.y);
    }
}