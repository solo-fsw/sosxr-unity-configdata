namespace SOSXR.ConfigData
{
    public class ValueIntToUnityEvent : BaseValueToUnityEvent<int>
    {
        protected override void FireEvent(int value)
        {
            EventToFire?.Invoke(value);
        }
    }
}