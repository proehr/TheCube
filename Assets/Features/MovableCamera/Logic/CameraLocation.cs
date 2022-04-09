using UnityEngine;

namespace Features.MovableCamera.Logic
{
    public class CameraLocation
    {
        private readonly CameraController cameraController;

        private CubeCorner_SO activeCorner;
        private CubeFace_SO activeFace;

        public CameraLocation(CameraController cameraController, CubeCorner_SO startCorner, CubeFace_SO startFace)
        {
            this.cameraController = cameraController;
            activeCorner = startCorner;
            activeFace = startFace;
        }

        public void SetActiveFace(out Vector3 oldOffset, out Vector3 newOffset)
        {

            oldOffset = default;
            newOffset = default;
            
            for (int i = 0; i < activeCorner.AdjacentFaces.Length; i++)
            {
                if (activeFace.Equals(activeCorner.AdjacentFaces[i]))
                {
                    if (i + 1 > activeCorner.AdjacentFaces.Length - 1)
                    {
                        oldOffset = activeFace.Offset;
                        activeFace = activeCorner.AdjacentFaces[0];
                        newOffset = activeCorner.AdjacentFaces[0].Offset;
                    }
                    else
                    {
                        oldOffset = activeFace.Offset;
                        activeFace = activeCorner.AdjacentFaces[i + 1];
                        newOffset = activeCorner.AdjacentFaces[i + 1].Offset;
                    }
                    
                    cameraController.ActiveFace = activeFace;
                    
                    break;
                }
            }
        }

        public void SetActiveCorner(int dir)
        {
            if (dir > 0)
            {
                for (int i = 0; i < activeFace.AdjacentCorners.Length; i++)
                {
                    if (activeCorner.Equals(activeFace.AdjacentCorners[i]))
                    {
                        if (i + 1 > activeFace.AdjacentCorners.Length - 1)
                        {
                            activeCorner = activeFace.AdjacentCorners[0];
                        }
                        else
                        {
                            activeCorner = activeFace.AdjacentCorners[i + 1];
                        }

                        cameraController.ActiveCorner = activeCorner;
                        
                        break;
                    }
                }
            }
            else
            {
                for (int i = activeFace.AdjacentCorners.Length - 1; i >= 0; i--)
                {
                    if (activeCorner.Equals(activeFace.AdjacentCorners[i]))
                    {
                        if (i - 1 < 0)
                        {
                            activeCorner = activeFace.AdjacentCorners[activeFace.AdjacentCorners.Length - 1];
                        }
                        else
                        {
                            activeCorner = activeFace.AdjacentCorners[i - 1];
                        }

                        cameraController.ActiveCorner = activeCorner;
                        
                        break;
                    }
                }
            }
        }
    }
}