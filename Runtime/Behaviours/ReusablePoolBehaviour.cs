using UnityEngine;

namespace Common.Pooling
{
    public class ReusablePoolBehaviour<T> : MonoBehaviour, IPool<T>
        where T : MonoBehaviour, IReusable
    {
        public enum EStartup
        {
            Manual,
            Awake,
            Start
        }

        [Header("Initialization")]
        [SerializeField]
        protected EStartup _startup = EStartup.Start;
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

        public void Initialize()
        {
            if (_startup == EStartup.Manual)
                _pool.Initialize(_initialize);
        }

        protected virtual void Awake()
        {
            _pool = new ReusableBehaviourPool<T>(_capacity, _prefab);

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
