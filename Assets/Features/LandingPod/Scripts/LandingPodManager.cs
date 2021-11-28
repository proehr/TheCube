using Features.ExtendedRandom;
using Features.Planet_Generation.Scripts;
using Features.Planet_Generation.Scripts.Events;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPodManager : MonoBehaviour
    {
        [SerializeField] private PlanetGeneratedActionEvent onPlanetGenerated;
        [SerializeField] private LandingPod landingPodPrefab;
        private LandingPod landingPod;

        private void Awake()
        {
            onPlanetGenerated.RegisterListener(PlaceLandingPod);
        }

        private void PlaceLandingPod(PlanetGenerator planetGenerator)
        {
            var surfaceCubes = planetGenerator.GetSurface(Surface.POSITIVE_Y);
            var pickedSurfaceCube = RandomSet<GameObject>.PickOne(surfaceCubes);
            this.landingPod = Instantiate(landingPodPrefab, this.transform);
            this.landingPod.Init(pickedSurfaceCube);
        }
    }
}
