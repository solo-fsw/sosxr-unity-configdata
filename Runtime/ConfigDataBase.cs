using UnityEngine;


/// <summary>
///     Derive from this class to create a new ConfigData.
///     This is so that you can always create your own data class and not have to worry about the implementation.
///     The ConfigDataHandler will want this base class as the type for the data it holds.
/// </summary>
public abstract class ConfigDataBase : ScriptableObject
{
    [HideInInspector] public ConfigDataHandler DataHandler; // This is hidden in the inspector as it is drawn by the ConfigDataEditor


    private void OnValidate()
    {
        if (DataHandler == null || DataHandler.ConfigData == this)
        {
            return;
        }

        DataHandler.ConfigData = this;
            
        Debug.Log("ConfigDataHandler ConfigData has been updated to match the current ConfigData");
    }
}