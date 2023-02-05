using Scellecs.Morpeh.Providers;
using UnityEngine;
using Zenject;

namespace Codebase.Assets
{
    public class InstantiateService
    {
        private DiContainer _container;

        public InstantiateService(DiContainer container)
        {
            _container = container;
        }

        public T Instantiate<T>(T prefab, Transform parent, Vector3 position, Quaternion rotation)
            where T : MonoBehaviour
        {
            T intance = GameObject.Instantiate(prefab, position, rotation, parent);
            _container.Inject(intance);
            return intance;
        }

        public T Instantiate<T>(T prefab, Transform parent)
            where T : MonoBehaviour
        {
            T intance = GameObject.Instantiate(prefab, parent);
            _container.Inject(intance);
            return intance;
        }
    }
}