using DataStructures.Factory;
using UnityEngine;

namespace Features.Audio
{
	[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
	public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
	{
		public SoundEmitter prefab = default;

		public override SoundEmitter Create()
		{
			return Instantiate(this.prefab);
		}
	}
}
