namespace SOSXR.ConfigData
{
    public class ValueFloatToUnityEvent : BaseValueToUnityEvent<float>
    {
        protected override void FireEvent(float value)
        {
            EventToFire?.Invoke(value);
        }
    }
}