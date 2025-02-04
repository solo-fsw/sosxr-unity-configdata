using UnityEngine;


namespace SOSXR.ConfigData
{
    public class ValueVector2ToUnityEvent : BaseValueToUnityEvent<Vector2>
    {
        protected override void FireEvent(Vector2 value)
        {
            EventToFire?.Invoke(value);
        }
    }
}