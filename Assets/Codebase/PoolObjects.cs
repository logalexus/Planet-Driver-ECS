using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Codebase
{
    public class PoolObjects<T> where T : MonoBehaviour
    {
        private List<T> _prefabs;
        private Transform _parent;
        private List<T> _pool;
        private System.Random _rnd;
        private int[] _randomNumbers;
        private int _lastFreeObject = 0;
        private bool _autoExpand;

        public PoolObjects(List<T> prefabs, int count)
        {
            _prefabs = prefabs;
            _parent = null;
            _rnd = new System.Random();
            _randomNumbers = GetRandomNumbers(count);
            CreatePool(count);
        }

        public PoolObjects(List<T> prefabs, int count, Transform parent, bool autoExpand = true)
        {
            _autoExpand = autoExpand;
            _prefabs = prefabs;
            _parent = parent;
            _rnd = new System.Random();
            _randomNumbers = GetRandomNumbers(count);
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            _pool = new List<T>();
            for (int i = 0; i < count; i++)
                CreateObject(i);

        }

        private int[] GetRandomNumbers(int count)
        {
            var numbers = Enumerable.Range(0, count).ToArray();
            var n = count;
            while (n > 1)
            {
                n--;
                var k = _rnd.Next(0, count);
                (numbers[k], numbers[n]) = (numbers[n], numbers[k]);
            }
            numbers = numbers.Select(x => x % _prefabs.Count).ToArray();
            return numbers;
        }

        private T CreateObject(int index, bool isActiveByDefault = false)
        {
            T createdObject = Object.Instantiate(_prefabs[_randomNumbers[index]], _parent);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        public bool HasFreeElement(out T element)
        {
            if (_lastFreeObject == _pool.Count - 1)
                _lastFreeObject = 0;

            for (int i = _lastFreeObject; i < _pool.Count; i++)
            {
                if (!_pool[i].gameObject.activeInHierarchy)
                {
                    element = _pool[i];
                    _pool[i].gameObject.SetActive(true);
                    _lastFreeObject = i;
                    return true;
                }
            }
            _lastFreeObject = 0;

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out T element))
                return element;

            if (_autoExpand)
                return CreateObject(_rnd.Next(0, _randomNumbers.Length - 1), true);

            throw new System.Exception("No free elements");
        }
    
        public void DeactivateAll()
        {
            foreach (var element in _pool)
                element.gameObject.SetActive(false);
        
        }
    }
}
