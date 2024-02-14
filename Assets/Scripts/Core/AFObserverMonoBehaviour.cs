using System.Collections;
using UnityEngine;

namespace AFSInterview.Core
{
    /// <summary>
    /// Base class that can register to events to observe changes in desired values and react to it in the next frame.
    /// Part of observer pattern.
    /// </summary>
    public abstract class AFObserverMonoBehaviour : MonoBehaviour
    {
        protected bool RequestedUpdateInThisFrame;

        protected virtual void Start()
        {
            RegisterEvents();
            ReactToChanges();
        }

        protected virtual void OnDestroy()
        {
            UnregisterEvents();
        }
        
        protected virtual void OnDisable()
        {
            RequestedUpdateInThisFrame = false;
        }
        
        protected abstract void RegisterEvents();
        protected abstract void UnregisterEvents();
        
        protected void ReactToChanges()
        {
            if (RequestedUpdateInThisFrame)
            {
                return;
            }

            RequestedUpdateInThisFrame = true;

            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(ReactToChangesCoroutine());
            }
            else
            {
                OnReactToChanges();
                RequestedUpdateInThisFrame = false;
            }
        }

        private IEnumerator ReactToChangesCoroutine()
        {
            yield return null;
            OnReactToChanges();
            RequestedUpdateInThisFrame = false;
        }

        protected abstract void OnReactToChanges();
    }
}