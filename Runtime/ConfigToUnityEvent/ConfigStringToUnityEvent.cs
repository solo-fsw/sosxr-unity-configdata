namespace SOSXR.ConfigData
{
    public class ValueStringToUnityEvent : BaseValueToUnityEvent<string>
    {
        protected override void FireEvent(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "";
            }

            EventToFire?.Invoke(value);
        }
    }
}