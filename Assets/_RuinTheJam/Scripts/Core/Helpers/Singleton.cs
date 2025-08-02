using UnityEngine;

namespace _RuinTheJam.Core.Helpers
{
    [DisallowMultipleComponent] [DefaultExecutionOrder(-1000)]
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                
                _instance = FindAnyObjectByType<T>();

                if (_instance is null)
                {
                    Debug.LogError($"Singleton of type {typeof(T)} not found in scene!");
                }

                return _instance;
            }
            private set => _instance = value;
        }


        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(this);
            // else
            //     _instance = this as T;
        }
    }
}