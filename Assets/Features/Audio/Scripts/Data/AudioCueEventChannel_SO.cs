using UnityEngine;

namespace Features.Audio
{
	/// <summary>
	/// Event on which <c>AudioCue</c> components send a message to play SFX and music. <c>AudioManager</c> listens on these events, and actually plays the sound.
	/// </summary>
	[CreateAssetMenu(menuName = "Audio/Audio Cue Event Channel")]
	public class AudioCueEventChannel_SO : ScriptableObject
	{
		public AudioCuePlayAction OnAudioCuePlayRequested;
		public AudioCueStopAction OnAudioCueStopRequested;
		public AudioCueFinishAction OnAudioCueFinishRequested;

		public AudioCueKey RaisePlayEvent(AudioCue_SO audioCue, AudioConfiguration_SO audioConfiguration, Vector3 positionInSpace = default)
		{
			AudioCueKey audioCueKey = AudioCueKey.Invalid;

			if (this.OnAudioCuePlayRequested != null)
			{
				audioCueKey = this.OnAudioCuePlayRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
			}
			else
			{
				Debug.LogWarning("An AudioCue play event was requested  for " + audioCue.name +", but nobody picked it up. " +
				                 "Check why there is no AudioManager already loaded, " +
				                 "and make sure it's listening on this AudioCue Event channel.");
			}

			return audioCueKey;
		}

		public bool RaiseStopEvent(AudioCueKey audioCueKey)
		{
			bool requestSucceed = false;

			if (this.OnAudioCueStopRequested != null)
			{
				requestSucceed = this.OnAudioCueStopRequested.Invoke(audioCueKey);
			}
			else
			{
				Debug.LogWarning("An AudioCue stop event was requested, but nobody picked it up. " +
				                 "Check why there is no AudioManager already loaded, " +
				                 "and make sure it's listening on this AudioCue Event channel.");
			}

			return requestSucceed;
		}

		public bool RaiseFinishEvent(AudioCueKey audioCueKey)
		{
			bool requestSucceed = false;

			if (this.OnAudioCueStopRequested != null)
			{
				requestSucceed = this.OnAudioCueFinishRequested.Invoke(audioCueKey);
			}
			else
			{
				Debug.LogWarning("An AudioCue finish event was requested, but nobody picked it up. " +
				                 "Check why there is no AudioManager already loaded, " +
				                 "and make sure it's listening on this AudioCue Event channel.");
			}

			return requestSucceed;
		}
	}

	public delegate AudioCueKey AudioCuePlayAction(AudioCue_SO audioCue, AudioConfiguration_SO audioConfiguration, Vector3 positionInSpace);
	public delegate bool AudioCueStopAction(AudioCueKey emitterKey);
	public delegate bool AudioCueFinishAction(AudioCueKey emitterKey);
}