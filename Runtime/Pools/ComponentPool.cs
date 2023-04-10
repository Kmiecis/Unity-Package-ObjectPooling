using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="UnityPool{T}"/> handling <see cref="Component"/> types
    /// </summary>
    [System.Serializable]
    public class ComponentPool<T> : UnityPool<T>
        where T : Component
    {
        protected override void Destroy(T item)
        {
            Object.Destroy(item.gameObject);
        }

        public override T Borrow()
        {
            var item = base.Borrow();
            item.gameObject.SetActive(true);
            return item;
        }

        public override void Return(T item)
        {
            item.gameObject.SetActive(false);
            base.Return(item);
        }
    }
}
