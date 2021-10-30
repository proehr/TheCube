using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "Utils/Variables/Vector3Variable")]
    public class Vector3Variable : AbstractVariable<Vector3>
    {
        public void ApplyChange(Vector3 amount) => runtimeValue += amount;

        public void ApplyChange(Vector3Variable amount) => runtimeValue += amount.runtimeValue;
    }
}

