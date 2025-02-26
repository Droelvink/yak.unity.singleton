using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace com.yak.singleton
{
    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                _instance = Resources.Load<T>($"SingleInstances/{typeof(T)}");
                Debug.Log(_instance);
                return _instance == null ? CreateInstance() : _instance;
            }
        }

        private static T CreateInstance()
        {
            _instance = CreateInstance<T>();
#if UNITY_EDITOR
            if(!Directory.Exists("Assets/Resources")) Directory.CreateDirectory("Assets/Resources");
            if(!Directory.Exists("Assets/Resources/SingleInstances")) Directory.CreateDirectory("Assets/Resources/SingleInstances");
            AssetDatabase.CreateAsset(_instance, $"Assets/Resources/SingleInstances/{typeof(T)}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
            return _instance;
        }

#if UNITY_EDITOR
        protected static void Open()
        {
            Selection.activeObject = Instance;
            EditorGUIUtility.PingObject(Instance);
            AssetDatabase.OpenAsset(Instance);
        }
#endif
    }

}
