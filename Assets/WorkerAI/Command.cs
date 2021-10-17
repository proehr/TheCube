using UnityEngine;

namespace WorkerAI
{
    public class Command
    {
        public Vector3 location { get; private set; }
        private GameObject commandPost;

        public Command(Vector3 location, GameObject commandPostPrefab)
        {
            this.location = location;
            commandPost = Object.Instantiate(commandPostPrefab, location, Quaternion.identity);
        }

        public void End()
        {
            Object.Destroy(commandPost);
        }
    }
}
