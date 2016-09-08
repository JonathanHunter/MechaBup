using UnityEngine;

namespace Assets.Scripts.Misc
{
    public class PlatformGhoster : MonoBehaviour
    {
        /// <summary> Controls whether this class tries to detect platforms to ghost through by itself. </summary>
        public bool raycastEnabled;

        /// <summary> The collider to toggle for ghosting. </summary>
        [SerializeField]
        private Collider2D col;
        /// <summary> The points to ray cast from. </summary>
        [SerializeField]
        private Transform[] platformDetectors;
        /// <summary> The layer to look for when doing the raycast. </summary>
        [SerializeField]
        private LayerMask layer;

        void Start()
        {
            raycastEnabled = true;
        }

        void Update()
        {
            RaycastHit temp;
            if (raycastEnabled)
            {
                foreach (Transform platformDetector in platformDetectors)
                {
                    if (Physics.Raycast(new Vector3(platformDetector.position.x, platformDetector.position.y + .3f, platformDetector.position.z), new Vector3(0, .2f, 0), out temp, .2f, layer))
                    {
                        Ghost = true;
                    }
                }
            }
        }

        /// <summary> Controls whether ghosting is happening. </summary>
        public bool Ghost
        {
            set
            {
                if (value)
                    col.isTrigger = true;
                else
                    col.isTrigger = false;
            }
        }

        /// <summary> Used to turn off ghosting when we come out the other side of a platform. </summary>
        void OnTriggerExit2D(Collider2D col)
        {
            if(col.gameObject.layer == layer)
                Ghost = false;
        }
    }
}
