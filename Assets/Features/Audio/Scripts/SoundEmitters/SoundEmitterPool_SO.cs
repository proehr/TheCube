using DataStructures.Factory;
using DataStructures.Pool;
using UnityEngine;

namespace Features.Audio
{
	[CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "Pool/SoundEmitter Pool")]
	public class SoundEmitterPool_SO : ComponentPool_SO<SoundEmitter>
	{
		[SerializeField] private SoundEmitterFactory_SO factory;

		public override IFactory<SoundEmitter> Factory
		{
			get
			{
				return this.factory;
			}
			set
			{
				this.factory = value as SoundEmitterFactory_SO;
			}
		}
	}
}
