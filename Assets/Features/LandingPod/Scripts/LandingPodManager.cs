using DataStructures.Variables;
using Features.ExtendedRandom;
using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPodManager : MonoBehaviour
    {
        [SerializeField] private LandingPod landingPodPrefab;

        [SerializeField] private IntVariable relicAmount;

        private LandingPod landingPod;

        public void PlaceLandingPod(GameObject[] landingSurface)
        {
            var surfaceCubes = landingSurface;
            var pickedSurfaceCube = RandomSet<GameObject>.PickOne(surfaceCubes);
            this.landingPod = Instantiate(landingPodPrefab, this.transform);
            this.landingPod.Init(pickedSurfaceCube);
        }

        public void Launch(LaunchInformation launchInformation)
        {
            // Safety check
            if (relicAmount.Get() <= 0) return;

            Debug.Log("We would launch now :)");

            Destroy(this.landingPod.gameObject);
            // TODO Do the launch!
        }
    }
}
