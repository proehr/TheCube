using DataStructures.Variables;
using UnityEngine;

namespace Features.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "newInventory", menuName = "Inventory")]
    public class Inventory_SO : ScriptableObject
    {
        [Tooltip("Amount of stored/available resources for worker production.")]
        [SerializeField] private IntVariable resource;

        public IntVariable Resource => resource;
    }
}
