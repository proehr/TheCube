using System.Collections.Generic;
using UnityEngine;

namespace Features.Perks.Logic
{
    [CreateAssetMenu(fileName = "PerkPool", menuName = "Feature/Perks")]
    public class PerkPool : ScriptableObject
    {
        [Header("For Debug")]
        [SerializeField] private List<PerkData> perks;

        public List<PerkData> Perks => perks;
    }
}