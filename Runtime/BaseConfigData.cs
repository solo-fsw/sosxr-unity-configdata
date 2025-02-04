using UnityEngine;


namespace SOSXR.ConfigData
{
    /// <summary>
    ///     Derive from this class to create a new ConfigData.
    ///     This is so that you can always create your own data class and not have to worry about the implementation.
    ///     The ConfigDataHandler will want this base class as the type for the data it holds.
    /// </summary>
    public abstract class BaseConfigData : ScriptableObject
    {
        public string ConfigName => name; // Always return the asset's name in the Project window
    }
}