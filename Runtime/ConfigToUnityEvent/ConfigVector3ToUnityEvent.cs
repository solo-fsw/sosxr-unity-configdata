using UnityEngine;


namespace SOSXR.ConfigData
{
    public class ValueVector3ToUnityEvent : BaseValueToUnityEvent<Vector3>
    {
        protected override void FireEvent(Vector3 value)
        {
            EventToFire?.Invoke(value);
        }
    }
}