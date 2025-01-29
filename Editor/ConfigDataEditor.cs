using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConfigDataBase), true)]
public class ConfigDataEditor : Editor
{
    private ConfigDataHandlerEditor _dataHandlerEditor;


    public override void OnInspectorGUI()
    {
        var configData = (ConfigDataBase) target;

        DrawDefaultInspector();
        
        GUILayout.Space(50);
        
        EditorGUILayout.LabelField("ConfigData", EditorStyles.boldLabel);
        
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("DataHandler"));
        serializedObject.ApplyModifiedProperties();

        if (configData.DataHandler == null)
        {
            EditorGUILayout.HelpBox("ConfigDataHandler is null. Please assign a ScriptableObject.", MessageType.Warning);

            return;
        }

        _dataHandlerEditor ??= CreateEditor(configData.DataHandler) as ConfigDataHandlerEditor;

        _dataHandlerEditor?.DrawEmbeddedInspector();
    }
}