using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace SOSXR.ConfigData
{
    public abstract class BaseConfigData : ScriptableObject
    {
        [SerializeField] [HideInInspector] private List<string> m_updateJsonOnSpecificValueChanged;

        private readonly Dictionary<string, Delegate> _valueChangedEvents = new();
        private readonly Dictionary<string, object> _previousValues = new();

        public List<string> UpdateJsonOnValueChange
        {
            get => m_updateJsonOnSpecificValueChanged;
            set
            {
                if (value == m_updateJsonOnSpecificValueChanged)
                {
                    return;
                }

                m_updateJsonOnSpecificValueChanged = value;
                NotifyChange(nameof(UpdateJsonOnValueChange), value);
            }
        }


        private event Action<string, object> _onAnyValueChanged;


        protected void SetValue<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return;
            }

            field = value;
            NotifyChange(propertyName, value);
        }


        public void Initialize()
        {
            foreach (var propertyName in UpdateJsonOnValueChange)
            {
                Subscribe(propertyName, obj => UpdateJson());
                Debug.LogFormat(this, "Subscribed to {0} for updating the Json", propertyName);
            }
        }


        private void UpdateJson()
        {
            HandleConfigData.UpdateConfigJson(this);
            Debug.LogFormat(this, "Updated Json");
        }


        public void Subscribe(string propertyName, Action<object> callback)
        {
            if (_valueChangedEvents.TryGetValue(propertyName, out var existing))
            {
                _valueChangedEvents[propertyName] = Delegate.Combine(existing, callback);
            }
            else
            {
                _valueChangedEvents[propertyName] = callback;
            }
        }


        public void Unsubscribe(string propertyName, Action<object> callback)
        {
            if (_valueChangedEvents.TryGetValue(propertyName, out var existing))
            {
                var updated = Delegate.Remove(existing, callback);

                if (updated == null)
                {
                    _valueChangedEvents.Remove(propertyName);
                }
                else
                {
                    _valueChangedEvents[propertyName] = updated;
                }
            }
        }


        public void SubscribeToAny(Action<string, object> callback)
        {
            _onAnyValueChanged += callback;
        }


        public void UnsubscribeFromAny(Action<string, object> callback)
        {
            _onAnyValueChanged -= callback;
        }


        private void NotifyChange(string propertyName, object value)
        {
            if (_valueChangedEvents.TryGetValue(propertyName, out var callback))
            {
                try
                {
                    (callback as Action<object>)?.Invoke(value);
                    Debug.LogFormat(this, $"Notified specific listeners for {propertyName}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error notifying specific listeners for {propertyName}: {ex}");
                }
            }

            try
            {
                _onAnyValueChanged?.Invoke(propertyName, value);
                Debug.LogFormat(this, $"ConfigData has changed");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error notifying general listeners for {propertyName}: {ex}");
            }
        }


        private void OnValidate()
        {
            var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Build a mapping of field names to property names
            var fieldToPropertyMap = new Dictionary<string, string>();

            foreach (var property in properties)
            {
                var backingFieldName = $"m_{char.ToLowerInvariant(property.Name[0])}{property.Name.Substring(1)}";
                fieldToPropertyMap[backingFieldName] = property.Name;
            }

            foreach (var field in fields)
            {
                var currentValue = field.GetValue(this);

                // Skip if this is our UpdateJsonOnValueChange backing field
                if (field.Name == nameof(m_updateJsonOnSpecificValueChanged))
                {
                    continue;
                }

                // Get the previous value, or set it if this is the first time
                if (!_previousValues.TryGetValue(field.Name, out var previousValue))
                {
                    _previousValues[field.Name] = currentValue;

                    continue;
                }

                // Check if the value actually changed
                if (!Equals(previousValue, currentValue))
                {
                    // Update the stored previous value
                    _previousValues[field.Name] = currentValue;

                    // Find the corresponding property name
                    if (fieldToPropertyMap.TryGetValue(field.Name, out var propertyName))
                    {
                        // Notify using the property name instead of the field name
                        NotifyChange(propertyName, currentValue);
                    }
                    else
                    {
                        // If no property found, use the field name directly
                        NotifyChange(field.Name, currentValue);
                    }

                    // Always notify general listeners with the same name we used above
                    _onAnyValueChanged?.Invoke(propertyName ?? field.Name, currentValue);
                }
            }
        }


        [ContextMenu(nameof(LoadConfigJson))]
        public void LoadConfigJson()
        {
            HandleConfigData.LoadConfigFromJson(this);
        }


        [ContextMenu(nameof(UpdateConfigJson))]
        public void UpdateConfigJson()
        {
            HandleConfigData.UpdateConfigJson(this);
        }


        public void Uninitialize()
        {
            foreach (var change in UpdateJsonOnValueChange)
            {
                Unsubscribe(change, obj => UpdateJson());
            }
        }


        private void OnDestroy()
        {
            HandleConfigData.DeleteConfigJson();
        }
    }
}