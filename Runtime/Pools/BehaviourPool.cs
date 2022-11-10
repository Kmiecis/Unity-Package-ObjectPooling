using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="UnityPool{T}"/> handling <see cref="MonoBehaviour"/> types
    /// </summary>
    [System.Serializable]
    public class BehaviourPool<T> : UnityPool<T>
        where T : MonoBehaviour
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
