using UnityEngine;
using UnityEngine.Events;


public class ConfigDataDebugToUnityEvent : MonoBehaviour
{
    [SerializeField] private ConfigData m_configData;
    [SerializeField] private UnityEvent m_eventToFire;


    private void OnEnable()
    {
        if (m_configData == null)
        {
            Debug.LogError("ConfigData is null");

            return;
        }

        if (!m_configData.ShowDebug)
        {
            return;
        }

        m_eventToFire.Invoke();
    }
}