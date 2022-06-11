using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Variables;
using Features.Commands.Scripts;
using Features.WorkerAI.Scripts;
using UnityEngine;
using UnityEngine.Localization;

namespace Features.Perks.Logic
{
    [System.Serializable]
    public struct PerkData
    {
        [SerializeField] private string name;
        [SerializeField, Multiline(3)] private string description;
        [SerializeField] private int level;
        [SerializeField] private int factor;
        [SerializeField] private float maxValue;
        [SerializeField] private float currentValue;
        [SerializeField] private float startValue;

        public string Name => name;
        public string Description => description;
        public int Level
        {
            get => level;
            set => level = value;
        }

        public int Factor => factor;
        public float MAXValue => maxValue;
        public float CurrentValue
        {
            get => currentValue;
            set => currentValue = value;
        }

        public float StartValue => startValue;
    }
}

namespace Features.Perks.Logic
{
    public class PerkManager : MonoBehaviour
    {
        [Header("Different Perks")]
        [SerializeField] private Command_SO resourceExcavationCommandData;
        [SerializeField] private PerkData defaultResourceExcavationRequiredWorker;
        [SerializeField] private PerkData defaultResourceExcavationDuration;

        [SerializeField] private Command_SO foundationExcavationCommandData;
        [SerializeField] private PerkData foundationResourceExcavationRequiredWorker;
        [SerializeField] private PerkData foundationResourceExcavationDuration;
        
        [SerializeField] private Command_SO relicExcavationCommandData;
        [SerializeField] private PerkData relicResourceExcavationRequiredWorker;
        [SerializeField] private PerkData relicResourceExcavationDuration;
        
        [SerializeField] private IntVariable workerT1Costs;
        [SerializeField] private PerkData workerCost;
        
        [SerializeField] private FloatVariable workerSpawnCooldown;
        [SerializeField] private PerkData workerSpawnRate;
        
        [SerializeField] private FloatVariable workerT1WalkSpeedMultiplier;
        [SerializeField] private PerkData walkSpeedMultiplier;
        
        [SerializeField] private IntVariable resourceAmount;
        [SerializeField] private PerkData startResourceAmount;
        
        [Header("Data")]
        [SerializeField] private PerkPool perkPool;
        
        private void Awake()
        {
            resourceExcavationCommandData.RequiredWorkers = (int)defaultResourceExcavationRequiredWorker.StartValue;
            defaultResourceExcavationRequiredWorker.CurrentValue = defaultResourceExcavationRequiredWorker.StartValue;
            resourceExcavationCommandData.Duration = defaultResourceExcavationDuration.StartValue;
            defaultResourceExcavationDuration.CurrentValue = defaultResourceExcavationDuration.StartValue;

            foundationExcavationCommandData.RequiredWorkers = (int)foundationResourceExcavationRequiredWorker.StartValue;
            foundationResourceExcavationRequiredWorker.CurrentValue = foundationResourceExcavationRequiredWorker.StartValue;
            foundationExcavationCommandData.Duration = foundationResourceExcavationDuration.StartValue;
            foundationResourceExcavationDuration.CurrentValue = foundationResourceExcavationDuration.StartValue;

            relicExcavationCommandData.RequiredWorkers = (int)relicResourceExcavationRequiredWorker.StartValue;
            relicResourceExcavationRequiredWorker.CurrentValue = relicResourceExcavationRequiredWorker.StartValue;
            relicExcavationCommandData.Duration = relicResourceExcavationDuration.StartValue;
            relicResourceExcavationDuration.CurrentValue = relicResourceExcavationDuration.StartValue;

            workerT1Costs.Set((int)workerCost.StartValue);
            workerCost.CurrentValue = workerCost.StartValue;
            
            workerSpawnCooldown.Set((int)workerSpawnRate.StartValue);
            workerSpawnRate.CurrentValue = workerSpawnRate.StartValue;

            workerT1WalkSpeedMultiplier.Set((int)walkSpeedMultiplier.StartValue);
            walkSpeedMultiplier.CurrentValue = walkSpeedMultiplier.StartValue;
            
            resourceAmount.Set((int)startResourceAmount.StartValue);
            startResourceAmount.CurrentValue = startResourceAmount.StartValue;
            
            // Assign Perks to Pool
            perkPool.Perks.Add(defaultResourceExcavationRequiredWorker);
            perkPool.Perks.Add(defaultResourceExcavationDuration);
            perkPool.Perks.Add(foundationResourceExcavationRequiredWorker);
            perkPool.Perks.Add(foundationResourceExcavationDuration);
            perkPool.Perks.Add(relicResourceExcavationRequiredWorker);
            perkPool.Perks.Add(relicResourceExcavationDuration);
            perkPool.Perks.Add(startResourceAmount);
        }

        public List<PerkData> ReceivePerksFromPool(int amount)
        {
            return perkPool.Perks.OrderBy(arg => Guid.NewGuid()).Take(amount).ToList();
        }

        public void ApplyPerkUpgrade(PerkData perkData)
        {
            foreach (PerkData perk in perkPool.Perks)
            {
                if (perkData.Equals(perk))
                {
                    perkData.Level += 1;
                    perkData.CurrentValue += perk.Factor;
                    AssignCurrentValueToSO(perkData);
                    
                    // Check for Perk removal from Pool
                    if (Math.Abs(perkData.CurrentValue - perkData.MAXValue) < float.Epsilon)
                    {
                        perkPool.Perks.Remove(perkData);
                    }
                    
                    return;
                }
            }
        }

        private void AssignCurrentValueToSO(PerkData perkData)
        {
            if (perkData.Equals(defaultResourceExcavationRequiredWorker))
            {
                resourceExcavationCommandData.RequiredWorkers = (int)defaultResourceExcavationRequiredWorker.CurrentValue;
            }

            if (perkData.Equals(defaultResourceExcavationDuration))
            {
                resourceExcavationCommandData.Duration = defaultResourceExcavationDuration.CurrentValue;
            }

            if (perkData.Equals(foundationResourceExcavationRequiredWorker))
            {
                foundationExcavationCommandData.RequiredWorkers = (int)foundationResourceExcavationRequiredWorker.CurrentValue;
            }

            if (perkData.Equals(foundationResourceExcavationDuration))
            {
                foundationExcavationCommandData.Duration = foundationResourceExcavationDuration.CurrentValue;
            }

            if (perkData.Equals(relicResourceExcavationRequiredWorker))
            {
                relicExcavationCommandData.RequiredWorkers = (int)relicResourceExcavationRequiredWorker.CurrentValue;
            }

            if (perkData.Equals(relicResourceExcavationDuration))
            {
                relicExcavationCommandData.Duration = relicResourceExcavationDuration.CurrentValue;
            }

            if (perkData.Equals(workerCost))
            {
                workerT1Costs.Set((int)workerCost.CurrentValue);
            }

            if (perkData.Equals(workerSpawnRate))
            {
                workerSpawnCooldown.Set((int)workerSpawnRate.CurrentValue);
            }

            if (perkData.Equals(walkSpeedMultiplier))
            {
                workerT1WalkSpeedMultiplier.Set((int)walkSpeedMultiplier.CurrentValue);
            }

            if (perkData.Equals(startResourceAmount))
            {
                resourceAmount.Set((int)startResourceAmount.CurrentValue);
            }
        }
    }
}
