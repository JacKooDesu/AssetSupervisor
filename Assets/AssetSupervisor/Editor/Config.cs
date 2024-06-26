using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace AssetSupervisor
{
    // [CreateAssetMenu(fileName = "Supervisor Config", menuName = "Config", order = 0)]
    internal class Config : ScriptableObject
    {
        public static Config Instance
        {
            get
            {
                if ((_instance ??= AssetDatabase.LoadAssetAtPath<Config>(Constants.CONFIG_PATH)) is not null)
                    return _instance;

                AssetDatabase.CreateAsset(_instance = ScriptableObject.CreateInstance<Config>(), Constants.CONFIG_PATH);
                EditorUtility.SetDirty(_instance);
                AssetDatabase.SaveAssets();

                return _instance;
            }
        }
        static Config _instance = null;

        [System.Flags]
        public enum ETrackType
        {
            None = 0,
            Texture = 1 << 0,
            Model = 1 << 1,
            Audio = 1 << 2,
        }

        [SerializeField] ETrackType _type = 0;
        public bool Tracking(ETrackType type) =>
            _type.HasFlag(type);
        // for path adding caching
        static string _pathBuffer;
        [SerializeField] List<string> _paths;
        internal List<string> Paths => _paths;
        [MenuItem("Assets/Add To Asset Supervisor Path")]
        static void AddPath()
        {
            Instance._paths.Add(_pathBuffer);

            EditorUtility.SetDirty(_instance);
            AssetDatabase.SaveAssetIfDirty(_instance);
        }
        [MenuItem("Assets/Add To Asset Supervisor Path", true)]
        static bool AddPathValidation()
        {
            var selection = Selection.activeObject;
            if (selection is null)
                return false;

            _pathBuffer = AssetDatabase.GetAssetPath(selection.GetInstanceID());
            return Directory.Exists(_pathBuffer);
        }

        public enum EStrategy
        {

        }
        [SerializeField] EStrategy _strategy;
    }
}