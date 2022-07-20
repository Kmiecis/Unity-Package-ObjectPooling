using UnityEngine;

namespace Common.Pooling
{
    public class RepoolablePoolBehaviour<T> : MonoBehaviour, IPool<IRepoolable<T>>
        where T : MonoBehaviour, IReusable
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

        private ReusableBehaviourPool<T> _subpool;
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

        public void Initialize()
        {
            if (_startup == EStartup.Manual)
                _pool.Initialize(_initialize);
        }

        protected virtual void Awake()
        {
            _subpool = new ReusableBehaviourPool<T>(_capacity, _prefab);
            _pool = new RepoolablePool<T>(_capacity, _subpool);

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

    public class RepoolablePoolBehaviour : RepoolablePoolBehaviour<ReusableBehaviour>
    {
        public override IRepoolable<ReusableBehaviour> Borrow()
        {
            var item = base.Borrow();
            item.Value.transform.SetParent(this.transform);
            return item;
        }
    }
}
