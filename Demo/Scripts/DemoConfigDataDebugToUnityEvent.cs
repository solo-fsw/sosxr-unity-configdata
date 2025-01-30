using UnityEngine;
using UnityEngine.Events;


public class DemoConfigDataDebugToUnityEvent : MonoBehaviour
{
    [SerializeField] private DemoConfigData m_demoConfigData;
    [SerializeField] private UnityEvent<bool> m_eventToFire;


    private void OnEnable()
    {
        if (m_demoConfigData == null)
        {
            Debug.LogError("ConfigData is null");

            return;
        }

        m_eventToFire.Invoke(m_demoConfigData.ShowDebug);
    }
}