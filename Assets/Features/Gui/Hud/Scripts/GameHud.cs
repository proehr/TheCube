using DataStructures.Variables;
using TMPro;
using UnityEngine;

public class GameHud : MonoBehaviour
{
    [SerializeField] private CommandModeVariable commandMode;
    [SerializeField] private IntVariable totalWorkerAmount;
    [SerializeField] private GameObject ExcavationModeButtonHighlight;
    [SerializeField] private GameObject TransportModeButtonHighlight;
    [SerializeField] private TMP_Text WorkerAmountLabel;

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void OnCommandModeChanged()
    {
        UpdateButtons();
    }
    
    public void OnWorkerAmountChanged()
    {
        if (!WorkerAmountLabel) return;
        WorkerAmountLabel.text = totalWorkerAmount.Get().ToString();
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
