using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSettingsGui : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    
    private GameSettings_SO gameSettingsSO;

    private void OnEnable()
    {
        this.gameSettingsSO = GameSettings_SO.Instance;
        InitResolutionDropdown();
    }

    private void InitResolutionDropdown()
    {
        this.resolutionDropDown.ClearOptions();
        List<string> resolutionEntries = gameSettingsSO.UpdateAvailableResolutionsAndGetDisplayNames();
        this.resolutionDropDown.AddOptions(resolutionEntries);
        this.resolutionDropDown.SetValueWithoutNotify(gameSettingsSO.currentResolutionIndex);
    }

    public void OnResolutionDropDownChanged (int index)
    {
        gameSettingsSO.SetResolution(index);
    }
}
