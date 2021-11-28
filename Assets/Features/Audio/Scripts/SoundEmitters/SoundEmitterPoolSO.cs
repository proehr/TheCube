using DataStructures.Factory;
using DataStructures.Pool;
using UnityEngine;

namespace Features.Audio
{
	[CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "Pool/SoundEmitter Pool")]
	public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
	{
		[SerializeField] private SoundEmitterFactorySO factory;

		public override IFactory<SoundEmitter> Factory
		{
			get
			{
				return this.factory;
			}
			set
			{
				this.factory = value as SoundEmitterFactorySO;
			}
		}
	}
}
