using DataStructures.Variables;
using Features.ExtendedRandom;
using Features.PlanetGeneration.Scripts;
using Features.PlanetGeneration.Scripts.Events;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPodManager : MonoBehaviour
    {
        [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;
        [SerializeField] private LandingPod landingPodPrefab;

        [SerializeField] private LaunchTriggeredActionEvent onLaunchTriggered;
        [SerializeField] private IntVariable relicAmount;

        private LandingPod landingPod;

        private void Awake()
        {
            onPlanetGenerated.RegisterListener(PlaceLandingPod);
            onLaunchTriggered.RegisterListener(Launch);
        }

        private void PlaceLandingPod(PlanetGenerator planetGenerator)
        {
            var surfaceCubes = planetGenerator.GetSurface(Surface.POSITIVE_Y);
            var pickedSurfaceCube = RandomSet<GameObject>.PickOne(surfaceCubes);
            this.landingPod = Instantiate(landingPodPrefab, this.transform);
            this.landingPod.Init(pickedSurfaceCube);
        }

        private void Launch(LaunchInformation launchInformation)
        {
            // Safety check
            if (relicAmount.Get() <= 0) return;

            Debug.Log("We would launch now :)");
            // TODO Do the launch!
        }
    }
}
