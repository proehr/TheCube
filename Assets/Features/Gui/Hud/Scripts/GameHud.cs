using DataStructures.Variables;
using TMPro;
using UnityEngine;

public class GameHud : MonoBehaviour
{
    [SerializeField] private CommandModeVariable commandMode;
    [SerializeField] private GameObject ExcavationModeButtonHighlight;
    [SerializeField] private GameObject TransportModeButtonHighlight;

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void OnCommandModeChanged()
    {
        UpdateButtons();
    }

    public void OnCommandModeExcavate()
    {
        commandMode.Set(CommandMode.Excavate);
    }
    
    public void OnCommandModeTransportLine()
    {
        commandMode.Set(CommandMode.TransportLine);
    }
    
    private void UpdateButtons()
    {
        if (!ExcavationModeButtonHighlight || !TransportModeButtonHighlight) return;
        if (!commandMode) return;
        ExcavationModeButtonHighlight.SetActive(commandMode.Get() == CommandMode.Excavate);
        TransportModeButtonHighlight.SetActive(commandMode.Get() == CommandMode.TransportLine);
    }
}
