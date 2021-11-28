using System;
using UnityEngine;

namespace Features.Audio
{
	/// <summary>
	/// A collection of grouped audio clips that are played in parallel, each group supports sequential or random playback within the group.
	/// </summary>
	[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio/Audio Cue")]
	public class AudioCueSO : ScriptableObject
	{
		public bool looping = false;
		[SerializeField] private AudioClipGroup[] audioClipGroups = default;

		public AudioClipData[] GetClips()
		{
			int numberOfClips = this.audioClipGroups.Length;
			AudioClipData[] resultingClips = new AudioClipData[numberOfClips];

			for (int i = 0; i < numberOfClips; i++)
			{
				resultingClips[i] = this.audioClipGroups[i].GetNextClip();
			}

			return resultingClips;
		}
	}

	[Serializable]
	public class AudioClipData
	{
		public AudioClip audioClip;
		public RangedFloat volume = new RangedFloat(1, 1);
		[MinMaxRange(0, 2)]
		public RangedFloat pitch = new RangedFloat(1, 1);
		[MinMaxRange(-1, 1)]
		public RangedFloat pan = new RangedFloat(0, 0);
	}

	[Serializable]
	public class AudioClipGroup
	{
		public SequenceMode sequenceMode = SequenceMode.RandomNoImmediateRepeat;
		public AudioClipData[] audioClipData;

		[NonSerialized] private int _nextClipToPlay = -1;
		[NonSerialized] private int _lastClipPlayed = -1;

		/// <summary>
		/// Chooses the next clip in the sequence, either following the order or randomly.
		/// </summary>
		/// <returns>A reference to an AudioClip</returns>
		public AudioClipData GetNextClip()
		{
			// Fast out if there is only one clip to play
			if (this.audioClipData.Length == 1)
				return this.audioClipData[0];

			if (this._nextClipToPlay == -1)
			{
				// Index needs to be initialised: 0 if Sequential, random if otherwise
				this._nextClipToPlay = (this.sequenceMode == SequenceMode.Sequential) ? 0 : UnityEngine.Random.Range(0, this.audioClipData.Length);
			}
			else
			{
				// Select next clip index based on the appropriate SequenceMode
				switch (this.sequenceMode)
				{
					case SequenceMode.Random:
						this._nextClipToPlay = UnityEngine.Random.Range(0, this.audioClipData.Length);
						break;

					case SequenceMode.RandomNoImmediateRepeat:
						do
						{
							this._nextClipToPlay = UnityEngine.Random.Range(0, this.audioClipData.Length);
						} while (this._nextClipToPlay == this._lastClipPlayed);
						break;

					case SequenceMode.Sequential:
						this._nextClipToPlay = (int)Mathf.Repeat(++this._nextClipToPlay, this.audioClipData.Length);
						break;
				}
			}

			this._lastClipPlayed = this._nextClipToPlay;

			return this.audioClipData[this._nextClipToPlay];
		}

		public enum SequenceMode
		{
			Random,
			RandomNoImmediateRepeat,
			Sequential,
		}
	}
}