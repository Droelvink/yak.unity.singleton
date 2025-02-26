using System;
using UnityEngine;

namespace com.yak.singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if(!_instance) _instance = FindFirstObjectByType<T>();
                if (_instance) return _instance;
                
                Debug.Log("Attempting to call function from an undefined singleton class.");
                return null;
            }
        }
    
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                Debug.LogWarning($"Attempted to create second instance of singleton ({gameObject.name}), destroying new instance");
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _instance = (T)this;
            }

            AfterAwake();
        }

        protected virtual void AfterAwake() { }
    }
}
