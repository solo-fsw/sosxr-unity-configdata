using System;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName = "ConfigDataHandler", menuName = "SOSXR/ConfigDataHandler")]
public class ConfigDataHandler : ScriptableObject
{
    [Tooltip("Always has an extension of .json")]
    [SerializeField] private string m_configFileName = "config";
    public ConfigData ConfigData;

    /// <summary>
    ///     Gets the path to the config file in the persistent data path. Check the User
    ///     name, Project name and Company name. Given that the file name is "config":
    ///     On Android is at:
    ///     - /storage/emulated/0/Android/data/com.DefaultCompany.ProjectName/files/config.json
    ///     On macOS is at:
    ///     - /Users/[USERNAME]/Library/Application Support/DefaultCompany/ProjectName/config.json
    ///     On Windows is at:
    ///     - C:\Users\[USERNAME]\AppData\LocalLow\DefaultCompany\ProjectName\config.json
    /// </summary>
    private string ConfigPath => Path.Combine(Application.persistentDataPath, m_configFileName + ".json");


    /// <summary>
    ///     Creates a new default config and writes it to the persistent data path.
    /// </summary>
    [ContextMenu(nameof(CreateConfigJson))]
    public void CreateConfigJson()
    {
        if (File.Exists(ConfigPath))
        {
            Debug.LogWarningFormat("You're trying to create a new config file, but it already exists at: " + ConfigPath + " Try amending the existing file instead.");

            return;
        }

        if (ConfigData == null)
        {
            Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

            return;
        }

        try
        {
            var jsonData = JsonUtility.ToJson(ConfigData, true);
            File.WriteAllText(ConfigPath, jsonData);

            Debug.LogFormat("Created new default config file at: " + ConfigPath);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Failed to create default config file: " + e.Message);
        }
    }


    /// <summary>
    ///     Loads the JSON data from the config file in the persistent data path.
    /// </summary>
    [ContextMenu(nameof(LoadConfigFromJson))]
    public void LoadConfigFromJson()
    {
        if (!File.Exists(ConfigPath))
        {
            Debug.LogWarningFormat("You're trying to load a config file, but it is not found at: " + ConfigPath);
            CreateConfigJson();

            return;
        }

        if (ConfigData == null)
        {
            Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

            return;
        }

        try
        {
            var jsonData = File.ReadAllText(ConfigPath);
            JsonUtility.FromJsonOverwrite(jsonData, this);
            Debug.LogFormat("Config loaded successfully form " + ConfigPath);
        }
        catch (UnauthorizedAccessException e)
        {
            Debug.LogErrorFormat("UnauthorizedAccessException: " + e.Message);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Exception: " + e.Message);
        }
    }


    [ContextMenu(nameof(AmendConfigData))]
    public void AmendConfigData()
    {
        if (!File.Exists(ConfigPath))
        {
            Debug.LogWarningFormat("You're trying to amend an existing config file, but it is not found at: " + ConfigPath);
            CreateConfigJson();

            return;
        }

        if (ConfigData == null)
        {
            Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

            return;
        }

        try
        {
            var jsonData = JsonUtility.ToJson(ConfigData, true);
            File.WriteAllText(ConfigPath, jsonData);

            Debug.LogFormat("Amended config file at: " + ConfigPath);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Failed to amend config file: " + e.Message);
        }
    }


    private void OnDestroy()
    {
        DeleteConfigJson();
    }


    [ContextMenu(nameof(DeleteConfigJson))]
    public void DeleteConfigJson()
    {
        if (File.Exists(ConfigPath))
        {
            File.Delete(ConfigPath);
            Debug.LogFormat("Deleted config file at: " + ConfigPath);
        }
    }
}