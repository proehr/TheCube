﻿using System.Collections;
using DataStructures.Variables;
using Features.ExtendedRandom;
using UnityEngine;

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

        [Header("Launching Events")]
        [SerializeField] private LaunchCompletedActionEvent onLaunchCompleted;

        private LandingPod landingPod;

        private LaunchInformation launchInformation;

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
            this.launchInformation = launchInformation;
            
            // Safety check
            if (relicAmount.Get() <= 0) return;
            
            // Launch mechanic
            StartCoroutine(LaunchingSequence());
        }

        private IEnumerator LaunchingSequence()
        {
            // Hardcoded 2 sec wait for MovableCamera reset
            // TODO needs to be fetched from Main Camera Brain (Default Blend)
            yield return new WaitForSeconds(2);
            
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
            onLaunchCompleted.Raise(launchInformation);
        }

        private IEnumerator LandingSequence(Vector3 landingPosition)
        {
            // Move LandingPod down to given position with LeanTween, Hardcoded easeInOutQuint
            LeanTween.moveLocal(landingPod.gameObject, landingPosition, 5)
                .setEase(LeanTweenType.easeInOutQuint);
            
            yield return new WaitForSeconds(5);
            // TODO trigger LandSound
            
            // Set parent of LandingPodCamera back, so that it will move with the landingPod again
            landingPod.LandingPodCam.transform.SetParent(landingPod.transform);
            landingPod.LandingPodCam.Priority = 8;
            
            // Hardcoded wait before landCompleted event
            yield return new WaitForSeconds(10);
        }
    }
}
