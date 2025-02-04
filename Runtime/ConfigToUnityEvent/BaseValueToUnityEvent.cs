using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


namespace SOSXR.ConfigData
{
    public abstract class BaseValueToUnityEvent<T> : MonoBehaviour
    {
        public BaseConfigData ConfigData;
        public string ValueName;
        public bool RunOnStart = true;
        public UnityEvent<T> EventToFire;
        public bool SubscribeToChanges = true;
        public Func<T> GetValue { get; private set; }


        private void OnValidate()
        {
            CacheValueGetter();
        }


        private void Awake()
        {
            CacheValueGetter();
        }


        private void Start()
        {
            if (!RunOnStart)
            {
                return;
            }

            FindValuesAndFireEvent();
        }


        private void OnEnable()
        {
            if (SubscribeToChanges)
            {
                HandleConfigData.OnConfigDataChanged += FindValuesAndFireEvent;
            }
        }


        public void FindValuesAndFireEvent()
        {
            if (GetValue == null)
            {
                var typeName = typeof(T).Name;

                if (typeName == "Single")
                {
                    typeName = "Float";
                }

                Debug.LogErrorFormat(this, $"Property or Field of type {typeName} '{ValueName}' not found in {ConfigData?.GetType().Name}.");

                return;
            }

            FireEvent(GetValue());
        }


        protected abstract void FireEvent(T value);


        public void CacheValueGetter()
        {
            if (ConfigData == null || string.IsNullOrEmpty(ValueName))
            {
                GetValue = null;

                return;
            }

            var configType = ConfigData.GetType();

            // Try property first
            var property = configType.GetProperty(ValueName, BindingFlags.Public | BindingFlags.Instance);

            if (property != null && property.PropertyType == typeof(T) && property.CanRead)
            {
                GetValue = () => (T) property.GetValue(ConfigData);

                return;
            }

            // Try field
            var field = configType.GetField(ValueName, BindingFlags.Public | BindingFlags.Instance);

            if (field != null && field.FieldType == typeof(T))
            {
                GetValue = () => (T) field.GetValue(ConfigData);

                return;
            }

            GetValue = null;
        }


        private void OnDisable()
        {
            if (SubscribeToChanges)
            {
                HandleConfigData.OnConfigDataChanged -= FindValuesAndFireEvent;
            }
        }
    }
}