using UnityEngine;
using UnityEngine.Events;

namespace AFSInterview.Misc
{
    public class AFOnStartEventComponent : MonoBehaviour
    {
        [field: SerializeField]
        public UnityEvent OnStartEvent { get; private set; }
        
        private void Start()
        {
            OnStartEvent?.Invoke();
        }
    }
}