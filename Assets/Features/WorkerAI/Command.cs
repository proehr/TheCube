using UnityEngine;

namespace Features.WorkerAI
{
    public class Command
    {
        public Vector3 location { get; private set; }
        private GameObject commandPost;

        public Command(Vector3 location, GameObject commandPostPrefab, Transform commandPostsParent, Vector3 planeNormal)
        {
            this.location = location;
            commandPost = Object.Instantiate(commandPostPrefab, location, Quaternion.identity, commandPostsParent);
            commandPost.transform.up = planeNormal;
        }

        public void End()
        {
            Object.Destroy(commandPost);
        }
    }
}
