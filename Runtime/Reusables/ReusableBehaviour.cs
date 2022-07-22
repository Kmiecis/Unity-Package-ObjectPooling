using UnityEngine;

namespace Common.Pooling
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> <see cref="IReusable"/> handling gameObject visibility
    /// </summary>
    public class ReusableBehaviour : MonoBehaviour, IReusable
    {
        public virtual void OnBorrow()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnReturn()
        {
            gameObject.SetActive(false);
        }
    }
}
