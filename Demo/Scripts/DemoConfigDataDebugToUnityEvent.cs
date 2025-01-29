using UnityEngine;
using UnityEngine.Events;


public class DemoConfigDataDebugToUnityEvent : MonoBehaviour
{
    [SerializeField] private DemoConfigData m_demoConfigData;
    [SerializeField] private UnityEvent m_eventToFire;


    private void OnEnable()
    {
        if (m_demoConfigData == null)
        {
            Debug.LogError("ConfigData is null");

            return;
        }

        if (!m_demoConfigData.ShowDebug)
        {
            return;
        }

        m_eventToFire.Invoke();
    }
}