using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace AFSInterview.Core
{
    public class AFGenericGameObjectFactory : PlaceholderFactory<MonoBehaviour>
    {
        private readonly DiContainer zenjectContainer;

        public AFGenericGameObjectFactory(DiContainer zenjectContainer) => this.zenjectContainer = zenjectContainer;

        public GameObject Create(GameObject prefab)
        {
            var instance = zenjectContainer.InstantiatePrefab(prefab);
            zenjectContainer.Inject(instance);
            return instance;
        }
        
        public GameObject Create(GameObject prefab, Transform parent)
        {
            var instance = zenjectContainer.InstantiatePrefab(prefab, parent);
            zenjectContainer.Inject(instance);
            return instance;
        }
        
        public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var instance = zenjectContainer.InstantiatePrefab(prefab, position, rotation, parent);
            zenjectContainer.Inject(instance);
            return instance;
        }
        
        public T Create<T>(T prefab) 
            where T : MonoBehaviour
        {
            var instance = zenjectContainer.InstantiatePrefab(prefab).GetComponent<T>();
            zenjectContainer.Inject(instance);
            return instance;
        }

        public T Create<T>(T prefab, Transform parent) 
            where T : MonoBehaviour
        {
            var instance = zenjectContainer.InstantiatePrefab(prefab, parent).GetComponent<T>();
            zenjectContainer.Inject(instance);
            return instance;
        }
        
        public T Create<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) 
            where T : MonoBehaviour
        {
            var instance = Object.Instantiate(prefab, position, rotation, parent);
            zenjectContainer.Inject(instance);
            return instance;
        }
    }
}