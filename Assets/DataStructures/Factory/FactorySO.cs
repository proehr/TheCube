using UnityEngine;

namespace DataStructures.Factory
{
	public abstract class FactorySO<T> : ScriptableObject, IFactory<T>
	{
		public abstract T Create();
	}
}
