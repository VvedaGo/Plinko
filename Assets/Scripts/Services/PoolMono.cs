using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public class PoolMono<T> : IPool<T> where T : MonoBehaviour
    {
        private const bool AutoExpand = true;
        private T _prefab;
        private readonly Transform _container;
        private List<T> _pool;

        public PoolMono(T prefab, int count, Transform container)
        {
            _prefab = prefab;
            _container = container;
            _pool = new List<T>();
            CreatePool(count);
        }

        private void CreatePool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(_prefab, _container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            _pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
            {
                if (!mono.gameObject.activeInHierarchy)
                {
                    element = mono;
                    mono.gameObject.SetActive(true);
                    return true;
                }
            }

            element = null;
            return false;
        }

        public T GetFreeElement()
        {
            if (HasFreeElement(out T element))
            {
                return element;
            }

            if (AutoExpand)
            {
                return CreateObject(true);
            }
        }
    }
}