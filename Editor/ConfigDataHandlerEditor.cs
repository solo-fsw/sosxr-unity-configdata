using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ConfigDataHandler))]
public class ConfigDataHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DrawButtons();
    }


    public void DrawEmbeddedInspector()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_configFileName"));

        GUILayout.Space(5);

        DrawButtons();

        serializedObject.ApplyModifiedProperties();
    }


    private void DrawButtons()
    {
        var configDataHandler = (ConfigDataHandler) target;

        if (GUILayout.Button(nameof(configDataHandler.CreateNewConfigJsonFile)))
        {
            configDataHandler.CreateNewConfigJsonFile();
        }

        if (GUILayout.Button(nameof(configDataHandler.LoadConfigFromJsonFile)))
        {
            configDataHandler.LoadConfigFromJsonFile();
        }

        if (GUILayout.Button("Amend Config Json with current Inspector values"))
        {
            configDataHandler.AmendConfigJsonFile();
        }

        if (GUILayout.Button(nameof(configDataHandler.DeleteConfigJsonFile)))
        {
            configDataHandler.DeleteConfigJsonFile();
        }
    }
}