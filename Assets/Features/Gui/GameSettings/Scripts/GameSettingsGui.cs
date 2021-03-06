using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameSettingsGui : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown;
    [SerializeField] private TMP_Dropdown fullScreenDropDown;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private UnityEvent<bool>[] languageSelection;
    
    private GameSettings_SO gameSettingsSO;

    private void OnEnable()
    {
        this.gameSettingsSO = GameSettings_SO.Instance;
        UpdateValues();
    }

    public void UpdateValues()
    {
        InitResolutionDropdown();
        InitFullScreenDropdown();
        UpdateMasterVolumeSlider();
        UpdateMusicVolumeSlider();
        UpdatEffectsVolumeSlider();
        UpdateLanguageSelection();
    }

    private void OnDisable()
    {
        gameSettingsSO.SaveToJSON();
    }

    private void InitResolutionDropdown()
    {
        this.resolutionDropDown.ClearOptions();
        List<Resolution> resolutions = gameSettingsSO.UpdateAndGetAvailableResolutions();
        var resolutionEntries = new List<string>();
        for (var index = 0; index < resolutions.Count; index++)
        {
        	var resolution = resolutions[index];
            resolutionEntries.Add(resolution.ToString());
        }
        this.resolutionDropDown.AddOptions(resolutionEntries);
        this.resolutionDropDown.SetValueWithoutNotify(gameSettingsSO.currentResolutionIndex);
    }
    
    private void InitFullScreenDropdown()
    {
        this.fullScreenDropDown.ClearOptions();
        List<Resolution> resolutions = gameSettingsSO.UpdateAndGetAvailableResolutions();
        var screenModeEntries = new List<string>()
        {
            "Fullscreen",
            "Fullscreen window",
            "Maximized window",
            "Windowed"
        };
        
        this.fullScreenDropDown.AddOptions(screenModeEntries);
        this.fullScreenDropDown.SetValueWithoutNotify(gameSettingsSO.currentfullScreenMode);
    }
    
    private void UpdateMasterVolumeSlider()
    {
        this.masterVolumeSlider.SetValueWithoutNotify(gameSettingsSO.masterVolume);
    }
    
    private void UpdateMusicVolumeSlider()
    {
        this.musicVolumeSlider.SetValueWithoutNotify(gameSettingsSO.musicVolume);
    }
    
    private void UpdatEffectsVolumeSlider()
    {
        this.effectsVolumeSlider.SetValueWithoutNotify(gameSettingsSO.effectsVolume);
    }
    
    private void UpdateLanguageSelection()
    {
        var indexToHighlight = this.gameSettingsSO.currentLanguage switch
        {
            "en" => 0,
            "de" => 1,
            _ => 0
        };
        for (var index = 0; index < this.languageSelection.Length; index++)
        {
            var highlightEvent = this.languageSelection[index];
            highlightEvent.Invoke(index == indexToHighlight);

        }
    }
}
