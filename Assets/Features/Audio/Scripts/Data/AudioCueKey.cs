using System;

namespace Features.Audio
{
	public struct AudioCueKey
	{
		public static AudioCueKey Invalid = new AudioCueKey(-1, null);

		internal int Value;
		internal AudioCueSO AudioCue;

		internal AudioCueKey(int value, AudioCueSO audioCue)
		{
			this.Value = value;
			this.AudioCue = audioCue;
		}

		public override bool Equals(Object obj)
		{
			return obj is AudioCueKey x && this.Value == x.Value && this.AudioCue == x.AudioCue;
		}
		public override int GetHashCode()
		{
			return this.Value.GetHashCode() ^ this.AudioCue.GetHashCode();
		}
		public static bool operator ==(AudioCueKey x, AudioCueKey y)
		{
			return x.Value == y.Value && x.AudioCue == y.AudioCue;
		}
		public static bool operator !=(AudioCueKey x, AudioCueKey y)
		{
			return !(x == y);
		}
	}
}
