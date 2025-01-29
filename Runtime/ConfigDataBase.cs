using UnityEngine;


/// <summary>
///     Derive from this class to create a new ConfigData.
///     This is so that you can always create your own data class and not have to worry about the implementation.
///     The ConfigDataHandler will want this base class as the type for the data it holds.
/// </summary>
public abstract class ConfigDataBase : ScriptableObject
{
}