using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

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
	private class SettingsData : ICloneable
	{
		//Audio
		[SerializeField, ReadOnly] public float masterVolume = 1;
		[SerializeField, ReadOnly] public float musicVolume = 1;
		[SerializeField, ReadOnly] public float effectsVolume = 1;
		//Video
		[SerializeField, ReadOnly] public int resolutionWidth;
		[FormerlySerializedAs("resolutioHeight")] [SerializeField, ReadOnly] public int resolutionHeight;
		[SerializeField, ReadOnly] public int fullScreenMode;
		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
	[SerializeField, ReadOnly] private SettingsData settingsData = new SettingsData();
	
	//Audio
	[SerializeField] private AudioMixer audioMixer;
	public float masterVolume => settingsData.masterVolume;
	public float musicVolume  => settingsData.musicVolume;
	public float effectsVolume => settingsData.effectsVolume;
	//Audio Defaults
	[SerializeField] private float defaultVolumeAll = 1;
	[SerializeField] private float defaultVolumeMusic = 1;
	[SerializeField] private float defaultVolumeEffects = 1;
	
	//Video
	private List<Resolution> availableResolutions = new List<Resolution>();
	[NonSerialized] private int _currentResolutionIndex = 0;
	[NonSerialized] private int _currentfullScreenMode = 0;
	public int currentResolutionIndex => _currentResolutionIndex;
	public int currentfullScreenMode => _currentfullScreenMode;

	//Video Defaults
	[SerializeField] private FullScreenMode defaultFullScreenMode = FullScreenMode.MaximizedWindow;

	private void Init()
	{
		UpdateAvailableResolutions();
		if (LoadFromJSON())
		{
			var value = Mathf.Clamp(masterVolume, 0.0001f, 1f);
			audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
			value = Mathf.Clamp(musicVolume, 0.0001f, 1f);
			audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
			value = Mathf.Clamp(effectsVolume, 0.0001f, 1f);
			audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
			Screen.SetResolution(this.settingsData.resolutionWidth, this.settingsData.resolutionHeight, (FullScreenMode)this.settingsData.fullScreenMode);
		}
		else
		{
			ResetToDefaults();
		}
		UpdateAvailableResolutions();
		_currentfullScreenMode = (int)Screen.fullScreenMode;
	}
	
	private void UpdateAvailableResolutions()
	{
		for (var index = 0; index < Screen.resolutions.Length; index++)
		{
			var resolution = Screen.resolutions[index];
			if (!this.availableResolutions.Contains(resolution))
			{
				this.availableResolutions.Add(resolution);
			}
			if (Screen.width.Equals(resolution.width) && Screen.height.Equals(resolution.height))
			{
				_currentResolutionIndex = index;
			}
		}
	}

	public List<Resolution> UpdateAndGetAvailableResolutions()
	{
		UpdateAvailableResolutions();
		return this.availableResolutions;
	}
	
	public void SetResolution(Int32 index)
	{
		if (this.availableResolutions.Count <= index || index < 0) return;
		var resolution = this.availableResolutions[index];
		this.SetResolution(resolution);
	}
	
	public void SetResolution(Resolution resolution)
	{
		if (!this.availableResolutions.Contains(resolution))
		{
			Debug.LogError("Tried to set unavailable resolution.");
			return;
		}
		this.settingsData.resolutionWidth = resolution.width;
		this.settingsData.resolutionHeight = resolution.height;
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
	}
	
	public void SetFullScreenModeFromInt(Int32 modeInt)
	{
		if (modeInt < 0 || modeInt > 3)
		{
			modeInt = 0;
		}
		FullScreenMode mode = (FullScreenMode)modeInt;
		this.settingsData.fullScreenMode = modeInt;
		Screen.fullScreenMode = mode;
	}
	
	public void SetFullScreenMode(FullScreenMode mode)
	{
		var modeInt = (int)mode;
		this.settingsData.fullScreenMode = modeInt;
		Screen.fullScreenMode = mode;
	}

	public void SetMasterVolume(float value)
	{
		value = Mathf.Clamp(value, 0.0001f, 1f);
		this.settingsData.masterVolume = value;
		if (!audioMixer) return;
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
	}

	public void SetMusicVolume (float value)
	{
		value = Mathf.Clamp(value, 0.0001f, 1f);
		this.settingsData.musicVolume = value;
		if (!audioMixer) return;
		audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
	}

	public void SetEffectsVolume (float value)
	{
		value = Mathf.Clamp(value, 0.0001f, 1f);
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
		SetResolution(Screen.resolutions[Screen.resolutions.Length - 1]);
		SetFullScreenMode(this.defaultFullScreenMode);
	}
	
	public void ResetAudioSettingsToDefault()
	{
		SetMasterVolume(this.defaultVolumeAll);
		SetMusicVolume(this.defaultVolumeMusic);
		SetEffectsVolume(this.defaultVolumeEffects);
	}

	public bool LoadFromJSON()
	{
		var sFileName = this.persistentDataPath + this.fileName;
		if (!File.Exists(sFileName))
		{
			return false;
		}
		JsonUtility.FromJsonOverwrite(File.ReadAllText(sFileName), this.settingsData);
		return this.settingsData != null;
	}

	public void SaveToJSON()
	{
		if (!Application.isPlaying) return;
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