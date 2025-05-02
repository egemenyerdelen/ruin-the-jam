using UnityEngine;

namespace Helpers
{
    [DisallowMultipleComponent]
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (Application.isPlaying) 
                    return _instance;
                
                if (_instance == null) 
                    _instance = (T)FindFirstObjectByType(typeof(T));
#endif
                return _instance;
            }

            private set => _instance = value;
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(this);
            else
                _instance = this as T;
        }
    }
}