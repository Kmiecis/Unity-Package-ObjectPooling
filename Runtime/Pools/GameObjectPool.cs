using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// <see cref="UnityPool{T}"/> handling <see cref="GameObject"/> types
    /// </summary>
    [System.Serializable]
    public class GameObjectPool : UnityPool<GameObject>
    {
        public override GameObject Borrow()
        {
            var item = base.Borrow();
            item.SetActive(true);
            return item;
        }

        public override void Return(GameObject item)
        {
            item.SetActive(false);
            base.Return(item);
        }
    }
}
