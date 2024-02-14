using UnityEngine;

namespace AFSInterview.Misc
{
    public class AFLookAtCamera : MonoBehaviour
    {
        private Camera mainCamera;
        private Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                }

                return mainCamera;
            }
        }
        
        private void Update()
        {
            transform.rotation = MainCamera.transform.rotation;
        }
    }
}