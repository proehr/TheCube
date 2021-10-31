using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class GameSettings_SO : ScriptableObject
{
	private static GameSettings_SO instance;
	public static GameSettings_SO Instance
	{
		get
		{
			if (!instance)
			{
				instance = Resources.Load<GameSettings_SO>("Assets/Features/Gui/GameSettings/Data/GameSettings.asset");
				if (!instance)
				{
					instance = Resources.FindObjectsOfTypeAll<GameSettings_SO>().FirstOrDefault();
				}
				if (instance)
				{
					instance.Init();
				}
			}
#if UNITY_EDITOR
			if (!instance)
			{
				InitializeFromDefault(
					UnityEditor.AssetDatabase.LoadAssetAtPath<GameSettings_SO>("Assets/Features/Gui/GameSettings/Data/GameSettings.asset"));
				if (instance)
				{
					instance.Init();
				}
			}
#endif
			return instance;
		}
	}
	
	//Saving
	[SerializeField] private string dataPathFolder = "/Settings/";
	[SerializeField] private string fileName = "SettingsData";

	private string persistentDataPath => Application.persistentDataPath + this.dataPathFolder;

	[Serializable]
	private class SettingsData
	{
		//Audio
		[SerializeField, ReadOnly] public float masterVolume = 1;
		[SerializeField, ReadOnly] public float musicVolume = 1;
		[SerializeField, ReadOnly] public float effectsVolume = 1;
		//Video
		[SerializeField, ReadOnly] public int resolutionWidth;
		[SerializeField, ReadOnly] public int resolutioHeight;
		[SerializeField, ReadOnly] public FullScreenMode fullScreenMode;
	}
	[SerializeField, ReadOnly] private SettingsData settingsData = new SettingsData();
	
	//Audio
	[SerializeField] private AudioMixer audioMixer;
	public float masterVolume => settingsData.masterVolume;
	public float musicVolume  => settingsData.musicVolume;
	private float effectsVolume=> settingsData.effectsVolume;
	//Audio Defaults
	[SerializeField] private float defaultVolumeAll = 1;
	[SerializeField] private float defaultVolumeMusic = 1;
	[SerializeField] private float defaultVolumeEffects = 1;
	
	//Video
	private List<Resolution> availableResolutions = new List<Resolution>();
	[SerializeField, ReadOnly] private int _currentResolutionIndex = 0;
	public int currentResolutionIndex => _currentResolutionIndex;

	//Video Defaults
	[SerializeField] private FullScreenMode defaultFullScreenMode = FullScreenMode.MaximizedWindow;

	private void Init()
	{
		if (File.Exists(this.persistentDataPath + this.fileName))
		{
			LoadFromJSON();
		}
		else
		{
			ResetToDefaults();
		}
	}

	private void OnDisable()
	{
		SaveToJSON();
	}

	public List<string> UpdateAvailableResolutionsAndGetDisplayNames()
	{
		var resolutionEntries = new List<string>();
		for (var index = 0; index < Screen.resolutions.Length; index++)
		{
			var resolution = Screen.resolutions[index];
			this.availableResolutions.Add(resolution);
			resolutionEntries.Add(resolution.ToString());
			if (Screen.currentResolution.Equals(resolution))
			{
				_currentResolutionIndex = index;
			}
		}
		return resolutionEntries;
	}
	
	public void SetResolution(int index)
	{
		if (this.availableResolutions.Count <= index) return;
		var resolution = this.availableResolutions[index];
		this.SetResolution(resolution);
	}
	
	public void SetResolution(Resolution resolution)
	{
		this.settingsData.resolutionWidth = resolution.width;
		this.settingsData.resolutioHeight = resolution.height;
		Screen.SetResolution(resolution.width, resolution.height, this.settingsData.fullScreenMode);
	}
	
	public void SetFullScreenMode(FullScreenMode mode)
	{
		this.settingsData.fullScreenMode = mode;
		Screen.fullScreenMode = mode;
	}

	public void SetMasterVolume(float value)
	{
		this.settingsData.masterVolume = value;
		if (!audioMixer) return;
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
	}

	public void SetMusicVolume (float value)
	{
		this.settingsData.musicVolume = value;
		if (!audioMixer) return;
		audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
	}

	public void SetEffectsVolume (float value)
	{
		this.settingsData.effectsVolume = value;
		if (!audioMixer) return;
		audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
	}
	
	public void ResetToDefaults()
	{
		ResetVideoSettingsToDefault();
		ResetAudioSettingsToDefault();
	}

	public void ResetVideoSettingsToDefault()
	{
		SetResolution(Screen.resolutions[Screen.resolutions.Length]);
		SetFullScreenMode(this.defaultFullScreenMode);
	}
	
	public void ResetAudioSettingsToDefault()
	{
		SetMasterVolume(this.defaultVolumeAll);
		SetMusicVolume(this.defaultVolumeMusic);
		SetEffectsVolume(this.defaultVolumeEffects);
	}

	public void LoadFromJSON()
	{
		JsonUtility.FromJsonOverwrite(File.ReadAllText(this.persistentDataPath + this.fileName), this.settingsData);
	}

	public void SaveToJSON()
	{
		var path = this.persistentDataPath;
		if (!Directory.Exists(path))
			Directory.CreateDirectory(path);
		Debug.LogFormat("Saving game settings to {0}", path);
		File.WriteAllText(path + this.fileName, JsonUtility.ToJson(this.settingsData, true));
	}
	
#if UNITY_EDITOR
	public static void InitializeFromDefault(GameSettings_SO settings)
	{
		if (instance) DestroyImmediate(instance);
		instance = Instantiate(settings);
		instance.hideFlags = HideFlags.HideAndDontSave;
	}
#endif
}