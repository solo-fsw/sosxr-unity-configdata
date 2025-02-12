using UnityEngine;


namespace SOSXR.ConfigData
{
    public class ConfigVector2ToUnityEvent : ConfigValueToUnityEvent<Vector2>
    {
        protected override void FireEvent(Vector2 value)
        {
            EventToFire?.Invoke(value);
        }
    }
}