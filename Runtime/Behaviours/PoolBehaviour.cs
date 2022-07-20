using UnityEngine;

namespace Common.Pooling
{
    public class PoolBehaviour<T> : MonoBehaviour, IPool<T>
        where T : MonoBehaviour
    {
        public enum EStartup
        {
            Manual,
            Awake,
            Start
        }

        [Header("Pooling")]
        [SerializeField]
        protected int _capacity = 1;
        [SerializeReference]
        protected T _prefab;

        [Header("Initialization")]
        [SerializeField]
        protected EStartup _startup = EStartup.Start;
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

        public virtual void Dispose()
        {
            _pool.Dispose();
        }

        public void Initialize()
        {
            if (_startup == EStartup.Manual)
                _pool.Initialize(_initialize);
        }

        protected virtual void Awake()
        {
            _pool = new BehaviourPool<T>(_capacity, _prefab);

            if (_startup == EStartup.Awake)
                _pool.Initialize(_initialize);
        }

        protected virtual void Start()
        {
            if (_startup == EStartup.Start)
                _pool.Initialize(_initialize);
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }

    public class PoolBehaviour : PoolBehaviour<MonoBehaviour>
    {
    }
}
