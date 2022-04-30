using System;
using System.Collections.Generic;
using DataStructures.Variables;
using Features.Planet_Generation.Scripts;
using UnityEngine;

namespace Features.Planet.Progression.Scripts
{
    [CreateAssetMenu(menuName = "PlanetProgression", fileName = "PlanetProgression_SO", order = 0)]
    public class PlanetProgression_SO : ScriptableObject
    {
        [SerializeField] private List<Planet_SO> planetDataList;
        [SerializeField] private IntVariable currentPlanet;
        [SerializeField] private IntVariable relicAmount;

        [SerializeField] private IntVariable progressionMomentum;

        internal Planet_SO GetNextPlanetData()
        {
            // at the moment: if inventory has a relic, previous level was won
            // TODO: save data on level win/loss in a BoolVariable 
            if (relicAmount.Get() > 0)
            {
                progressionMomentum.Set(1);
            }
            else
            {
                progressionMomentum.Set(Math.Max(progressionMomentum.Get() - 1, -1));
            }

            currentPlanet.Set(Math.Max(0, Math.Min(planetDataList.Count - 1, currentPlanet.Get() + progressionMomentum.Get())));
            return planetDataList[currentPlanet.Get()];
        }
    }
}