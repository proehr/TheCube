using UnityEngine;

namespace Features.LandingPod.Scripts
{
    public class LandingPod : MonoBehaviour
    {
        public void Init(GameObject surfaceCube)
        {
            var surfaceCubeTransform = surfaceCube.transform;
            var thisTransform = this.transform;
            thisTransform.position = surfaceCubeTransform.position +
                                  surfaceCubeTransform.up * surfaceCubeTransform.localScale.y;
            thisTransform.rotation = surfaceCubeTransform.rotation;
        }
    }
}
