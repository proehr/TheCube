using System.Collections;
using UnityEngine;

namespace Features.Audio
{
	public class AudioCue : MonoBehaviour
	{
		[Header("Sound definition")]
		[SerializeField] private AudioCueSO _audioCue = default;
		[SerializeField] private bool _playOnStart = false;

		[Header("Configuration")]
		[SerializeField] private AudioCueEventChannelSO _audioCueEventChannel = default;
		[SerializeField] private AudioConfigurationSO _audioConfiguration = default;

		private AudioCueKey controlKey = AudioCueKey.Invalid;

		private void Start()
		{
			if (this._playOnStart)
				StartCoroutine(PlayDelayed());
		}

		private void OnDisable()
		{
			this._playOnStart = false;
			StopAudioCue();
		}

		private IEnumerator PlayDelayed()
		{
			//The wait allows the AudioManager to be ready for play requests
			yield return new WaitForSeconds(1f);

			//This additional check prevents the AudioCue from playing if the object is disabled or the scene unloaded
			//This prevents playing a looping AudioCue which then would be never stopped
			if (this._playOnStart)
				PlayAudioCue();
		}

		public void PlayAudioCue()
		{
			this.controlKey = this._audioCueEventChannel.RaisePlayEvent(this._audioCue, this._audioConfiguration, this.transform.position);
		}

		public void StopAudioCue()
		{
			if (this.controlKey != AudioCueKey.Invalid)
			{
				if (!this._audioCueEventChannel.RaiseStopEvent(this.controlKey))
				{
					this.controlKey = AudioCueKey.Invalid;
				}
			}
		}

		public void FinishAudioCue()
		{
			if (this.controlKey != AudioCueKey.Invalid)
			{
				if (!this._audioCueEventChannel.RaiseFinishEvent(this.controlKey))
				{
					this.controlKey = AudioCueKey.Invalid;
				}
			}
		}
	}
}
