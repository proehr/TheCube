using System.Collections.Generic;

namespace Features.Audio
{
	public class SoundEmitterVault
	{
		private int nextUniqueKey = 0;
		private Dictionary<AudioCueKey, SoundEmitter[]> soundEmitters;
		public int Count => soundEmitters?.Count ?? 0;
		
		public SoundEmitterVault()
		{
			this.soundEmitters = new Dictionary<AudioCueKey, SoundEmitter[]>();
		}

		public AudioCueKey GetKey(AudioCueSO cue)
		{
			return new AudioCueKey(this.nextUniqueKey++, cue);
		}

		public void Add(AudioCueKey key, SoundEmitter[] emitters)
		{
			this.soundEmitters.Add(key, emitters);
		}

		public AudioCueKey Add(AudioCueSO cue, SoundEmitter[] emitters)
		{
			AudioCueKey emitterKey = GetKey(cue);
			this.soundEmitters.Add(emitterKey, emitters);
			return emitterKey;
		}

		public bool Get(AudioCueKey key, out SoundEmitter[] emitters)
		{
			return this.soundEmitters.TryGetValue(key, out emitters);
		}

		public bool Remove(AudioCueKey key)
		{
			return this.soundEmitters.Remove(key);
		}

		public void RemoveEntryIfAllEmittersFinishedPlaying(AudioCueKey key)
		{
			if (key == AudioCueKey.Invalid) return;
			if (!this.soundEmitters.TryGetValue(key, out var emitters)) return;
			bool allEmittersFinishedPlaying = true;
			for (var index = 0; index < emitters.Length; index++)
			{
				var emitter = emitters[index];
				if (emitter != null && emitter.IsPlaying())
				{
					allEmittersFinishedPlaying = false;
				}
			}
			if (allEmittersFinishedPlaying)
			{
				this.soundEmitters.Remove(key);
			}
		}
	}
}
