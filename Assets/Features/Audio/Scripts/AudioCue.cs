using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Audio
{
	public class AudioCue : MonoBehaviour
	{
		[Header("Sound definition")]
		[SerializeField] private AudioCue_SO audioCue = default;
		[SerializeField] private bool playOnStart = false;

		[Header("Configuration")]
		[SerializeField] private AudioCueEventChannel_SO audioCueEventChannel = default;
		[SerializeField] private AudioConfiguration_SO audioConfiguration = default;

		private AudioCueKey controlKey = AudioCueKey.Invalid;

		private void Start()
		{
			if (this.playOnStart)
				StartCoroutine(PlayDelayed());
		}

		private void OnDisable()
		{
			this.playOnStart = false;
			StopAudioCue();
		}

		private IEnumerator PlayDelayed()
		{
			//The wait allows the AudioManager to be ready for play requests
			yield return new WaitForSeconds(1f);

			//This additional check prevents the AudioCue from playing if the object is disabled or the scene unloaded
			//This prevents playing a looping AudioCue which then would be never stopped
			if (this.playOnStart)
				PlayAudioCue();
		}

		public void PlayAudioCue()
		{
			this.controlKey = this.audioCueEventChannel.RaisePlayEvent(this.audioCue, this.audioConfiguration, this.transform.position);
		}

		public void StopAudioCue()
		{
			if (this.controlKey != AudioCueKey.Invalid)
			{
				if (!this.audioCueEventChannel.RaiseStopEvent(this.controlKey))
				{
					this.controlKey = AudioCueKey.Invalid;
				}
			}
		}

		public void FinishAudioCue()
		{
			if (this.controlKey != AudioCueKey.Invalid)
			{
				if (!this.audioCueEventChannel.RaiseFinishEvent(this.controlKey))
				{
					this.controlKey = AudioCueKey.Invalid;
				}
			}
		}
	}
}
