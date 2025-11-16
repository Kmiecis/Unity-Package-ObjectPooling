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

        protected override void Destroy(T item)
        {
            if (Application.isPlaying)
            {
                Object.Destroy(item.gameObject);
            }
            else
            {
                Object.DestroyImmediate(item.gameObject);
            }
        }
    }
}
