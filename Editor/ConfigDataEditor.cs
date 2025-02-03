using System.IO;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConfigDataBase), true)]
public class ConfigDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var configData = (ConfigDataBase) target;

        DrawDefaultInspector();

        GUILayout.Space(50);

        EditorGUILayout.LabelField("ConfigData", EditorStyles.boldLabel);

        if (GUILayout.Button(nameof(ConfigDataHandler.CreateNewConfigJsonFile)))
        {
            ConfigDataHandler.CreateNewConfigJsonFile(configData);
        }

        if (GUILayout.Button(nameof(ConfigDataHandler.LoadConfigFromJsonFile)))
        {
            ConfigDataHandler.LoadConfigFromJsonFile(configData);
        }

        if (GUILayout.Button("Amend Config Json with current Inspector values"))
        {
            ConfigDataHandler.AmendConfigJsonFile(configData);
        }

        if (GUILayout.Button(nameof(ConfigDataHandler.DeleteConfigJsonFile)))
        {
            ConfigDataHandler.DeleteConfigJsonFile();
        }
        
        #if !UNITY_EDITOR_LINUX
        if (File.Exists(ConfigDataHandler.ConfigPath) && GUILayout.Button("Reveal in Finder"))
        {
            EditorUtility.RevealInFinder(ConfigDataHandler.ConfigPath);
        }
        #endif
    }
}