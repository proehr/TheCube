using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Features.Audio
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundEmitter : MonoBehaviour
	{
		private AudioSource audioSource;

		public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;
		private AudioCueKey audioCueKey = AudioCueKey.Invalid;

		public AudioCueKey AudioCueKey => audioCueKey;
		public void SetAudioCueKey(AudioCueKey key)
		{
			audioCueKey = key;
		}

		private void Awake()
		{
			this.audioSource = this.GetComponent<AudioSource>();
			this.audioSource.playOnAwake = false;
		}
		
		public void PlayAudioClip(AudioClipData clipData, AudioConfigurationSO settings, bool hasToLoop, Vector3 position = default)
		{
			this.audioSource.clip = clipData.audioClip;
			settings.ApplyTo(this.audioSource);
			if (settings.SpatialBlend > 0)
			{
				this.audioSource.transform.position = position;
			}
			this.audioSource.loop = hasToLoop;
			this.audioSource.time = 0f; //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
			this.audioSource.volume = Random.Range(clipData.volume.minValue, clipData.volume.maxValue);
			this.audioSource.pitch = Random.Range(clipData.pitch.minValue, clipData.pitch.maxValue);
			this.audioSource.panStereo = Random.Range(clipData.pan.minValue, clipData.pan.maxValue);
			this.audioSource.Play();

			if (!hasToLoop)
			{
				StartCoroutine(FinishedPlaying(clipData.audioClip.length));
			}
		}
		
		public void FadeInAudioClip(AudioClipData clipData, AudioConfigurationSO settings, bool hasToLoop, Vector3 position = default)
		{
			this.audioSource.clip = clipData.audioClip;
			settings.ApplyTo(this.audioSource);
			if (settings.SpatialBlend > 0)
			{
				this.audioSource.transform.position = position;
			}
			this.audioSource.loop = hasToLoop;
			this.audioSource.time = 0f; //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
			this.audioSource.volume = 0;
			this.audioSource.pitch = Random.Range(clipData.pitch.minValue, clipData.pitch.maxValue);
			this.audioSource.panStereo = Random.Range(clipData.pan.minValue, clipData.pan.maxValue);
			this.audioSource.Play();

			if (!hasToLoop)
			{
				StartCoroutine(FinishedPlaying(clipData.audioClip.length));
			}

			StartCoroutine(DoFade(Random.Range(clipData.volume.minValue, clipData.volume.maxValue)));
		}

		public void FadeOutAudioClip()
		{
			if (!this.audioSource.isPlaying) return;
			
			StartCoroutine(DoFade(0, -0.1f));
		}
		
		IEnumerator DoFade(float goalVolume, float multiplier = 0.1f)
		{
			float currentVol = this.audioSource.volume;
			while (Math.Abs(this.audioSource.volume - goalVolume) > 0.01f)
			{
				this.audioSource.volume = currentVol;
				currentVol += Time.deltaTime * multiplier;
				yield return new WaitForEndOfFrame();
			}
			this.audioSource.volume = goalVolume;
			if (Mathf.Approximately(this.audioSource.volume,0))
			{
				OnFinished();
			}
			else
			{
				FadeOutAudioClip();
			}
		}

		public AudioClip GetClip()
		{
			return this.audioSource.clip;
		}
	
		public void Pause()
		{
			this.audioSource.Pause();
		}
		
		public void Resume()
		{
			this.audioSource.Play();
		}

		public void Stop()
		{
			this.audioSource.Stop();
		}

		public void Finish()
		{
			if (this.audioSource.loop)
			{
				this.audioSource.loop = false;
				float timeRemaining = this.audioSource.clip.length - this.audioSource.time;
				StartCoroutine(FinishedPlaying(timeRemaining));
			}
		}

		public bool IsPlaying()
		{
			return this.audioSource.isPlaying;
		}

		public bool IsLooping()
		{
			return this.audioSource.loop;
		}

		IEnumerator FinishedPlaying(float clipLength)
		{
			yield return new WaitForSeconds(clipLength);

			OnFinished();
		}

		private void OnFinished()
		{
			OnSoundFinishedPlaying.Invoke(this);
		}
	}
}
