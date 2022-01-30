using DataStructures.Variables;
using UnityEngine;
namespace Features.Inventory.Scripts
{
    [CreateAssetMenu(fileName = "newInventory", menuName = "Inventory")]
    public class Inventory_SO : ScriptableObject
    {
        [Tooltip("Amount of stored/available resources for worker production.")]
        [SerializeField] private IntVariable resource;

        [Tooltip("Amount of collected relics.")]
        [SerializeField] private IntVariable relics;

        public IntVariable Resource => resource;
        public IntVariable Relics => relics;

        public void Reset()
        {
            resource.Restore();
            relics.Restore();
        }
    }
}
