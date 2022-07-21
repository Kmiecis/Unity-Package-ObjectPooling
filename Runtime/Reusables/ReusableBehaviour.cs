using UnityEngine;

namespace Common.Pooling
{
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
