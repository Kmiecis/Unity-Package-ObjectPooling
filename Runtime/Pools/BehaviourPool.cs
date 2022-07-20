﻿using UnityEngine;

namespace Common.Pooling
{
    public class BehaviourPool<T> : APool<T>
        where T : MonoBehaviour
    {
        protected readonly T _prefab;

        public BehaviourPool(int capacity, T prefab) :
            base(capacity)
        {
            _prefab = prefab;
        }

        public override T Construct()
        {
            return Object.Instantiate(_prefab);
        }

        public override void Destroy(T item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}
