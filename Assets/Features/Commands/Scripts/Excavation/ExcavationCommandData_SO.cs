using UnityEngine;

namespace Features.Commands.Scripts.Excavation
{
    [CreateAssetMenu(fileName = "NewExcavationCommandData", menuName = "Command/Excavation")]
    public class ExcavationCommandData_SO : Command_SO
    {
        // [Tooltip("How many seconds between each resource gain?")]
        // [SerializeField] private float tickDuration = 3;

        [Tooltip("How many resources are maximally excavated per tick?"), Min(1)]
        [SerializeField] private int excavationAmountPerTick = 1;

        public int ExcavationAmountPerTick => excavationAmountPerTick;
    }
}
