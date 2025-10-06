using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="UnityPool{T}"/> handling <see cref="Component"/> types
    /// </summary>
    [System.Serializable]
    public class ComponentMultiPool<T> : UnityMultiPool<T>
        where T : Component
    {
        public override U Borrow<U>()
        {
            var item = base.Borrow<U>();
            item.gameObject.SetActive(true);
            return item;
        }

        public override void Return<U>(U item)
        {
            item.gameObject.SetActive(false);
            base.Return(item);
        }

        protected override void Destroy<U>(U item)
        {
            Object.Destroy(item.gameObject);
        }
    }
}
