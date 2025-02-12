namespace SOSXR.ConfigData
{
    public class ConfigFloatToUnityEvent : ConfigValueToUnityEvent<float>
    {
        protected override void FireEvent(float value)
        {
            EventToFire?.Invoke(value);
        }
    }
}