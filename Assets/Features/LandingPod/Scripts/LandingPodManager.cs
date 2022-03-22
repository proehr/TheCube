using System.Collections;
using DataStructures.Event;
using DataStructures.Variables;
using Features.ExtendedRandom;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.LandingPod.Scripts
{
    public class LandingPodManager : MonoBehaviour
    {
        [SerializeField] private LandingPod landingPodPrefab;

        [SerializeField] private IntVariable relicAmount;
        
        [Header("Launching parameters")]
        [SerializeField, Range(0, 10)] private float shakeTime;
        [SerializeField, Range(0, 20)] private float shakeSpeed;
        [SerializeField, Range(0, 20)] private float shakeDistance;

        [SerializeField] private GameEvent onResetCamera;
        
        [Header("Launching Events")]
        [FormerlySerializedAs("onLaunchSequenceStarted")][SerializeField] private GameEvent onBeforeLaunch;
        [FormerlySerializedAs("onLaunchSequenceCompleted")] [SerializeField] private GameEvent onAfterLaunch;
        [FormerlySerializedAs("onLandSequenceStarted")] [SerializeField] private GameEvent onBeforeLanding;
        [FormerlySerializedAs("onLandSequenceCompleted")] [SerializeField] private GameEvent onAfterLanding;

        private LandingPod landingPod;

        public void PlaceLandingPod(GameObject[] landingSurface)
        {
            var surfaceCubes = landingSurface;
            var pickedSurfaceCube = RandomSet<GameObject>.PickOne(surfaceCubes);
            
            if (landingPod == null)
            {
                this.landingPod = Instantiate(landingPodPrefab, this.transform);
                this.landingPod.Init(pickedSurfaceCube);
            }
            else
            {
                Land(this.landingPod.GetLandingPosition(pickedSurfaceCube));
            }
        }

        private void Land(Vector3 landingPosition)
        {
            // Land mechanic
            StartCoroutine(LandingSequence(landingPosition));
        }

        public void Launch(LaunchInformation launchInformation)
        {
            // Safety check
            if (relicAmount.Get() <= 0) return;

            // Launch mechanic
            StartCoroutine(LaunchingSequence());
        }

        private IEnumerator LaunchingSequence()
        {
            // TODO trigger event for turning off Commands (State)
            // TODO trigger event for turning off CameraRig (State)
            // TODO stop Worker Spawning
            
            onBeforeLaunch.Raise();
            
            // Hardcoded 2 sec wait for MovableCamera reset
            onResetCamera.Raise();
            yield return new WaitForSeconds(2);
            
            // TODO if we use more than two virtual Cams we will need a StateDrivenCam later on
            landingPod.LandingPodCam.Priority = 11;
            // Set parent of LandingPodCamera so that it will not move with the landingPod
            landingPod.LandingPodCam.transform.SetParent(this.transform);
            
            // TODO trigger LaunchSound
            
            // Cache landingPod Position
            Vector3 landingPodPosition = landingPod.transform.localPosition;
            Vector3 landingPodStartPosition = landingPod.transform.localPosition;
            
            // // shake animation
            // TODO adjust shake parameters
            // float time = 0;
            // while (time <= shakeTime)
            // {
            //     landingPodPosition.x = Mathf.Lerp(Mathf.Sin(time * shakeSpeed) * shakeDistance, 0, time / shakeTime);
            //     landingPod.transform.localPosition += new Vector3(landingPodPosition.x,0f,0f);
            //     time += Time.deltaTime;
            //     yield return null;
            // }
            //
            // landingPod.transform.localPosition += new Vector3(landingPodStartPosition.x , 0f, 0f);
            
            // Hardcoded wait for LaunchingSound and shake animation
            yield return new WaitForSeconds(5);

            // Move LandingPod up with LeanTween, Hardcoded easeInOutQuint
            LeanTween.moveLocal(landingPod.gameObject, new Vector3(0, landingPodPosition.y + 400, 0), 10)
                .setEase(LeanTweenType.easeInOutQuint);
            
            // Hardcoded wait before launchCompleted event
            yield return new WaitForSeconds(10);
            onAfterLaunch.Raise();
        }

        private IEnumerator LandingSequence(Vector3 landingPosition)
        {
            // TODO REMOVE this after gameController events are implemented
            // (this is only for testing) Waiting for launching is completed
            yield return new WaitForSeconds(23);

            onBeforeLanding.Raise();
            
            // TODO trigger LaunchSound
            
            // Move LandingPod down to given position with LeanTween, Hardcoded easeInOutQuint
            LeanTween.moveLocal(landingPod.gameObject, landingPosition, 5)
                .setEase(LeanTweenType.easeInOutQuint);
            
            yield return new WaitForSeconds(5);
            
            // TODO trigger event for turning on CameraRig (State)
            // TODO trigger event for turning on Commands (State)
            // TODO start Worker Spawning
            
            landingPod.LandingPodCam.transform.SetParent(landingPod.transform);
            landingPod.LandingPodCam.Priority = 8;
            
            // Hardcoded wait before landCompleted event
            yield return new WaitForSeconds(10);
            onAfterLanding.Raise();
            
        }
    }
}
