using UnityEngine;


namespace SOSXR.ConfigData
{
    /// <summary>
    ///     The only purpose of this class is to hold a reference to a ConfigData in the Hierarchy.
    ///     This can be useful for visualising the ConfigData.
    ///     It can be important in the (unlikely) case that the ConfigData is not referenced by any other object, and therefore
    ///     not loaded into memory. However, if your ConfigData is not referenced by any other object, then you're clearly not
    ///     using it, and you should probably not worry about it.
    /// </summary>
    public class WrapConfigData : MonoBehaviour
    {
        public BaseConfigData ConfigData;
    }
}