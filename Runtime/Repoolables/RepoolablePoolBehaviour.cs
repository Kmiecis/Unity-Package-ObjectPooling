using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> <see cref="RepoolablePool{T}"/>
    /// </summary>
    public class RepoolablePoolBehaviour<T> : MonoBehaviour, IPool<IRepoolable<T>>
        where T : MonoBehaviour, IReusable
    {
        [Header("Pooling")]
        [SerializeField]
        protected int _capacity = 1;
        [SerializeReference]
        protected T _prefab;

        [Header("Initialization")]
        [SerializeField]
        protected int _initialize = 1;

        private RepoolablePool<T> _pool;

        public virtual IRepoolable<T> Borrow()
        {
            return _pool.Borrow();
        }

        public virtual void Return(IRepoolable<T> item)
        {
            _pool.Return(item);
        }

        public virtual void Dispose()
        {
            _pool.Dispose();
        }

        protected virtual void Awake()
        {
            var subpool = new ReusableBehaviourPool<T>(_capacity, _prefab);
            _pool = new RepoolablePool<T>(_capacity, subpool);
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
    /// <see cref="RepoolablePoolBehaviour{T}"/> of <see cref="ReusableBehaviour"/>
    /// </summary>
    public class RepoolablePoolBehaviour : RepoolablePoolBehaviour<ReusableBehaviour>
    {
    }
}
