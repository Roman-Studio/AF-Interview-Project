using UnityEngine;

namespace AFSInterview.Misc
{
    [RequireComponent(typeof(Canvas))]
    public class AFAssignCanvasCamera : MonoBehaviour
    {
        private Canvas _TargetCanvas;
        private Canvas TargetCanvas
        {
            get
            {
                if (_TargetCanvas == null)
                {
                    _TargetCanvas = GetComponent<Canvas>();
                }

                return _TargetCanvas;
            }
        }

        [SerializeField]
        private float _PlaneDistance = 1f;

        private void Awake()
        {
            TargetCanvas.worldCamera = Camera.main;
            TargetCanvas.planeDistance = _PlaneDistance;
        }
    }
}