using UnityEngine;
using UnityEngine.Audio;

namespace Features.Audio
{
	public class AudioManager : MonoBehaviour
	{
		[Header("SoundEmitters pool")]
		[SerializeField] private SoundEmitterPool_SO pool = default;
		[SerializeField] private int initialSize = 10;

		[Header("Listening on channels")]
		[SerializeField] private AudioCueEventChannel_SO effectsEventChannel = default;
		private SoundEmitterVault soundEmitterVault;
		private SoundEmitter musicSoundEmitter;
	
		[Header("Music")]
		[SerializeField] private AudioConfiguration_SO ingameMusicConfiguration = default;
		[SerializeField] private AudioMixer audioMixer;
		[SerializeField] private GameSettings_SO gameSettings = default;

		private void Awake()
		{
			this.soundEmitterVault = new SoundEmitterVault();
			this.pool.Prewarm(this.initialSize);
			this.pool.SetParent(this.transform);
		}

		private void Start()
		{
			SetAudioVolumesFromSettings();
			this.effectsEventChannel.OnAudioCuePlayRequested += PlayAudioCue;
			this.effectsEventChannel.OnAudioCueStopRequested += StopAudioCue;
			this.effectsEventChannel.OnAudioCueFinishRequested += FinishAudioCue;
		}

		private void SetAudioVolumesFromSettings()
		{
			var value = Mathf.Clamp(this.gameSettings.masterVolume, 0.0001f, 1f);
			this.audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
			value = Mathf.Clamp(this.gameSettings.musicVolume, 0.0001f, 1f);
			this.audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
			value = Mathf.Clamp(this.gameSettings.effectsVolume, 0.0001f, 1f);
			this.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
		}

		private void OnDestroy()
		{
			this.effectsEventChannel.OnAudioCuePlayRequested -= PlayAudioCue;
			this.effectsEventChannel.OnAudioCueStopRequested -= StopAudioCue;
			this.effectsEventChannel.OnAudioCueFinishRequested -= FinishAudioCue;
			this.StopMusic();
		}
		
		public void PlayMusicTrack(AudioCue_SO pTrack)
		{
			if (!this.ingameMusicConfiguration || pTrack == null || pTrack.GetClips() == null || pTrack.GetClips().Length == 0) return;
			if (!this.musicSoundEmitter)
			{
				this.musicSoundEmitter = this.pool.Request();
			}
			this.musicSoundEmitter.FadeInAudioClip(pTrack.GetClips()[0], this.ingameMusicConfiguration, true);
			this.musicSoundEmitter.OnSoundFinishedPlaying += StopMusicEmitter;
		}
	
		private bool StopMusic()
		{
			if (this.musicSoundEmitter != null && this.musicSoundEmitter.IsPlaying())
			{
				this.musicSoundEmitter.Stop();
				return true;
			}
			return false;
		}
		
		public AudioCueKey PlayAudioCue(AudioCue_SO audioCueData, AudioConfiguration_SO config, Vector3 position = default)
		{
			AudioClipData[] clipsToPlay = audioCueData.GetClips();
			SoundEmitter[] soundEmitterArray = new SoundEmitter[clipsToPlay.Length];
			var audioCueKey = this.soundEmitterVault.Add(audioCueData, soundEmitterArray);
			for (int i = 0; i < clipsToPlay.Length; i++)
			{
				soundEmitterArray[i] = this.pool.Request();
				if (soundEmitterArray[i] == null) continue;

				soundEmitterArray[i].PlayAudioClip(clipsToPlay[i], config, audioCueData.looping, position);
				if (!audioCueData.looping)
				{
					soundEmitterArray[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
				}
				soundEmitterArray[i].SetAudioCueKey(audioCueKey);
			}
			return audioCueKey;
		}

		public bool FinishAudioCue(AudioCueKey audioCueKey)
		{
			bool emittersFound = this.soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters);
			if (emittersFound)
			{
				for (int i = 0; i < soundEmitters.Length; i++)
				{
					if (!soundEmitters[i] || audioCueKey != soundEmitters[i].AudioCueKey) continue;
					soundEmitters[i].OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
					soundEmitters[i].Finish();
				}
			}
			else
			{
				Debug.LogWarning("Finishing an AudioCue was requested, but the AudioCue was not found.");
			}

			return emittersFound;
		}

		public bool StopAudioCue(AudioCueKey audioCueKey)
		{
			bool emittersFound = this.soundEmitterVault.Get(audioCueKey, out SoundEmitter[] soundEmitters);

			if (emittersFound)
			{
				for (int i = 0; i < soundEmitters.Length; i++)
				{
					if (!soundEmitters[i] || audioCueKey != soundEmitters[i].AudioCueKey) continue;
					StopAndCleanEmitter(soundEmitters[i]);
				}
				this.soundEmitterVault.Remove(audioCueKey);
			}

			return emittersFound;
		}

		private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
		{
			StopAndCleanEmitter(soundEmitter);
		}

		private void StopAndCleanEmitter(SoundEmitter soundEmitter)
		{
			if (!soundEmitter.IsLooping())
			{
				soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
			}
			soundEmitter.Stop();
			if (this.soundEmitterVault.Count > 0 && soundEmitter.AudioCueKey != AudioCueKey.Invalid)
			{
				this.soundEmitterVault.RemoveEntryIfAllEmittersFinishedPlaying(soundEmitter.AudioCueKey);
			}
			this.pool.Return(soundEmitter);
		}

		private void StopMusicEmitter(SoundEmitter soundEmitter)
		{
			soundEmitter.OnSoundFinishedPlaying -= StopMusicEmitter;
			this.pool.Return(soundEmitter);
			if (this.musicSoundEmitter == soundEmitter)
			{
				this.musicSoundEmitter = null;
			}
		}
	}
}
