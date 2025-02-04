using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;


namespace SOSXR.ConfigData
{
    [Serializable]
    public static class HandleConfigData
    {
        public static string ConfigPath { get; private set; }
        private static string _previousJson;


        private static string _previousConfigPath;

        public static Action OnConfigDataChanged;


        /// <summary>
        ///     Gets the path to the config file in the persistent data path. Check the User
        ///     name, Project name and Company name. Given that the file name is "config":
        ///     On Android is at:
        ///     - /storage/emulated/0/Android/data/com.DefaultCompany.ProjectName/files/config.json
        ///     On macOS is at:
        ///     - /Users/[USERNAME]/Library/Application Support/DefaultCompany/ProjectName/config.jsoni
        ///     On Windows is at:
        ///     - C:\Users\[USERNAME]\AppData\LocalLow\DefaultCompany\ProjectName\config.json
        /// </summary>
        public static string CreateConfigPath(string fileName)
        {
            ConfigPath = Path.Combine(Application.persistentDataPath, string.Concat(fileName, ".json"));

            return ConfigPath;
        }


        /// <summary>
        ///     Creates a new default config and writes it to the persistent data path.
        /// </summary>
        [ContextMenu(nameof(CreateNewConfigJsonFile))]
        public static void CreateNewConfigJsonFile(BaseConfigData configData)
        {
            if (File.Exists(_previousConfigPath))
            {
                DeleteConfigJsonFile(_previousConfigPath);
            }

            if (File.Exists(ConfigPath))
            {
                DeleteConfigJsonFile(ConfigPath);
            }

            if (configData == null)
            {
                Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

                return;
            }


            CreateConfigPath(configData.ConfigName);

            try
            {
                var jsonData = JsonUtility.ToJson(configData, true);
                jsonData = CleanJson(jsonData);
                File.WriteAllText(ConfigPath, jsonData);

                OnConfigDataChanged?.Invoke();

                _previousConfigPath = ConfigPath;
                _previousJson = jsonData;
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Failed to create default config file: " + e.Message);
            }
        }


        /// <summary>
        ///     Loads the JSON data from the config file in the persistent data path.
        /// </summary>
        [ContextMenu(nameof(LoadConfigFromJsonFile))]
        public static void LoadConfigFromJsonFile(BaseConfigData configData)
        {
            if (configData == null)
            {
                Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

                return;
            }

            CreateConfigPath(configData.ConfigName);

            if (!File.Exists(ConfigPath))
            {
                CreateNewConfigJsonFile(configData);

                Debug.Log("Could not find existing Json config file at: " + ConfigPath + "" +
                          "\nCreated a new one instead.");

                return;
            }

            var jsonData = File.ReadAllText(ConfigPath);
            jsonData = CleanJson(jsonData);

            if (_previousJson == jsonData)
            {
                Debug.LogFormat("ConfigData has not changed.");

                return;
            }

            try
            {
                JsonUtility.FromJsonOverwrite(jsonData, configData);

                OnConfigDataChanged?.Invoke();
                _previousJson = jsonData;
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


        [ContextMenu(nameof(AmendConfigJsonFile))]
        public static void AmendConfigJsonFile(BaseConfigData configData)
        {
            if (_previousConfigPath != CreateConfigPath(configData.ConfigName))
            {
                DeleteConfigJsonFile(_previousConfigPath);
            }

            if (!File.Exists(ConfigPath))
            {
                CreateNewConfigJsonFile(configData);

                return;
            }

            if (configData == null)
            {
                Debug.LogWarningFormat("ConfigData is null. Please assign a ConfigData to this ConfigDataHandler.");

                return;
            }

            var jsonData = JsonUtility.ToJson(configData, true);
            jsonData = CleanJson(jsonData);


            if (_previousJson == jsonData)
            {
                Debug.LogFormat("ConfigData has not changed.");

                return;
            }

            try
            {
                File.WriteAllText(ConfigPath, jsonData);

                OnConfigDataChanged?.Invoke();

                _previousConfigPath = ConfigPath;
                _previousJson = jsonData;

                Debug.LogFormat("Amended config file at: " + ConfigPath);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Failed to amend config file: " + e.Message);
            }
        }


        [ContextMenu(nameof(DeleteConfigJsonFile))]
        public static void DeleteConfigJsonFile(string configPath)
        {
            if (!File.Exists(configPath))
            {
                return;
            }

            File.Delete(configPath);

            _previousJson = string.Empty;
            _previousConfigPath = null;
        }


        /// <summary>
        ///     Properties have an ugly <PropertyName>k__BackingField in the json. This cleans it up.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string CleanJson(string json)
        {
            return Regex.Replace(json, @"""<(.+?)>k__BackingField""", @"""$1""");
        }
    }
}