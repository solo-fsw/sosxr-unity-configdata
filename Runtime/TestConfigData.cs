using UnityEngine;


namespace SOSXR.ConfigData
{
    [CreateAssetMenu(fileName = "Test Config Data", menuName = "SOSXR/Config Data/Test Config Data")]
    public class TestConfigData : BaseConfigData
    {
        [SerializeField] private string m_baseURL = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeA";
        [SerializeField] private string m_taskName = "TaskToDo";
        [SerializeField] private bool m_showDebug = false;
        [SerializeField] [Range(0, 30)] private int m_debugUpdateInterval = 1;
        [SerializeField] private bool m_showAffordances = false;

        public string BaseURL
        {
            get => m_baseURL;
            set => SetValue(ref m_baseURL, value, nameof(BaseURL));
        }

        public string TaskName
        {
            get => m_taskName;
            set => SetValue(ref m_taskName, value, nameof(TaskName));
        }

        public bool ShowDebug
        {
            get => m_showDebug;
            set => SetValue(ref m_showDebug, value, nameof(ShowDebug));
        }

        public int DebugUpdateInterval
        {
            get => m_debugUpdateInterval;
            set => SetValue(ref m_debugUpdateInterval, value, nameof(DebugUpdateInterval));
        }

        public bool ShowAffordances
        {
            get => m_showAffordances;
            set => SetValue(ref m_showAffordances, value, nameof(ShowAffordances));
        }
    }
}