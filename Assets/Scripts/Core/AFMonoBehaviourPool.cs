using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace AFSInterview.Core
{ 
    public abstract class AFMonoBehaviourPool<TPoolElement, TPoolObservedType> : AFObserverMonoBehaviour
        where TPoolElement : AFObserverMonoBehaviour<TPoolObservedType>
    {
        [Inject]
        protected AFGenericGameObjectFactory _ElementsFactory;
        
        [SerializeField]
        protected TPoolElement _PoolElementPrefab;

        [SerializeField]
        protected Transform _TargetTransform;

        protected ObjectPool<TPoolElement> _ObjectPool;
        protected ObjectPool<TPoolElement> ObjectPool => _ObjectPool ??= new ObjectPool<TPoolElement>(CreatePoolElementInstance, OnGetPoolElement, OnReleasePoolElement, OnDestroyPoolElement);

        protected List<PooledObject<TPoolElement>> _CurrentlyActiveObjects = new();
        protected bool _ScenePrefabPresentInPool;

        protected override void Start()
        {
            _PoolElementPrefab.gameObject.SetActive(false);
            base.Start();
        }

        protected virtual TPoolElement CreatePoolElementInstance()
        {
            if (string.IsNullOrEmpty(_PoolElementPrefab.gameObject.scene.name) || _ScenePrefabPresentInPool)
            {
                return _ElementsFactory.Create(_PoolElementPrefab, _TargetTransform);
            }
            
            _ScenePrefabPresentInPool = true;
            _PoolElementPrefab.gameObject.SetActive(false);
            return _PoolElementPrefab;
        }

        protected virtual void OnGetPoolElement(TPoolElement poolElement)
        {
            poolElement.gameObject.SetActive(true);
        }

        protected virtual void OnReleasePoolElement(TPoolElement poolElement)
        {
            poolElement.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyPoolElement(TPoolElement poolElement)
        {
            Destroy(poolElement.gameObject);
        }

        public void BindCollection(IEnumerable<TPoolObservedType> collectionToBind)
        {
            ClearActive();

            var pooledElements = new List<TPoolElement>();

            foreach (var _ in collectionToBind)
            {
                _CurrentlyActiveObjects.Add(ObjectPool.Get(out var pooledElement));
                pooledElements.Add(pooledElement);
            }
            
            pooledElements.Sort((a,b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

            var index = 0;
            
            foreach (var objectToBind in collectionToBind)
            {
                pooledElements[index].Set(objectToBind);
                index++;
            }
        }

        protected void ClearActive()
        {
            foreach (var currentlyActiveObject in _CurrentlyActiveObjects)
            {
                ((IDisposable)currentlyActiveObject).Dispose();
            }
            
            _CurrentlyActiveObjects.Clear();
        }
    }
}