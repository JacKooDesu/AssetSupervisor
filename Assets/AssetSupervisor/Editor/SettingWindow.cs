#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace AssetSupervisor
{
    internal class SettingWindow : EditorWindow
    {
        [MenuItem("Asset Supervisor/Setting")]
        static void ShowWindow()
        {
            var window = GetWindow<SettingWindow>();
            window.titleContent = new GUIContent("Asset Supervisor Setting");
            window.Show();
        }

        void OnGUI()
        {

        }
    }
}

#endif