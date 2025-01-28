using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConfigDataHandler))]
public class ConfigDataHandlerEditor : Editor
{
    private Editor _configDataEditor;


    public override void OnInspectorGUI()
    {
        var configDataHandler = (ConfigDataHandler) target;

        DrawDefaultInspector();
        
        GUILayout.Space(15);

        if (configDataHandler.ConfigData != null)
        {
            if (_configDataEditor == null)
            {
                _configDataEditor = CreateEditor(configDataHandler.ConfigData);
            }

            _configDataEditor.OnInspectorGUI();
        }
        else
        {
            EditorGUILayout.HelpBox("ConfigData is null. Please assign a ScriptableObject.", MessageType.Warning);
        }

        if (configDataHandler.ConfigData == null)
        {
            return;
        }

        GUILayout.Space(15);
        
        if (GUILayout.Button(nameof(configDataHandler.CreateConfigJson)))
        {
            configDataHandler.CreateConfigJson();
        }

        if (GUILayout.Button(nameof(configDataHandler.LoadConfigFromJson)))
        {
            configDataHandler.LoadConfigFromJson();
        }

        if (GUILayout.Button("Amend Config Json with current Inspector values"))
        {
            configDataHandler.AmendConfigData();
        }

        if (GUILayout.Button(nameof(configDataHandler.DeleteConfigJson)))
        {
            configDataHandler.DeleteConfigJson();
        }
    }
}