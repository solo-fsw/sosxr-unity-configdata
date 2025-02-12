using UnityEngine;


namespace SOSXR.ConfigData
{
    public class ConfigVector3ToUnityEvent : ConfigValueToUnityEvent<Vector3>
    {
        protected override void FireEvent(Vector3 value)
        {
            EventToFire?.Invoke(value);
        }
    }
}