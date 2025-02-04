namespace SOSXR.ConfigData
{
    public class ConfigBoolToUnityEvent : BaseValueToUnityEvent<bool>
    {
        public bool Invert = false;


        protected override void FireEvent(bool value)
        {
            if (Invert)
            {
                value = !value;
            }

            EventToFire?.Invoke(value);
        }
    }
}