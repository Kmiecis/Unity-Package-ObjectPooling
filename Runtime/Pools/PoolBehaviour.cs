using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> <see cref="BehaviourPool{T}"/>
    /// </summary>
    public class PoolBehaviour<T> : MonoBehaviour, IPool<T>
        where T : MonoBehaviour
    {
        [Header("Pooling")]
        [SerializeField]
        protected int _capacity = 1;
        [SerializeReference]
        protected T _prefab;

        [Header("Initialization")]
        [SerializeField]
        protected int _initialize = 1;

        private BehaviourPool<T> _pool;

        public virtual T Borrow()
        {
            return _pool.Borrow();
        }

        public virtual void Return(T item)
        {
            _pool.Return(item);
        }

        public virtual int Capacity
        {
            get => _capacity;
            set
            {
                _capacity = value;
                _pool.Capacity = value;
            }
        }

        public virtual void Dispose()
        {
            _pool.Dispose();
        }

        protected virtual void Awake()
        {
            _pool = new BehaviourPool<T>(_capacity, _prefab);
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
    /// <see cref="PoolBehaviour{T}"/> of <see cref="MonoBehaviour"/>
    /// </summary>
    public class PoolBehaviour : PoolBehaviour<ReusableBehaviour>
    {
    }
}
