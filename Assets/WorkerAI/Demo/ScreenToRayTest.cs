using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace WorkerAI
{
    //Attach this script to your Camera
//This draws a line in the Scene view going through a point 200 pixels from the lower-left corner of the screen
//To see this, enter Play Mode and switch to the Scene tab. Zoom into your Camera's position.
    public class ScreenToRayTest : MonoBehaviour
    {
        Camera cam;
        Vector3 pos = new Vector3(200, 200, 0);


        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        }
    }
}
