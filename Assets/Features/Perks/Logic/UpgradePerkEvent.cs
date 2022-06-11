using DataStructures.Event;
using UnityEngine;

namespace Features.Perks.Logic
{
    [CreateAssetMenu(fileName = "UpgradePerkEvent", menuName = "Feature/Perks")]
    public class UpgradePerkEvent : ActionEventWithParameter<PerkData>
    {
        
    }
}