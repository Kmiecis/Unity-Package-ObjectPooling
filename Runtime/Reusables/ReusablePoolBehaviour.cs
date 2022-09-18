using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> <see cref="ReusableBehaviourPool{T}"/>
    /// </summary>
    public class ReusablePoolBehaviour<T> : MonoBehaviour, IPool<T>
        where T : MonoBehaviour, IReusable
    {
        [Header("Initialization")]
        [SerializeField]
        protected int _initialize = 1;

        [Header("Pooling")]
        [SerializeField]
        protected int _capacity = 1;
        [SerializeReference]
        protected T _prefab;

        private ReusableBehaviourPool<T> _pool;

        public virtual T Borrow()
        {
            return _pool.Borrow();
        }

        public virtual void Return(T item)
        {
            _pool.Return(item);
        }

        public virtual void Dispose()
        {
            _pool.Dispose();
        }

        protected virtual void Awake()
        {
            _pool = new ReusableBehaviourPool<T>(_capacity, _prefab);
        }

        protected virtual void Start()
        {
            _pool.Initialize(_initialize);
        }

        protected virtual void OnDestroy()
        {
            Dispose();
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_pool != null && _pool.Capacity != _capacity)
            {
                _pool.Capacity = _capacity;
            }
        }
#endif
    }

    /// <summary>
    /// <see cref="ReusablePoolBehaviour{T}"/> of <see cref="ReusableBehaviour"/>
    /// </summary>
    public class ReusablePoolBehaviour : ReusablePoolBehaviour<ReusableBehaviour>
    {
        public override ReusableBehaviour Borrow()
        {
            var item = base.Borrow();
            item.transform.SetParent(this.transform);
            return item;
        }
    }
}
