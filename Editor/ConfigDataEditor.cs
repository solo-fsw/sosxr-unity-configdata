using System.IO;
using UnityEditor;
using UnityEngine;


namespace SOSXR.ConfigData
{
    [CustomEditor(typeof(BaseConfigData), true)]
    public class ConfigDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var configData = (BaseConfigData) target;

            DrawDefaultInspector();

            GUILayout.Space(50);

            EditorGUILayout.LabelField("ConfigData", EditorStyles.boldLabel);

            if (!File.Exists(HandleConfigData.ConfigPath) && GUILayout.Button(nameof(HandleConfigData.CreateNewConfigJsonFile)))
            {
                HandleConfigData.CreateNewConfigJsonFile(configData);
            }

            if (GUILayout.Button(nameof(HandleConfigData.LoadConfigFromJsonFile)))
            {
                HandleConfigData.LoadConfigFromJsonFile(configData);
            }

            if (File.Exists(HandleConfigData.ConfigPath) && GUILayout.Button("Amend Config Json with current Inspector values"))
            {
                HandleConfigData.AmendConfigJsonFile(configData);
            }

            if (File.Exists(HandleConfigData.ConfigPath) && GUILayout.Button(nameof(HandleConfigData.DeleteConfigJsonFile)))
            {
                HandleConfigData.DeleteConfigJsonFile(HandleConfigData.ConfigPath);
            }

            #if !UNITY_EDITOR_LINUX
            if (File.Exists(HandleConfigData.ConfigPath) && GUILayout.Button("Reveal in Finder"))
            {
                EditorUtility.RevealInFinder(HandleConfigData.ConfigPath);
            }
            #endif
        }
    }
}