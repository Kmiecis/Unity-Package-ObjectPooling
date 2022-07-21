using UnityEngine;

namespace Common.Pooling
{
    public class ReusableBehaviourPool<T> : AReusablePool<T>
        where T : MonoBehaviour, IReusable
    {
        protected readonly T _prefab;

        public ReusableBehaviourPool(int capacity, T prefab) :
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
