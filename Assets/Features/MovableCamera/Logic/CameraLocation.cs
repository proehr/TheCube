using UnityEngine;

namespace Features.MovableCamera.Logic
{
    public class CameraLocation
    {
        private readonly CameraController cameraController;

        private CubeCorner_SO activeCorner;
        private CubeFace_SO activeFace;

        public CubeCorner_SO ActiveCorner
        {
            get => activeCorner;
            set => activeCorner = value;
        }

        public CubeFace_SO ActiveFace
        {
            get => activeFace;
            set => activeFace = value;
        }

        public CameraLocation(CameraController cameraController, CubeCorner_SO startCorner, CubeFace_SO startFace)
        {
            this.cameraController = cameraController;
            activeCorner = startCorner;
            activeFace = startFace;
        }

        public void SetActiveFace(out Vector3 newOffset)
        {

            newOffset = default;
            
            for (int i = 0; i < activeCorner.AdjacentFaces.Length; i++)
            {
                if (!activeFace.Equals(activeCorner.AdjacentFaces[i])) continue;
                
                if (i + 1 > activeCorner.AdjacentFaces.Length - 1)
                {
                    activeFace = activeCorner.AdjacentFaces[0];
                    newOffset = activeCorner.AdjacentFaces[0].Offset;
                }
                else
                {
                    activeFace = activeCorner.AdjacentFaces[i + 1];
                    newOffset = activeCorner.AdjacentFaces[i + 1].Offset;
                }
                    
                cameraController.ActiveFace = activeFace;
                    
                return;
            }
        }

        public void SetActiveCornerOnRightRotation()
        {
            for (int i = 0; i < activeFace.AdjacentCorners.Length; i++)
            {
                if (!activeCorner.Equals(activeFace.AdjacentCorners[i])) continue;
                
                if (i + 1 > activeFace.AdjacentCorners.Length - 1)
                {
                    activeCorner = activeFace.AdjacentCorners[0];
                }
                else
                {
                    activeCorner = activeFace.AdjacentCorners[i + 1];
                }

                cameraController.ActiveCorner = activeCorner;
                    
                return;
            }
        }

        public void SetActiveCornerOnLeftRotation()
        {
            for (int i = activeFace.AdjacentCorners.Length - 1; i >= 0; i--)
            {
                if (!activeCorner.Equals(activeFace.AdjacentCorners[i])) continue;
                
                if (i - 1 < 0)
                {
                    activeCorner = activeFace.AdjacentCorners[activeFace.AdjacentCorners.Length - 1];
                }
                else
                {
                    activeCorner = activeFace.AdjacentCorners[i - 1];
                }

                cameraController.ActiveCorner = activeCorner;
                    
                return;
            }
        }
    }
}