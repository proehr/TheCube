using DataStructures.Event;
using UnityEngine;

namespace DataStructures.Variables
{
    public class ResourceAmountVariable : IntVariable
    {
        [SerializeField] protected GameEvent onValueIncreased;
        [SerializeField] protected GameEvent onValueDecreased;

        public override void Add(int value)
        {
            base.Add(value);
            if (value > 0)
            {
                if (onValueIncreased != null) onValueIncreased.Raise();
            }
            else if (value < 0)
            {
                if (onValueDecreased != null) onValueDecreased.Raise();
            }
        }

        public override void Add(IntVariable value)
        {
            if (value.Get() > 0)
            {
                if (onValueIncreased != null) onValueIncreased.Raise();
            }
            else if (value.Get() < 0)
            {
                if (onValueDecreased != null) onValueDecreased.Raise();
            }
            base.Add(value);
        }
    }
}
